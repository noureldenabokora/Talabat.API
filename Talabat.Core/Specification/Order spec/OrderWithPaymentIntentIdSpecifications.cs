using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.Core.Specification.Order_spec
{
    public class OrderWithPaymentIntentIdSpecifications : BaseSpecification<Order>
    {
        public OrderWithPaymentIntentIdSpecifications(string paymentIntentId)
            : base(o => o.PaymentIntentId == paymentIntentId)
        {
            
        }
    }
}
