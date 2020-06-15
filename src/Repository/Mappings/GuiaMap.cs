using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace Repository.Maps
{
    public class GuiaMap : IEntityTypeConfiguration<Guia>
    {
        public void Configure (EntityTypeBuilder<Guia> builder)
        {
            builder.ToTable("Guia");
            
            builder.OwnsOne(c => c.Prestador).Property(p => p.Id).HasColumnName("PrestadorId").HasMaxLength(10).HasColumnType("varchar(10)").IsRequired();
            builder.OwnsOne(c => c.Unidade).Property(p => p.Id).HasColumnName("UnidadeId").IsRequired();
            builder.Property(g => g.PushId).HasMaxLength(10).HasColumnType("varchar(10)");
            builder.Property(g => g.TokenId).HasMaxLength(10).HasColumnType("varchar(10)");
            builder.Property(g => g.Numero).HasMaxLength(50).HasColumnType("varchar(50)").IsRequired();
            builder.OwnsOne(g => g.Beneficiario).Property(p => p.Nome).HasColumnName("Beneficiario").HasMaxLength(100).HasColumnType("varchar(100)").IsRequired();
            builder.OwnsOne(g => g.Beneficiario).Property(p => p.Cartao).HasColumnName("BeneficiarioCartao").HasMaxLength(50).HasColumnType("varchar(50)").IsRequired();
            builder.Property(g => g.Valor).HasColumnType("money").IsRequired();
            builder.Property(g => g.GuiaXML).HasColumnType("varchar(max)");
            builder.Property(g => g.IdGuiaExterno).IsRequired();
            
            builder.HasKey  (c => c.Id);
            builder.HasOne(a => a.GuiaStatus)
                .WithMany(g => g.Guias)
                .HasForeignKey(k => k.GuiaStatusId);

            builder.HasOne(a => a.GuiaTipo)
                .WithMany(g => g.Guias)
                .HasForeignKey(k => k.GuiaTipoId);

            builder.HasOne(a => a.GuiaStatusCheckIns)
                .WithMany(g => g.Guias)
                .HasForeignKey(k => k.StatusCheckInId);
        }
    }
}