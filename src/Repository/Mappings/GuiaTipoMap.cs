using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace Repository.Maps
{
    public class GuiaTipoMap : IEntityTypeConfiguration<GuiaTipo>
    {
        public void Configure (EntityTypeBuilder<GuiaTipo> builder)
        {
            builder.ToTable ("GUIA_TIPO");
            builder.Property(g => g.Id).HasColumnName("GUIA_TIPO_ID").IsRequired();
            builder.Property(g => g.Descricao).HasColumnName("GUIA_TIPO_DESCRICAO").HasMaxLength(50).HasColumnType("varchar(50)").IsRequired();
            builder.Property(g => g.Local).HasColumnName("GUIA_TIPO_LOCAL");

            builder.HasKey(g => g.Id);

            builder.HasData(
                new GuiaTipo {Id = 1, Descricao = "SP-SATD", Local = 0},
                new GuiaTipo {Id = 2, Descricao = "RESUMO INTERNAÇÃO", Local = 0},
                new GuiaTipo {Id = 3, Descricao = "HONORÁRIOS", Local = 0},
                new GuiaTipo {Id = 4, Descricao = "CONSULTA", Local = 0},
                new GuiaTipo {Id = 5, Descricao = "ODONTOLOGIA", Local = 0}
            );
        }
    }
}