using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Comment
{
    public class UpdateCommentDto
    {
        [Required]
        [MinLength(4, ErrorMessage = "Title must be at least 5 characters!")]
        [MaxLength(300, ErrorMessage = "Title must not be more than 300 characters!")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(4, ErrorMessage = "Content must be at least 5 characters!")]
        [MaxLength(500, ErrorMessage = "Content must not be more than 500 characters!")]
        public string Content { get; set; } = string.Empty;
    }
}