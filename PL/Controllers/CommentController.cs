using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentService _commentService;
        private IUserService _userService;

        public CommentController(ICommentService commentService,
                                 IUserService userService)
        {
            _commentService = commentService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<CommentDTO> comments = _commentService.GetAll();
            return Ok(comments);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostAsync([FromBody]CommentDTO comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model");
            }
            var user = await _userService.IdentifyUserAsync(User);
            comment.AuthorId = user.Id;
            var createdComment = _commentService.Add(comment);
            return Created($"api/Comment/{createdComment.Id}", createdComment);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            CommentDTO comment = _commentService.Get(id);
            return Ok(comment);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var user = await _userService.IdentifyUserAsync(User);
            var comment = _commentService.Get(id);
            if (user.Id == comment.AuthorId)
            {
                _commentService.Delete(id);
                return Ok();
            }
            return Forbid();
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromBody]CommentDTO comment)
        {
            var user = await _userService.IdentifyUserAsync(User);
            if (user.Id == comment.AuthorId)
            {
                _commentService.Update(comment);
                return Ok();
            }
            return Forbid();
        }
    }
}