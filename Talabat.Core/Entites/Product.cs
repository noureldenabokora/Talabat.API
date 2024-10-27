using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        
        public int ProductBrandId { get; set; } // Forigen Key 

        public ProductBrand productBrand { get; set; } // Navigational property [ONE]

        public int ProductTypeId { get; set; } // Forigen Key 
        public ProductType productType { get; set; }// Navigational property [ONE]
    }
}
