using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace Repository.Maps
{
    public class GuiaMap : IEntityTypeConfiguration<Guia>
    {
        public void Configure (EntityTypeBuilder<Guia> builder)
        {
            builder.ToTable ("GUIA");
            
            builder.HasKey(c => c.Id);
            
            builder.Property(g => g.Id)
                .HasColumnName("GUIA_ID")
                .HasColumnType("numeric(18,0)");
            
            builder.Property(g => g.LoteId)
                .HasColumnName("LOTE_ID")
                .HasColumnType("numeric(18,0)");

            builder.OwnsOne(g => g.Prestador, prestador => { 
                prestador.Property(p => p.Id)
                    .IsRequired()
                    .HasColumnName("PRESTADOR_ID")
                    .HasColumnType("varchar(50)"); 
                prestador.Property(p => p.LoginId)
                    .IsRequired(false)
                    .HasColumnName("PRESTADOR_LOGIN_ID");
            });

            builder.OwnsOne(g => g.Unidade)
                .Property(p => p.Id)
                .HasColumnName("PRESTADOR_UNIDADE_ID")
                .HasColumnType("varchar(50)");
            
            builder.Property(g => g.PushId)
                .HasColumnName("GUIA_TOKEN")
                .HasColumnType("varchar(50)");
            
            builder.OwnsOne(g => g.GuiaNumero, guia => {
                guia.Property(n => n.Numero)
                    .IsRequired()
                    .HasColumnName("GUIA_NUMERO")
                    .HasColumnType("varchar(50)");
                
                guia.Property(n => n.NumeroOperadora)
                    .IsRequired()
                    .HasColumnName("GUIA_NUMERO_OPERADORA")
                    .HasColumnType("varchar(50)");
            });
            
            builder.Property(g => g.GuiaTipoFK)
                .IsRequired()
                .HasColumnName("GUIA_TIPO_ID");
            
            builder.Property(g => g.GuiaStatusFK)
                .IsRequired()
                .HasColumnName("GUIA_STATUS_ID");
            
            builder.OwnsOne (g => g.Beneficiario, beneficiario => {
                beneficiario.Property(b => b.Nome)
                    .HasColumnName("GUIA_BENEFICIARIO")
                    .HasMaxLength(100)
                    .HasColumnType("varchar(100)")
                    .IsRequired(true);
                beneficiario.Property(b => b.Cartao).HasColumnName("GUIA_BENEFICIARIO_CARTAO")
                    .HasMaxLength(50)
                    .HasColumnType("varchar(50)")
                    .IsRequired();
            });
            
            builder.Property(g => g.StatusCheckInFK)
                .IsRequired()
                .HasColumnName("GUIA_BENEFICIARIO_CHECKIN_STATUS_ID");
            
            builder.Property(g => g.Valor)
                .IsRequired()
                .HasColumnName("GUIA_VALOR")
                .HasColumnType("decimal(18,2)");
            
            builder.Property(g => g.Data)
                .IsRequired()
                .HasColumnName("GUIA_DATA")
                .HasColumnType("date");
            
            builder.Property(g => g.GuiaXML)
                .HasColumnName("GUIA_XML")
                .HasColumnType("varchar(max)");
            
            builder.Property(g => g.Deletada)
                .IsRequired()
                .HasColumnName("GUIA_DELETADA");
            
            builder.Property(g => g.GuiaOrigemFK)
                .IsRequired()
                .HasColumnName("GUIA_ORIGEM_ID");
            
            builder.Property(g => g.TokenId)
                .HasColumnName("GUIA_BENEFICIARIO_TOKEN")
                .HasMaxLength(10)
                .HasColumnType("varchar(10)");

            /*
             * Foreign Keys
             */
            
            builder.HasOne(a => a.GuiaTipo)
                .WithMany (g => g.Guias)
                .HasForeignKey(k => k.GuiaTipoFK);

            builder.HasOne(a => a.GuiaStatus)
                .WithMany (g => g.Guias)
                .HasForeignKey(k => k.GuiaStatusFK);

            builder.HasOne(a => a.GuiaStatusCheckIns)
                .WithMany (g => g.Guias)
                .HasForeignKey(k => k.StatusCheckInFK);

             builder.HasOne(a => a.GuiaOrigem)
                .WithMany (g => g.Guias)
                .HasForeignKey(k => k.GuiaOrigemFK);
        }
    }
}