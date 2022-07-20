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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {        
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.Id)
                .HasMaxLength(30)
                .IsRequired();
            builder.HasOne(x => x.DeliveryMode)
                .WithMany()
                .HasForeignKey(x => x.DeliveryModeId);            
        }
    }
}
