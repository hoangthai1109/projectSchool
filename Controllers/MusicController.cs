using System.Diagnostics;
using API.BodyRequest;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class MusicController: baseApiController
    {
        readonly DataContext _context;
        readonly IImageService _imageService;
        readonly ICloudStorageService _cloudStorageService;

        public MusicController(DataContext context, IImageService imageService, ICloudStorageService cloudStorageService) {
            _context = context;
            _imageService = imageService;
            _cloudStorageService = cloudStorageService;
        }

        [HttpPost("/unau/get-music-list")]
        public async Task<IActionResult> getMusicList([FromBody]PaginationFilter filter) {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.Musics.Where(x => x.MusicStatus == "2" && x.musicName.Contains(filter.KeySearch)).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
            var TotalRecords = await _context.Musics.CountAsync();
            return Ok(new PagedResponse<List<Music>>(pagedData, validFilter.PageNumber, validFilter.PageSize, TotalRecords));
        }

        [HttpPost("/unau/get-music-list-by-owner")]
        public async Task<IActionResult> getMusicListByOwner([FromBody]PaginationFilter filter) {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.Musics.Where(x => x.MusicStatus == "2" && x.owner.Contains(filter.KeySearch)).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
            var TotalRecords = await _context.Musics.CountAsync();
            return Ok(new PagedResponse<List<Music>>(pagedData, validFilter.PageNumber, validFilter.PageSize, TotalRecords));
        }

        [HttpPost("/get-music-list-by-owner")]
        public async Task<IActionResult> getMusicByType([FromBody]RequestType requestType) {
            var validFilter = new PaginationFilter(requestType.PageNumber, requestType.PageSize);
            var pagedData = await _context.Musics.Where(x => x.MusicStatus == "2" && x.MusicType == requestType.MusicType).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
            var TotalRecords = await _context.Musics.CountAsync();
            return Ok(new PagedResponse<List<Music>>(pagedData, validFilter.PageNumber, validFilter.PageSize, TotalRecords));
        }

        [HttpPost("get-music-by-code")]
        public async Task<IActionResult> getMusicByCode([FromBody]RequestByCode requestByCode)
        {
            var music = await _context.Musics.FirstOrDefaultAsync(x => x.MusicCode == requestByCode.MusicCode);
            if (music == null) return BadRequest("Cannot find the song");

            return Ok(music);
        }

        [HttpPost("add-new")]
        public async Task<ActionResult<MusicDto>> AddNewMusic([FromBody]MusicDto musicDto) {
            try {
                if (await CheckExistMusic(musicDto.musicName, musicDto.owner)) return BadRequest("Cannot add new music");

                var musicPlayList = await _context.MusicPlaylistUsers.FirstOrDefaultAsync(x => x.MusicCode == musicDto.MusicCode);

                var playList = await _context.PlaylistUsers.FirstOrDefaultAsync(x => x.PlayListUserCode == musicPlayList.PlayListUserCode);

                var music = new Music {
                    MusicCode = Guid.NewGuid().ToString(),
                    musicName = musicDto.musicName,
                    owner = musicDto.owner,
                    isMain = musicDto.isMain,
                    isAlbum = musicDto.isAlbum,
                    AlbumName = musicDto.AlbumName,
                    ReleaseDate = musicDto.ReleaseDate,
                    CreateDate = DateTime.Now,
                    ModifiedBy = musicDto.ModifiedBy,
                    MusicType = playList.PlaylistType,
                    TotalListen = 0,
                    ModifiedDate = DateTime.Now,
                    MusicStatus = playList.Status,
                };

                _context.Musics.Add(music);
                await _context.SaveChangesAsync();

                return Ok(music);
            } catch (Exception e) {
                throw(e);
            }
        }

        [HttpPost("/unau/update-music")]
        public async Task<ActionResult<MusicDto>> UpdateMusic([FromHeader]IFormFile file, [FromBody]MusicDto musicDto) {
            try
            {
                var music = await _context.Musics.FirstOrDefaultAsync(x => x.MusicCode == musicDto.MusicCode);
                if (music.MusicCode != musicDto.MusicCode) return BadRequest();

                var musicPlayList = await _context.MusicPlaylistUsers.FirstOrDefaultAsync(x => x.MusicCode == musicDto.MusicCode);

                var playList = await _context.PlaylistUsers.FirstOrDefaultAsync(x => x.PlayListUserCode == musicPlayList.PlayListUserCode);

                var fileInfo = new FileInfo(file.FileName);
                if (fileInfo.Exists && fileInfo.Length > 0) {
                    await ReplaceSong(file, musicDto);
                    music.musicName = musicDto.musicName;
                    music.MusicCode = musicDto.MusicCode;
                    music.owner = musicDto.owner;
                    music.isMain = musicDto.isMain;
                    music.Url = musicDto.Url;
                    music.isAlbum = musicDto.isAlbum;
                    music.AlbumImageUrl = musicDto.AlbumImageUrl;
                    music.AlbumName = musicDto.AlbumName;
                    music.ReleaseDate = musicDto.ReleaseDate;
                    music.CreateDate = musicDto.CreateDate;
                    music.ModifiedBy = musicDto.ModifiedBy;
                    music.TotalListen = musicDto.TotalListen;
                    music.MusicType = playList.PlaylistType;
                    music.ModifiedDate = DateTime.Now;
                    music.MusicStatus = playList.Status;
                    _context.Musics.Update(music);
                    await _context.SaveChangesAsync();
                    return Ok(musicDto);
                }
                else 
                {
                    var _music = new Music
                    {
                        musicName = musicDto.musicName,
                        MusicCode = musicDto.MusicCode,
                        owner = musicDto.owner,
                        SongPath = musicDto.SongPath,
                        TrustSongPath = musicDto.TrustSongPath,
                        isMain = musicDto.isMain,
                        Url = musicDto.Url,
                        isAlbum = musicDto.isAlbum,
                        AlbumImageUrl = musicDto.AlbumImageUrl,
                        AlbumName = musicDto.AlbumName,
                        ReleaseDate = musicDto.ReleaseDate,
                        CreateDate = musicDto.CreateDate,
                        ModifiedBy = musicDto.ModifiedBy,
                        TotalListen = musicDto.TotalListen,
                        MusicType = playList.PlaylistType,
                        ModifiedDate = DateTime.Now,
                        MusicStatus = playList.Status,
                    };
                    _context.Musics.Update(_music);
                    await _context.SaveChangesAsync();
                    return Ok(musicDto);
                }
            }
            catch(Exception e)
            {
                throw(e);
            }
        }

        [HttpPost("/unau/delete-song")]
        public async Task<ActionResult<Music>> DeleteSong([FromBody]int id) {
            try
            {
                var music = await _context.Musics.FindAsync(id);
                if (music == null) return BadRequest("Cannot find song");

                if (!string.IsNullOrEmpty(music.musicName))
                {
                    await _cloudStorageService.DeleteFileAsync(music.musicName);
                    music.musicName = String.Empty;
                    music.TrustSongPath = String.Empty;
                    music.SongPath = String.Empty;
                }
                _context.Musics.Remove(music);
                await _context.SaveChangesAsync();
                return StatusCode(200, "delete success");
            }
            catch (Exception e)
            {
                throw(e);
            }
        }

        [HttpPost("unau/upload-image")]
        public async Task<ActionResult<MusicDto>> UploadFile(IFormFile file, string musicCode) {
            try
            {
                var _music = await _context.Musics.FirstOrDefaultAsync(x => x.MusicCode == musicCode);

                if (_music == null) return BadRequest("Cannot find music info");

                var result = await _imageService.AddPhotoAsync(file);

                if (result.Error != null) return BadRequest(result.Error.Message);

                    _music.Url = result.SecureUrl.AbsoluteUri;

                if (_music.isAlbum == 1) {
                    _music.AlbumImageUrl = result.SecureUrl.AbsoluteUri;
                }

                _context.Musics.Update(_music);
                await _context.SaveChangesAsync();
                return Ok(_music);
            }
            catch(Exception e) {
                StatusCode(500, $"error: {e}");
                throw;
            }
        }

        [HttpPost("unau/upload-song")]
        public async Task<ActionResult<MusicDto>> UploadSong(IFormFile file, string musicCode, string musicName) {
            try
            {
                var _music = await _context.Musics.FirstOrDefaultAsync(x => x.MusicCode == musicCode);

                if (_music == null) return BadRequest("Cannot find music info");

                _music.musicName = musicName;
                _music.SongPath = await _cloudStorageService.UploadFileAsync(file, musicName);
                _music.TrustSongPath = await _cloudStorageService.GetSignedUrlAsync(musicName);

                _context.Musics.Update(_music);
                await _context.SaveChangesAsync();
                return Ok(_music);
            }
            catch(Exception e) {
                throw(e);
            }
        }

        [HttpPost("update-list-music")]
        public async Task<ActionResult<MusicDto>> UpdateMusicListen([FromBody]ListenMusic listenMusic)
        {
            try
            {
                var music = await _context.Musics.FirstOrDefaultAsync(x => x.MusicCode == listenMusic.MusicCode);
                if (music == null) return BadRequest("Cannot find music");

                var musicPlayListUser = await _context.MusicPlaylistUsers.FirstOrDefaultAsync(x => x.MusicCode == music.MusicCode);

                var playlist = await _context.PlaylistUsers.FirstOrDefaultAsync(x => x.PlayListUserCode == musicPlayListUser.PlayListUserCode); 

                music.TotalListen += 1;

                playlist.TotalListon += music.TotalListen;

                _context.PlaylistUsers.Update(playlist);
                _context.Musics.Update(music);
                await _context.SaveChangesAsync();

                return Ok(music);
            }
            catch(Exception e)
            {
                StatusCode(500, $"Error: {e}");
                throw;
            }
        }

        private string? GenerateFileNameTosave(string incomingFileName)
        {
            var fileName = Path.GetFileNameWithoutExtension(incomingFileName);
            var extension = Path.GetExtension(incomingFileName);
            return $"{fileName} - {DateTime.Now.ToUniversalTime().ToString("yyyyMMddHHmmss")}{extension}";
        }

        protected async Task<bool> CheckExistMusic(string musicName, string owner) {
            var dynamicMenu = await _context.Musics.FirstOrDefaultAsync(x => x.musicName == musicName && x.owner == owner);

            if (dynamicMenu == null) {
                return false;
            }

            return true;
        }

        private async Task ReplaceSong(IFormFile file, MusicDto musicDto) {
            var music = await _context.Musics.FindAsync(musicDto.MusicCode);
            if (musicDto.SongPath != null)
            {
                await _cloudStorageService.DeleteFileAsync(musicDto.musicName);
                
                music.SongPath = await _cloudStorageService.UploadFileAsync(file, musicDto.musicName);
                music.TrustSongPath = await _cloudStorageService.GetSignedUrlAsync(musicDto.musicName);
            }
        }

    }
}