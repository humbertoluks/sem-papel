using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Repository.Maps
{
    public class GuiaOrigemMap : IEntityTypeConfiguration<GuiaOrigem>
    {
        public void Configure (EntityTypeBuilder<GuiaOrigem> builder)
        {
            builder.ToTable ("GUIA_ORIGEM");
            builder.Property(g => g.Id).HasColumnName("GUIA_ORIGEM_ID").IsRequired();
            builder.Property(g => g.Descricao).HasColumnName("GUIA_ORIGEM_DESCRICAO").HasMaxLength(50).HasColumnType("varchar(50)").IsRequired();
            
            builder.HasKey(g => g.Id);

            builder.HasData(
                new GuiaOrigem {Id = 1, Descricao = "URL"},
                new GuiaOrigem {Id = 2, Descricao = "PORTAL"}
            );
        }
    }
}


