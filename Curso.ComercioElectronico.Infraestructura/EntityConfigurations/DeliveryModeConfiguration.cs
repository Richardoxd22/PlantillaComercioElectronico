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
    public class DeliveryModeConfiguration: IEntityTypeConfiguration<DeliveryMode>
    {        
        public void Configure(EntityTypeBuilder<DeliveryMode> builder)
        {
            builder.ToTable("DeliveryModes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasMaxLength(30)
                .IsRequired();
            builder.Property(x => x.Description)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.IsDeleted).IsRequired();
        }
    }
}
