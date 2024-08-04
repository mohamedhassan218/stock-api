using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Stock
{
    public class UpdateStockDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol must not be more than 10 chatacter!")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(20, ErrorMessage = "CompanyName must not be more than 20 chatacter!")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000000000, ErrorMessage = "Purchase must be between 1 and 1000000000!")]
        public decimal Purchase { get; set; }
        [Required]
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        [Required]
        public long MarketCap { get; set; }
    }
}