using API.Dtos.Comment;
using API.Extensions;
using API.Interfaces;
using API.Mappers;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<User> _userManager;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo, UserManager<User> userManager)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
        }

        // GET api/comment
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _commentRepo.GetAllAsync();
            var commentsDto = comments.Select(c => c.ToCommentDto());
            return Ok(commentsDto);
        }

        // GET api/comment?id=x
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepo.GetByIdAsync(id);
            return (comment == null) ? NotFound() : Ok(comment.ToCommentDto());
        }

        // POST api/comment?stockId=x
        [HttpPost("{stockId:int}")]
        [Authorize]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto createCommentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _stockRepo.StockExist(stockId))
                return BadRequest("Stock doesn't exist.");

            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);



            var comment = createCommentDto.ToCommentFromCreateDto(stockId);
            comment.UserId = user.Id;
            await _commentRepo.CreateAsync(comment);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToCommentDto());
        }

        // PUT api/comment?id=x
        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepo.UpdateAsync(id, updateCommentDto.ToCommentFromUpdateDto());
            if (comment != null)
                return Ok(comment.ToCommentDto());
            return NotFound("Comment not found!");
        }

        // DELETE api/comment?id=x
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepo.DeleteAsync(id);
            if (comment != null)
                return Ok(comment);
            return NotFound("Comment doesn't exist!");
        }
    }
}