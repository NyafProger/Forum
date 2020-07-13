using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Entities;
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

        //api/user/identify
        [HttpGet("identify")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var user = await _userService.IdentifyUserAsync(User);
            return Ok(user);
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
        //api/user/role/
        [HttpGet("role/{role}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersByRoleAsync(string role)
        {
            return Ok(await _userService.GetUsersByRoleAsync(role));
        }
    }
}