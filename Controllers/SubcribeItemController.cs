using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class SubcribeItemController:baseApiController
    {
        readonly DataContext _context;
        public SubcribeItemController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<SubcribeItem>>> GetListSubcribe() {
            var subcribeItem = await _context.SubcribeItems.ToListAsync();
            return StatusCode(200, subcribeItem);
        }

        [HttpPost("add-subcribe")]
        [Authorize]
        public async Task<ActionResult<SubcribeItemDto>> AddSubcribeAdmin([FromBody]SubcribeItemDto SubcribeItemDto) {
            try
            {
               if (await CheckExistSubcri(SubcribeItemDto.PakageDescript)) return BadRequest("Subcribe has been create");

               var subcribe = new SubcribeItem
               {
                PakageValue = SubcribeItemDto.PakageValue,
                PakageDescript = SubcribeItemDto.PakageDescript,
                PakageType = SubcribeItemDto.PakageType
               };
               _context.SubcribeItems.Add(subcribe);
               await _context.SaveChangesAsync();

               return StatusCode(200, subcribe);
            }
            catch(Exception e)
            {
                StatusCode(500, $"Error: {e}");
                throw;
            }
        }

        [HttpPost("add-subcribe-user")]
        [Authorize]
        public async Task<ActionResult<SubcribeItemDto>> AddSubcribeUser([FromBody]SubcribeItemDto SubcribeItemDto) {
            try
            {
               if (await CheckExistSubcri(SubcribeItemDto.PakageDescript)) return BadRequest("Subcribe has been create");

               var subcribe = new SubcribeItem
               {
                PakageValue = SubcribeItemDto.PakageValue,
                PakageDescript = SubcribeItemDto.PakageDescript,
                PakageType = SubcribeItemDto.PakageType
               };
               _context.SubcribeItems.Add(subcribe);
               await _context.SaveChangesAsync();

               var user = await _context.appUsers.FindAsync(SubcribeItemDto.UserId);
               user.AccountType = subcribe.PakageType == 0 ? "0" : (subcribe.PakageType == 1 ? "1" : "2");
               _context.appUsers.Update(user);
               await _context.SaveChangesAsync();

               return StatusCode(200, subcribe);
            }
            catch(Exception e)
            {
                StatusCode(500, $"Error: {e}");
                throw;
            }
        }

        [HttpPost("update-subcribe")]
        [Authorize]
        public async Task<ActionResult<SubcribeItem>> UpdateSubcribeAdmin([FromBody]SubcribeItem SubcribeItem) {
            try
            {
                var subcribe = await _context.SubcribeItems.FindAsync(SubcribeItem.Id);

                if (subcribe == null) return BadRequest("Cannot find subcribe");


               _context.SubcribeItems.Update(subcribe);
               await _context.SaveChangesAsync();

               return StatusCode(200, subcribe);
            }
            catch(Exception e)
            {
                StatusCode(500, $"Error: {e}");
                throw;
            }
        }

        protected async Task<bool> CheckExistSubcri(string PakageDescript) {
            var subcribeItem = await _context.SubcribeItems.FirstOrDefaultAsync(x => x.PakageDescript == PakageDescript);

            if (subcribeItem == null) {
                return true;
            }

            return false;
        }
    }
}