using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites.Order_Aggregate
{
    public class Order :BaseEntity
    {
        //ctor for EF to understand this class will be a table in database 
        public Order()
        {
            
        }
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subtotal,string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
          //  DeliveryMethodId = deliveryMethodId;
          
            DeliveryMethod = deliveryMethod;
            Items = items;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
        }

        public string BuyerEmail { get; set; }
        //use date tmie off set 
        // عشان تغير الساعات بالنسة لكل دوله
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Address ShippingAddress { get; set; }

        public int DeliveryMethodId  { get; set; } //Forigen Key
        public DeliveryMethod DeliveryMethod { get; set; }// NAvigational Property[ONE]

        public ICollection<OrderItem> Items { get; set;} = new HashSet<OrderItem>();// NAvigational Property[Many]

        public decimal Subtotal { get; set; }
       /* [NotMapped]
        public decimal Total { get => Subtotal + DeliveryMethod.Cost; }
*/
        public decimal GetTotal() => Subtotal + DeliveryMethod.Cost;

        public string PaymentIntentId { get; set; }
    }
}
