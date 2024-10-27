using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.Repository.Data.Configuration
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, ShippingAddress => ShippingAddress.WithOwner());

            // هنا بخليه يحفظ فالداتا بيز الحاله علي هيئة استرينج
            // بس لما يرجعها هيرجعهالي لاصلها اللي هو اوردر ستاتيس 
            builder.Property(o => o.Status)
                .HasConversion(
                OStauts => OStauts.ToString(),
                OStauts => (OrderStatus) Enum.Parse(typeof(OrderStatus), OStauts)
                );

            //عشان احدد داتا تايب بتاعه ل decimal
            builder.Property(o => o.Subtotal)
                .HasColumnType("decimal(18,2)");
        }
    }
}
