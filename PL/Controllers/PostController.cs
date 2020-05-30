using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private IPostService _postService;
        private IUserService _userService;
        public PostController(IPostService postService,
                              IUserService userService)
        {
            _postService = postService;
            _userService = userService;
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


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostAsync([FromBody]PostDTO post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model");
            }
            var user = await _userService.IdentifyUserAsync(User);
            post.Author.Id = user.Id;
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
            if(user.Id == post.Author.Id)
            {
                _postService.Update(post);
                return Ok();
            }
            return Forbid();
        }
    }
}