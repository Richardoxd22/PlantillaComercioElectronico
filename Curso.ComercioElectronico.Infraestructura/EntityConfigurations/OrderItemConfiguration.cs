using Curso.ComercioElectronico.Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Infraestructura.EntityConfigurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.HasKey(x => x.Id);

            builder.Property(x=>x.Id)
                .HasMaxLength(30)
                .IsRequired();
            builder.Property(x => x.ProductId)
                .IsRequired();
            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId);
            builder.Property(x => x.OrderId)
                .IsRequired();

            //builder.Property(x => x.Order)
            //    .HasMaxLength(30);

            builder.Property(x => x.CantProduct)
                .IsRequired();
            
        }
    }
}
