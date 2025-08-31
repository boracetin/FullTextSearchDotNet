using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullTextSearch.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FullTextSearch.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly Context.DemoContext _context;
        public UserController(Context.DemoContext context)
        {
            _context = context;
        }
        [HttpGet("search")]
        public IActionResult SearchUsers([FromQuery] string query)
        {
            var users = _context.Users
                .FullTextSearch(query, u => u.NormalizedName) // NormalizedName kullanılıyor
                .ToList();
            return Ok(users);
        }
        
    }
}