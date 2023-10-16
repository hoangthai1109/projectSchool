using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Utils.ConfigOptions
{
    public class GCSConfigOptions
    {
        public string? GCPStorageAuthFile {get;set;}
        public string? GoogleCloudStorageBucketName {get;set;}
    }
}