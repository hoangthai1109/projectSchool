using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.BodyRequest;
using API.Data;
using API.Entities;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class SortListController:baseApiController
    {
        readonly DataContext _context;
        public SortListController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("top-hit-day-50")]
        public async Task<IActionResult>TopHitDayTop50()
        {
            var inDay = DateTime.Today.AddDays(-1);
            var music = await _context.Musics.Where(x => x.CreateDate <= inDay).Take(50).OrderByDescending(x => x.CreateDate.Date).ToListAsync();

            return Ok(music);
        }

        [HttpPost("top-playlist-listen")]
        public async Task<IActionResult>TopListenPlaylist()
        {
            var playlistUsers = await _context.PlaylistUsers.Take(50).OrderByDescending(x => x.TotalListon).ToListAsync();

            return Ok(playlistUsers);
        }

        [HttpPost("top-song-listen")]
        public async Task<IActionResult>TopListenSong()
        {
            var music = await _context.Musics.Take(50).OrderByDescending(x => x.TotalListen).ToListAsync();

            return Ok(music);
        }

        [HttpPost("top-rating-playlist")]
        public async Task<IActionResult>TopRatingPlayList()
        {
            var playlistUsers = await _context.PlaylistUsers.Where(x => x.Rating >= 4).Take(50).OrderBy(x => x.Rating).OrderByDescending(x => x.TotalListon).ToListAsync();

            return Ok(playlistUsers);
        }
    }
}