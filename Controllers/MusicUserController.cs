using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.BodyRequest;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite.Storage.Internal;

namespace API.Controllers
{
    public class MusicUserController:baseApiController
    {
        readonly DataContext _context;
        public MusicUserController(DataContext context) {
            _context = context;
        }

        [HttpPost("get-user-music-list")]
        public async Task<IActionResult> GetUserMusicList([FromBody]SearchRequest searchRequest)
        {
            var validFilter = new PaginationFilter(searchRequest.PageNumber, searchRequest.PageSize);
            var pagedData = await _context.MusicUsers.Where(x => x.MadeBy == searchRequest.CreateBy).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
            var TotalRecords = await _context.MusicUsers.CountAsync();
            return Ok(new PagedResponse<List<MusicUser>>(pagedData, validFilter.PageNumber, validFilter.PageSize, TotalRecords));
        }

        [HttpPost("Add-user-music-list")]
        public async Task<ActionResult<musicUserDto>> AddUserMusicList([FromBody]musicUserDto musicUserDto)
        {
            try
            {
                if (await CheckExistMusic(musicUserDto.MusicUserCode)) return BadRequest("This music you already add! Pls check your music list");

                var musicUser = new MusicUser
                {
                    MusicUserCode = Guid.NewGuid().ToString(),
                    MadeBy = musicUserDto.MadeBy,
                    CreatedDate = DateTime.Now,
                };
                _context.MusicUsers.Add(musicUser);
                await _context.SaveChangesAsync();

                foreach(var musics in musicUserDto.Musics)
                {
                    var musicForUser = new MusicForUser
                    {
                        MusicCode = musics.MusicCode,
                        MusicUserCode = musicUser.MusicUserCode,
                    };
                    _context.MusicForUsers.Add(musicForUser);
                    await _context.SaveChangesAsync();
                }

                return Ok(musicUserDto);

            }
            catch(Exception e)
            {
                StatusCode(500, $"Error: {e}");
                throw;
            }
        }

        [HttpPost("Update-user-music-list")]
        public async Task<ActionResult<musicUserDto>> UpdateUserMusicList([FromBody]musicUserDto musicUserDto)
        {
            try
            {
                var musicUser = await _context.MusicUsers.FirstOrDefaultAsync(x => x.MusicUserCode == musicUserDto.MusicUserCode);
                if (musicUser == null) return BadRequest("This music you already add! Pls check your music list");

                
                if (musicUserDto.Musics.Count > 0)
                {
                    foreach(var music in musicUserDto.Musics)
                    {
                        var musicForUser= await _context.MusicForUsers.FirstOrDefaultAsync(x => x.MusicCode == music.MusicCode);
                        if (musicForUser == null)
                        {
                            MusicForUser ms = new MusicForUser
                            {
                               MusicUserCode = musicUser.MusicUserCode,
                               MusicCode = music.MusicCode,
                            };
                        _context.MusicForUsers.Add(musicForUser);
                        await _context.SaveChangesAsync();
                        }
                    }
                }

                return Ok(musicUserDto);

            }
            catch(Exception e)
            {
                StatusCode(500, $"Error: {e}");
                throw;
            }
        }

        [HttpPost("Delete-user-music-list")]
        public async Task<ActionResult<musicUserDto>> DeleteUserMusicList([FromBody]MusicForUserRequest musicForUserRequest)
        {
            try
            {
                var musicUser = await _context.MusicUsers.FirstOrDefaultAsync(x=> x.MusicUserCode == musicForUserRequest.MusicUserCode);

                if (musicUser == null) return BadRequest("Delete failure");

                var musicPlayList = await _context.MusicForUsers.Where(x => x.MusicUserCode == musicUser.MusicUserCode).ToListAsync();

                _context.MusicUsers.Remove(musicUser);
                await _context.SaveChangesAsync();
                foreach(var ms in musicPlayList)
                {
                    _context.MusicForUsers.Remove(ms);
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

        protected async Task<bool> CheckExistMusic(string musicUserCode) {
            var check = await _context.PlaylistUsers.FirstOrDefaultAsync(x => x.PlayListUserCode == musicUserCode);

            if (check == null) {
                return true;
            }

            return false;
        }
    }
}