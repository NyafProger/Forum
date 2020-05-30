using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;
        private IUserService _userService;

        public AccountController(IAccountService accountService,
                                 IUserService userService)
        {
            _accountService = accountService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterModel regModel)
        {
            if (ModelState.IsValid)
            {
                var userCheck = _userService.GetByName(regModel.UserName);
                if (userCheck != null)
                {
                    return Conflict($"Name { regModel.UserName} is already taken.");
                }

                var emailCheck = _userService.GetByEmail(regModel.Email);
                if (emailCheck != null)
                {
                    return Conflict($"Account with this email {regModel.Email} already created.");
                }

                var regUser = await _accountService.RegisterAsync(regModel);
                if (regUser != null)
                {
                    return Created($"/user/{regUser.Id}", regUser);
                }
                return BadRequest("");
            }
            return BadRequest("Invalid model");
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody]LogInModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model");
            }
            var token = await _accountService.LogInAsync(model);
            if (token != null)
            {
                var loggedUser = _userService.GetByEmail(model.Email);
                //var userRoles = await _roleService.GetUserRoles(loggedUser);
                return Ok(new
                {
                    userId = loggedUser.Id,
                    //roles = userRoles,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized("Wrong username or password");
        }
    }
}