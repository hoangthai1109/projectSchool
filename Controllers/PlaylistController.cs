using API.Data;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class PlaylistController: baseApiController
    {
        readonly DataContext _context;
        readonly IImageService _imageService;
        public PlaylistController(DataContext context, IImageService imageService) {
            _context = context;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlayList([FromBody]PaginationFilter filter) {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.PlayLists.Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
            var TotalRecords = await _context.Musics.CountAsync();
            return Ok(new PagedResponse<List<PlayList>>(pagedData, validFilter.PageNumber, validFilter.PageSize, TotalRecords));
        }

        [HttpPost("get-playlist-by-id")]
        public async Task<ActionResult<PlaylistDto>> GetPlayListById([FromBody]ListenMusic listenMusic)
        {
            try
            {
                if (listenMusic.PlayListCode == null) return BadRequest("Missing payload");
                
                var playlist = await _context.PlayLists.FindAsync(listenMusic.PlayListCode);

                if (playlist == null) return BadRequest("Cannot find playlist");

                var listMusic = _context.MusicPlaylists.Where(x => x.PlayListCode == playlist.PlayListCode).ToList();
                var countListMusic = await _context.MusicPlaylists.Where(x =>x .PlayListCode == playlist.PlayListCode).CountAsync();

                List<Music> musics = new List<Music>();
                for (int i = 0; i < countListMusic; i++) {
                    var item =  await _context.Musics.FindAsync(listMusic[i].MusicCode);
                    musics.Add(item);
                }

                PlaylistDto playlistDto = new PlaylistDto
                {
                    PlayListCode = playlist.PlayListCode,
                    PlaylistName = playlist.PlaylistName,
                    ImagePlaylist = playlist.ImagePlaylist,
                    Rating = playlist.Rating,
                    TotalListon = playlist.TotalListon,
                    TotalSong = playlist.TotalSong,
                    PlaylistType = playlist.PlaylistType,
                    CreatedDate = playlist.CreatedDate,
                    Musics = musics
                };
                
                return Ok(playlistDto);
            }
            catch(Exception e)
            {
                StatusCode(500, $"Error: {e}");
                throw;
            }
        }

        [HttpPost("create-playlist")]
        public async Task<ActionResult<PlaylistDto>> CreatePlaylist([FromBody]PlaylistDto playlistDto, [FromHeader]IFormFile formFile) {
            try
            {
                if (await CheckExistPlaylist(playlistDto.PlayListCode)) return BadRequest("The playlist has been created");

                var file = await _imageService.AddPhotoAsync(formFile);

                if (file.Error != null) return BadRequest(file.Error.Message);

                var playlist = new PlayList
                {
                    PlayListCode = Guid.NewGuid().ToString(),
                    PlaylistName = playlistDto.PlaylistName,
                    ImagePlaylist = file.SecureUrl.AbsoluteUri,
                    Rating = playlistDto.Rating,
                    TotalSong = playlistDto.TotalSong,
                    PlaylistType = playlistDto.PlaylistType,
                    TotalListon = 0,
                    CreatedDate = DateTime.Now
                };

                _context.PlayLists.Add(playlist);

                foreach(var music in playlistDto.Musics)
                {
                    var musicPLayList = new MusicPlaylist
                    {
                        MusicCode = music.MusicCode,
                        PlayListCode = playlist.PlayListCode
                    };

                    _context.MusicPlaylists.Add(musicPLayList); 
                }

                await _context.SaveChangesAsync();

                return Ok(playlistDto);
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error: {e}");
                throw;
            }
        }

        [HttpPost("update-playlist")]
        public async Task<ActionResult<PlaylistDto>> UpdatePlaylist([FromBody]PlaylistDto playlistDto, [FromHeader]IFormFile formFile) {
            try
            {
                var playlist = await _context.PlayLists.FindAsync(playlistDto.PlayListCode);

                if (playlist == null) return BadRequest("Cannot find playlist");

                var fileInfo = new FileInfo(formFile.FileName);

                playlist.PlaylistName = playlistDto.PlaylistName;
                playlist.TotalSong = playlistDto.TotalSong;
                playlist.TotalListon = playlistDto.TotalListon;
                playlist.Rating = playlistDto.Rating;
                playlist.PlaylistType = playlistDto.PlaylistType;
                
                if (fileInfo.Exists && fileInfo.Length > 0) {
                    var file = await _imageService.AddPhotoAsync(formFile);
                    if (file.Error != null) return BadRequest(file.Error.Message);
                    playlist.ImagePlaylist = file.Url.AbsoluteUri;
                }
                // var file = await _imageService.AddPhotoAsync(formFile);

                if (playlistDto.Musics.Count > 0) {
                    foreach(var music in playlistDto.Musics)
                    {
                        var musicPLayList = await _context.MusicPlaylists.FirstOrDefaultAsync(x => x.MusicCode == music.MusicCode);
                        if (musicPLayList == null) {
                            MusicPlaylist ms = new MusicPlaylist
                            {
                                MusicCode = music.MusicCode,
                                PlayListCode = playlistDto.PlayListCode,
                            };
                           _context.MusicPlaylists.Add(ms); 
                            await _context.SaveChangesAsync();
                        }
                    }
                }

                _context.Update(playlist);

                await _context.SaveChangesAsync();

                return Ok(playlistDto);
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error: {e}");
                throw;
            }
        }

        [HttpPost("update-listen")]
        public async Task<ActionResult<PlaylistDto>> UpdateListen([FromBody]ListenMusic ListenMusic) {
            try
            {
                var playlist = await _context.PlayLists.FindAsync(ListenMusic.PlayListCode);

                if (playlist == null) return BadRequest("Cannot find the playlist");

                playlist.TotalListon += 1;

                _context.Update(playlist);
                await _context.SaveChangesAsync();
                
                return StatusCode(200, playlist);
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error: {e}");
                throw;
            }
        }

        [HttpPost("delete-music")]
        public async Task<ActionResult> DeleteListen([FromBody]ListenMusic ListenMusic) {
            try
            {
                var playlist = await _context.PlayLists.FindAsync(ListenMusic.PlayListCode);
                var playlistMusic = await _context.MusicPlaylists.FindAsync(ListenMusic.MusicCode);

                if (playlist == null) return BadRequest("Cannot find the playlist");

                _context.PlayLists.Remove(playlist);

                _context.MusicPlaylists.Remove(playlistMusic);
                await _context.SaveChangesAsync();
                
                return StatusCode(200, "Delete success");
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error: {e}");
                throw;
            }
        }

        protected async Task<bool> CheckExistPlaylist(string playlistCode) {
            var check = await _context.PlayLists.FirstOrDefaultAsync(x => x.PlayListCode == playlistCode);

            if (check == null) {
                return true;
            }

            return false;
        }
    }
}