using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }
        /*
        [HttpGet]
        public IActionResult Get([FromBody]UserDTO user)
        {
            return Ok(_postService.GetByAuthor(user));
        }*/

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_userService.Get(id));
        }

        [HttpGet("{role}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersByRoleAsync(string role)
        {
            return Ok(await _userService.GetUsersByRoleAsync(role));
        }
    }
}