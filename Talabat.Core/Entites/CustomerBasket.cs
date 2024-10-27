using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites
{
    public class CustomerBasket
    {
        public string Id { get; set; }

        public List<BasketItem> Items { get; set; } = new List<BasketItem>();


        /// related with payment process
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
        
        public int? DeliveryMethodId { get; set; }

        public decimal ShippingCost { get; set; }

        public CustomerBasket(string id )
        {
            Id = id;
        }
    }
}
