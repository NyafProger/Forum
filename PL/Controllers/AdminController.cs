using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IPostService _postService;
        private IUserService _userService;
        private ICommentService _commentService;
        public AdminController(IUserService userService,
                               IPostService postService,
                               ICommentService commentService)
        {
            _postService = postService;
            _userService = userService;
            _commentService = commentService;
        }
        [HttpPut("role/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddToRoleAsync([FromBody]string role, int id)
        {
            var user = _userService.Get(id);
            var roles = await _userService.GetUserRolesAsync(user);
            foreach (var r in roles)
            {
                await _userService.RemoveFromRoleAsync(user, r);
            }
            await _userService.AddToRoleAsync(user, role);
            return Ok();
        }

        [HttpDelete("post/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeletePost(int id)
        {
            _postService.Delete(id);
            return Ok();
        }

        [HttpDelete("comment/{id}")]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Moderator")]
        public IActionResult DeleteComment(int id)
        {
            _commentService.Delete(id);
            return Ok();
        }
    }
}