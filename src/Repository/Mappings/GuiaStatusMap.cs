using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace Repository.Maps
{
    public class GuiaStatusMap : IEntityTypeConfiguration<GuiaStatus>
    {
        public void Configure (EntityTypeBuilder<GuiaStatus> builder)
        {
            builder.ToTable ("GUIA_STATUS");
            builder.Property(g => g.Id).HasColumnName("GUIA_STATUS_ID").IsRequired();
            builder.Property(g => g.Descricao).HasColumnName("GUIA_STATUS_DESCRICAO").HasMaxLength(50).HasColumnType("varchar(50)").IsRequired();
            
            builder.HasKey(g => g.Id);

            builder.HasData(
                new GuiaStatus {Id = 1, Descricao = "ABERTA"},
                new GuiaStatus {Id = 2, Descricao = "FECHADA"},
                new GuiaStatus {Id = 3, Descricao = "NÃO VALIDADA"}
            );
        }
    }
}

