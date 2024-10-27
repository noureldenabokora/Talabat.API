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
    internal class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            //دي عشان اقوله ادخل جوه البرودكت ده وهات البروب اللي جواه 
            // واحفظها عندك كعموايد 
            builder.OwnsOne(orderItem => orderItem.product, product => product.WithOwner());

            // عشان احدد الداتا تايب بتاعه ل decimal
            builder.Property(orderItem => orderItem.Price)
                .HasColumnType("decimal(18,2)");


        }
    }
}
