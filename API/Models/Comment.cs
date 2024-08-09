using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? StockId { get; set; }
        public Stock? Stock { get; set; }
        public string UserId { get; set; }   // Should be string as the Id in the IdentityUser table.
        public User? UserNavigation { get; set; }
    }
}