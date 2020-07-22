using System.Threading.Tasks;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("user/{id}/role/{role}")]
        public async Task<IActionResult> AddToRoleAsync(int id, string role)
        {
            var roles = await _userService.GetUserRolesAsync(id);
            await _userService.AddToRoleAsync(id, role);
            foreach (var r in roles)
            {
                await _userService.RemoveFromRoleAsync(id, r);
            }
            await _userService.AddToRoleAsync(id, role);
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
        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult DeleteComment(int id)
        {
            _commentService.Delete(id);
            return Ok();
        }

        [HttpGet("user/role/{role}")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> GetByRole(string role) 
        {
            var usersInRole = await _userService.GetUsersByRoleAsync(role);
            return Ok(usersInRole);
        }
    }
}