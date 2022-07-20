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
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {            
            builder.ToTable("Carts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.Price)
                .IsRequired();

            builder.HasOne(x => x.Product)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.DeliveryMode)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.DeliveryModeId);
        }
    }
}
