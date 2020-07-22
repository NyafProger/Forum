using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;

        public PostController(IPostService postService,
                              IUserService userService,
                              ICommentService commentService)
        {
            _postService = postService;
            _userService = userService;
            _commentService = commentService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_postService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_postService.Get(id));
        }

        [HttpGet("{id}/comments")]
        public IActionResult GetCommets(int id) 
        {
            var comments = _commentService.GetByPost(id);
            return Ok(comments);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostAsync([FromBody]PostDTO post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model");
            }
            var user = await _userService.IdentifyUserAsync(User);
            post.AuthorId = user.Id;
            var createdPost = _postService.Add(post);
            return Created($"api/Post/{createdPost.Id}", createdPost);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var user = await _userService.IdentifyUserAsync(User);
            var post = _postService.Get(id);
            if (user.Id == post.Author.Id)
            {
                _postService.Delete(id);
                return Ok();
            }
            return Forbid();
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody]PostDTO post)
        {
            var user = await _userService.IdentifyUserAsync(User);
            if(user.Id == post.AuthorId)
            {
                _postService.Update(post);
                return Ok();
            }
            return Forbid();
        }
    }
}