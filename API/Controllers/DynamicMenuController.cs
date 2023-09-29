using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class DynamicMenuController: baseApiController
    {
        readonly DataContext _context;

        public DynamicMenuController(DataContext context) {
            _context = context;
        }

        [HttpPost("get-list-menu")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DynamicMenu>>> GetMenuList() {
            return await _context.dynamicMenu.ToListAsync();
        }

        [HttpPost("get-list-menu/{id}")]
        [Authorize]
        public async Task<ActionResult<DynamicMenu>> GetMenu(int id) {
            var dynamicMenu = await _context.dynamicMenu.FindAsync(id);

            if (dynamicMenu == null) return BadRequest("Cannot find menu");
            

            return dynamicMenu;
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<ActionResult<DynamicMenu>> AddNewMenu(DynamicMenu dynamic) {
            if (await this.CheckExistMenu(dynamic.id, dynamic.idParent, dynamic.role)) return BadRequest("Add new menu fail!");

            _context.dynamicMenu.Add(dynamic);
            await _context.SaveChangesAsync();

            return dynamic;
        }

        protected async Task<bool> CheckExistMenu(int id, int parentId, byte[] role) {
            var dynamicMenu = await _context.dynamicMenu.FirstOrDefaultAsync(x => x.id == id);

            if (dynamicMenu == null && dynamicMenu.idParent == parentId && dynamicMenu.role == role) {
                return true;
            }

            return false;
        }
    }
}