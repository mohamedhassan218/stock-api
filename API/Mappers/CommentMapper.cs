using API.Dtos.Comment;
using API.Models;

namespace API.Mappers
{
    public static class CommentMapper
    {
        // Transform Comment Obj to CommentDto Obj.
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId,
                CreatedBy = comment.UserNavigation.UserName
            };
        }

        // Transform an CreateCommentDto Obj to Comment Obj.
        public static Comment ToCommentFromCreateDto(this CreateCommentDto comment, int stockId)
        {
            return new Comment
            {
                Title = comment.Title,
                Content = comment.Content,
                StockId = stockId
            };
        }

        // Transform an UpdateCommentDto Obj to Comment Obj.
        public static Comment ToCommentFromUpdateDto(this UpdateCommentDto updateCommentDto)
        {
            return new Comment
            {
                Title = updateCommentDto.Title,
                Content = updateCommentDto.Content,
            };
        }
    }
}