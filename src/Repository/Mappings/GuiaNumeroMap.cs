using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace Repository.Maps
{
    public class GuiaNumeroMap : IEntityTypeConfiguration<GuiaNumero>
    {
        public void Configure (EntityTypeBuilder<GuiaNumero> builder)
        {
            builder.ToTable ("GUIA_NUMERO");
            builder.Property(g => g.CodigoPrestador).HasColumnName("CD_PRESTADOR").HasMaxLength(50).HasColumnType("varchar(50)").IsRequired();
            builder.Property(g => g.Numero).HasColumnName("NR_GUIA_NUMERO").HasMaxLength(50).HasColumnType("int").IsRequired();
           
            builder.HasKey(o => new { o.CodigoPrestador });
        }
    }
}