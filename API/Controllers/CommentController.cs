using API.Dtos.Comment;
using API.Interfaces;
using API.Mappers;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        // GET api/comment
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();
            var commentsDto = comments.Select(c => c.ToCommentDto());
            return Ok(commentsDto);
        }

        // GET api/comment?id=x
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            return (comment == null) ? NotFound() : Ok(comment.ToCommentDto());
        }

        // POST api/comment?stockId=x
        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto createCommentDto)
        {
            if (!await _stockRepo.StockExist(stockId))
                return BadRequest("Stock doesn't exist.");
            var comment = createCommentDto.ToCommentFromCreateDto(stockId);
            await _commentRepo.CreateAsync(comment);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToCommentDto());
        }

        // PUT api/comment?id=x
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            var comment = await _commentRepo.UpdateAsync(id, updateCommentDto.ToCommentFromUpdateDto());
            if (comment != null)
                return Ok(comment.ToCommentDto());
            return NotFound("Comment not found!");
        }

        // DELETE api/comment?id=x
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var comment = await _commentRepo.DeleteAsync(id);
            if (comment != null)
                return Ok(comment);
            return NotFound("Comment doesn't exist!");
        }
    }
}