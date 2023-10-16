using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Utils.ConfigOptions;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public interface ICloudStorageService
        {
            Task<string> GetSignedUrlAsync(string filenameToRead, int timeOutIntMinutes = 30);
            Task<string> UploadFileAsync(IFormFile fileToUpload, string fileNameToSave);
            Task DeleteFileAsync(string fileNameToDelete);
        }

        public class CloudStorageService : ICloudStorageService
        {
            private readonly GCSConfigOptions _options;
            private readonly ILogger<CloudStorageService> _logger;
            private readonly GoogleCredential _googleCredential;

        public CloudStorageService(IOptions<GCSConfigOptions> options, ILogger<CloudStorageService> logger)
            {
                _options = options.Value;
                _logger = logger;
                try
                {
                    var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIROMENT");
                    if (enviroment == Environments.Production) {
                        _googleCredential = GoogleCredential.FromJson(_options.GCPStorageAuthFile);
                    } else {
                        _googleCredential = GoogleCredential.FromFile(_options.GCPStorageAuthFile);
                    }
                }
                catch(Exception e)
                {
                    _logger.LogError($"{e.Message}");
                    throw;
                }
            }
            public async Task DeleteFileAsync(string fileNameToDelete)
            {
                try
                {
                    using (var storageClient  = StorageClient.Create(_googleCredential))
                    {
                        await storageClient.DeleteObjectAsync(_options.GoogleCloudStorageBucketName, fileNameToDelete);
                    }
                    _logger.LogInformation($"File {fileNameToDelete} deleted");
                }
                catch (Exception e)
                {
                    _logger.LogError($"Error occured while deleting file {fileNameToDelete}: {e.Message}");
                }
            }

            public async Task<string> GetSignedUrlAsync(string filenameToRead, int timeOutIntMinutes = 30)
            {
                try
                {
                    var sac = _googleCredential.UnderlyingCredential as ServiceAccountCredential;
                    var urlSigner = UrlSigner.FromServiceAccountCredential(sac);
                    var signedUrl = await urlSigner.SignAsync(_options.GoogleCloudStorageBucketName, filenameToRead, TimeSpan.FromMinutes(timeOutIntMinutes));
                    _logger.LogInformation($"Signed url obtained for file {filenameToRead}");
                    return signedUrl.ToString();
                }
                catch (Exception e)
                {
                    _logger.LogError($"error occured while obtaining signed url for file {filenameToRead}: {e.Message}");
                    throw;
                }
            }

            public async Task<string> UploadFileAsync(IFormFile fileToUpload, string fileNameToSave)
            {
                try
                {
                    _logger.LogInformation($"Uploading: file {fileNameToSave} to storage {_options.GoogleCloudStorageBucketName}");
                    using (var memoryStream = new MemoryStream())
                    {
                        await fileToUpload.CopyToAsync(memoryStream);

                        using (var storageClient = StorageClient.Create(_googleCredential))
                        {
                            var uploadFile = await storageClient.UploadObjectAsync(_options.GoogleCloudStorageBucketName, fileNameToSave, fileToUpload.ContentType, memoryStream);
                            _logger.LogInformation($"Uploaded: file {fileNameToSave} to storage {_options.GoogleCloudStorageBucketName}");
                            return uploadFile.MediaLink;
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"Error while upload file {fileNameToSave}: {e.Message}");
                    throw;
                }
            }
        }
}