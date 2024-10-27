using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.Core.Specification.Order_spec
{
    public class OrderSpecification : BaseSpecification<Order>
    {
        // this ctor to get all order for specifec user
        public OrderSpecification(string email): base(o => o.BuyerEmail == email)
        {
            Includes.Add(o =>  o.DeliveryMethod);  
            Includes.Add(o =>  o.Items);

            AddOrderByDesc(o => o.OrderDate);  
        }

        // this ctor to get order by id
        public OrderSpecification(int id,  string email) : base(o => o.BuyerEmail == email && o.Id ==id ) 
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);

        }


    }
}
