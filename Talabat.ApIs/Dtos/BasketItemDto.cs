using System.ComponentModel.DataAnnotations;

namespace Talabat.ApIs.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Desription { get; set; }
        [Required]
        [Range(1, int.MaxValue,ErrorMessage = "Quantity Must be one item at least!")]
        public int Quantity { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price Must be greater than zero!")]
        public decimal Price { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }

    }
}