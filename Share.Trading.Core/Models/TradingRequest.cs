using System.ComponentModel.DataAnnotations;

namespace Share.Trading.Domain.Entities.Models
{
    public class TradingRequest
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Shares must be at least 1")]
        public int Shares { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be positive")]
        public decimal PricePerShare { get; set; }

        [Required(ErrorMessage = "Share symbol must be provided")]
        public string Symbol { get; set; }

        [Required(ErrorMessage = "Share name must be provided")]
        public string ShareName { get; set; }
    }
}
