using API.BodyRequest;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class PlayListUserController: baseApiController
    {
        readonly DataContext _context;
        readonly IImageService _imageService;
        public PlayListUserController(DataContext context, IImageService imageService) {
            _context = context;
            _imageService = imageService;
        }

        [HttpPost("get-artis-play-list")]
        [Authorize]
        public async Task<IActionResult>GetArtisList([FromBody]UserPlsPageDto userPlsPageDto)
        {
            if (userPlsPageDto.isUserUpload != 1) return BadRequest("Your are not artis");
            var validFilter = new PaginationFilter(userPlsPageDto.PageNumber, userPlsPageDto.PageSize);
            var pagedData = await _context.PlaylistUsers.Where(x => x.OwnerPl == userPlsPageDto.userName && x.isUserUpload == 1).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
            var TotalRecords = await _context.PlaylistUsers.CountAsync();
            return Ok(new PagedResponse<List<PlaylistUser>>(pagedData, validFilter.PageNumber, validFilter.PageSize, TotalRecords));
        }

        [HttpPost("get-play-list-user-add")]
        [Authorize]
        public async Task<IActionResult>GetUserListAdd([FromBody]UserPlsPageDto userPlsPageDto)
        {
            var validFilter = new PaginationFilter(userPlsPageDto.PageNumber, userPlsPageDto.PageSize);
            var pagedData = await _context.PlaylistUsers.Where(x => x.OwnerPl == userPlsPageDto.userName && x.isUserUpload == 0).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
            var TotalRecords = await _context.PlaylistUsers.CountAsync();
            return Ok(new PagedResponse<List<PlaylistUser>>(pagedData, validFilter.PageNumber, validFilter.PageSize, TotalRecords));
        }

        [HttpPost("get-playlist-by-status")]
        public async Task<IActionResult>GetListByStatus([FromBody]UserPlsPageDto userPlsPageDto)
        {
            var validFilter = new PaginationFilter(userPlsPageDto.PageNumber, userPlsPageDto.PageSize);
            var pagedData = await _context.PlaylistUsers.Where(x => x.Status == "2").Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
            var TotalRecords = await _context.PlaylistUsers.CountAsync();
            return Ok(new PagedResponse<List<PlaylistUser>>(pagedData, validFilter.PageNumber, validFilter.PageSize, TotalRecords));
        }

        [HttpPost("search-playlist")]
        public async Task<IActionResult>SearchPlayList([FromBody]SearchRequest searchReqeust)
        {
            var validFilter = new PaginationFilter(searchReqeust.PageNumber, searchReqeust.PageSize);
            var pagedData = await _context.PlaylistUsers.Where(x => x.Status == "2" && x.PLayListName.Contains(searchReqeust.KeySearch)).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
            var TotalRecords = await _context.PlaylistUsers.CountAsync();
            return Ok(new PagedResponse<List<PlaylistUser>>(pagedData, validFilter.PageNumber, validFilter.PageSize, TotalRecords));
        }

        [HttpPost("get-playlist-user-by-code")]
        public async Task<ActionResult<PlaylistUserDto>> getPlayListByCode([FromBody]RequestByCode requestByCode)
        {
            try
            {
                var playList = await _context.PlaylistUsers.FirstOrDefaultAsync(x => x.PlayListUserCode == requestByCode.PlayListUserCode && x.Status == "2");
                if (playList == null) return BadRequest("Cannot find playlist");

                var musicPlayListUser = await _context.MusicPlaylistUsers.Where(x => x.PlayListUserCode == playList.PlayListUserCode).ToListAsync();
                
                List<Music> musics = new List<Music>();
                for(var i = 0; i < musicPlayListUser.Count; i++)
                {
                    var music = await _context.Musics.FirstOrDefaultAsync(x => x.MusicCode == musicPlayListUser[i].MusicCode);
                    musics.Add(music);
                }

                PlaylistUserDto playlistUserDto = new PlaylistUserDto
                {
                    PLayListName = playList.PLayListName,
                    OwnerPl = playList.OwnerPl,
                    ImageUrlFolder = playList.ImageUrlFolder,
                    PlaylistType = playList.PlaylistType,
                    Musics = musics
                };

                return Ok(playlistUserDto);
            }
            catch(Exception e)
            {
                StatusCode(500, $"Error: {e}");
                throw;
            }
        }

        [HttpPost("new-play-list-user")]
        [Authorize]
        public async Task<ActionResult<PlaylistUserDto>> NewPlsUser([FromBody]PlaylistUserDto playlistUserDto, [FromHeader]FormFile formFile)
        {
            try
            {
                if (await CheckExistPlaylist(playlistUserDto.PlayListUserCode)) return BadRequest("The playlist has been create");

                var playlistUser = new PlaylistUser();

                if (playlistUserDto.PlayListUserCode != null)
                {
                    playlistUser.PlayListUserCode = playlistUserDto.PlayListUserCode;
                    playlistUser.ImageUrlFolder = playlistUserDto.ImageUrlFolder;
                    playlistUser.CreatedDate = DateTime.Now;
                }
                else
                {
                    var file = await _imageService.AddPhotoAsync(formFile);

                    if (file.Error != null) return BadRequest(file.Error.Message);

                    playlistUser.PlayListUserCode = Guid.NewGuid().ToString();
                    playlistUser.ImageUrlFolder = file.SecureUrl.AbsoluteUri;
                    playlistUser.CreatedDate = DateTime.Now;
                }

                playlistUser.OwnerPl = playlistUserDto.OwnerPl;
                playlistUser.CreateBy = playlistUserDto.CreateBy;
                playlistUser.TotalListon = playlistUserDto.TotalListon;
                playlistUser.TotalSong = playlistUserDto.TotalSong;
                playlistUser.Rating = playlistUserDto.Rating;
                playlistUser.isUserUpload = playlistUserDto.isUserUpload;
                playlistUser.PlaylistType = playlistUserDto.PlaylistType;
                playlistUser.Status = "0";
                if (playlistUserDto.isUserUpload == 1)
                {
                    int result = DateTime.Compare(DateTime.Now, playlistUserDto.ReleaseDatePls);
                    if (result < 0)
                    {
                        playlistUser.Status = "1";
                    }
                }
                _context.PlaylistUsers.Add(playlistUser);
                await _context.SaveChangesAsync();

                foreach(var music in playlistUserDto.Musics)
                {
                    var musicPlayListUser = new MusicPlaylistUser
                    {
                        PlayListUserCode = playlistUser.PlayListUserCode,
                        MusicCode = music.MusicCode,
                    };
                    _context.MusicPlaylistUsers.Add(musicPlayListUser);
                    await _context.SaveChangesAsync();
                }

                return Ok(playlistUserDto);
            }
            catch (Exception e)
            {
                StatusCode(500, $"Error: {e}");
                throw;
            }
        }

        [HttpPost("update-play-list-user")]
        [Authorize]
        public async Task<ActionResult<PlaylistUserDto>> UpdatePlsUser([FromBody]PlaylistUserDto playlistUserDto, [FromHeader]FormFile formFile)
        {
            try
            {
                var playlistUser = await _context.PlaylistUsers.FirstOrDefaultAsync(x => x.PlayListUserCode == playlistUserDto.PlayListUserCode);

                playlistUser.PlayListUserCode = playlistUserDto.PlayListUserCode;
                playlistUser.OwnerPl = playlistUserDto.OwnerPl;
                playlistUser.CreateBy = playlistUserDto.CreateBy;
                playlistUser.CreatedDate = playlistUserDto.CreatedDate;
                playlistUser.TotalListon = playlistUserDto.TotalListon;
                playlistUser.TotalSong = playlistUserDto.TotalSong;
                playlistUser.Rating = playlistUserDto.Rating;
                playlistUser.isUserUpload = playlistUserDto.isUserUpload;
                playlistUser.PlaylistType = playlistUserDto.PlaylistType;
                playlistUser.Status = playlistUserDto.Status;
                /**
                Status
                "0" = wait approve
                "1" = wait to release
                "2" = Approve || Release
                "3" = reject approve
                **/
                if (playlistUserDto.isUserUpload == 1 && playlistUserDto.Status == "0")
                {
                    int result = DateTime.Compare(DateTime.Now, playlistUserDto.ReleaseDatePls);
                    if (result < 0)
                    {
                        playlistUser.Status = "1"; // Wait to release
                    }
                }
                playlistUser.ImageUrlFolder = playlistUserDto.ImageUrlFolder;

                if (formFile != null) {
                    var file = await _imageService.AddPhotoAsync(formFile);
                    playlistUser.ImageUrlFolder = file.SecureUrl.AbsoluteUri;

                    if (file.Error != null) return BadRequest(file.Error.Message);
                }
                _context.PlaylistUsers.Update(playlistUser);
                await _context.SaveChangesAsync();
                

                if (playlistUserDto.Musics.Count > 0)
                {
                    foreach(var music in playlistUserDto.Musics)
                    {
                        var musicPlayListUser = await _context.MusicPlaylistUsers.FirstOrDefaultAsync(x => x.MusicCode == music.MusicCode);
                        if (musicPlayListUser == null)
                        {
                            MusicPlaylistUser ms = new MusicPlaylistUser
                            {
                               PlayListUserCode = playlistUser.PlayListUserCode,
                               MusicCode = music.MusicCode,
                            };
                        _context.MusicPlaylistUsers.Add(musicPlayListUser);
                        await _context.SaveChangesAsync();
                        }
                    }
                }

                return Ok(playlistUserDto);
            }
            catch (Exception e)
            {
                StatusCode(500, $"Error: {e}");
                throw;
            }
        }

        [HttpPost("update-playlist-status")]
        [Authorize]
        public async Task<ActionResult<PlaylistUserDto>> UpdatePLayListStatus([FromBody]MusicStatus music)
        {
            try
            {
                var playlistUser = await _context.PlaylistUsers.FirstOrDefaultAsync(x => x.PlayListUserCode == music.PlaylistUserCode);

                if (playlistUser == null) return BadRequest("Cannot find playlist");

                music.Status = music.StatusChange;

                _context.PlaylistUsers.Update(playlistUser);
                await _context.SaveChangesAsync();
                return Ok(playlistUser);
            }
            catch(Exception e)
            {
                StatusCode(500, $"Error: {e}");
                throw;
            }
        }

        [HttpPost("delete-playlist")]
        public async Task<ActionResult<PlaylistUserDto>>DeletePLayList([FromBody]ListenMusic listenMusic)
        {
            try
            {
                var list = await _context.PlaylistUsers.FirstOrDefaultAsync(x=> x.PlayListUserCode == listenMusic.PlayListCode);

                if (list == null) return BadRequest("Delete failure");

                var musicPlayList = await _context.MusicPlaylistUsers.Where(x => x.PlayListUserCode == listenMusic.PlayListCode).ToListAsync();

                _context.PlaylistUsers.Remove(list);
                await _context.SaveChangesAsync();
                foreach(var ms in musicPlayList)
                {
                    _context.MusicPlaylistUsers.Remove(ms);
                    await _context.SaveChangesAsync();
                }
                return Ok(true);
            }
            catch(Exception e)
            {
                StatusCode(500, $"Error: {e}");
                throw;
            }
        }

        protected async Task<bool> CheckExistPlaylist(string PlayListUserCode) {
            var check = await _context.PlaylistUsers.FirstOrDefaultAsync(x => x.PlayListUserCode == PlayListUserCode);

            if (check == null) {
                return true;
            }

            return false;
        }
    }
}