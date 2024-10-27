using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entites;

namespace Talabat.ApIs.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }

        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();

    }
}
