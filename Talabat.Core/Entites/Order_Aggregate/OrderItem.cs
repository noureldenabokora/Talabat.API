using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites.Order_Aggregate
{
    public class OrderItem :BaseEntity
    {
        //ctor for EF to understand this class will be a table in database 
        public OrderItem()
        {
            
        }
        public OrderItem(ProductItemOrdered product, decimal price, int quantity)
        {
            this.product = product;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrdered product {get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }



    }
}
