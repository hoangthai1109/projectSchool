using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class  BuggyController: baseApiController
    {
        private readonly DataContext _context;
        public BuggyController(DataContext context) {
            _context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> getSecret() {
            return "secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> getNotFound() {
            var thing = _context.appUsers.Find(-1);

            if (thing == null) return NotFound();

            return thing;
        }

        [HttpGet("server-error")]
        public ActionResult<string> getServerError() {
            try {
                var thing = _context.appUsers.Find(-1);
                
                var thingToReturn = thing.ToString();
                
                return thingToReturn;
            } catch (Exception ex) 
            {
                return StatusCode(500, "Server Error");
            }
        }

        [HttpGet("bad-request")]
        public ActionResult<string> getBadRequest() {
            return BadRequest("this was not a good request");
        }
    }
}