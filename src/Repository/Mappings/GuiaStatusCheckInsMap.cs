using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Repository.Maps
{
    public class GuiaStatusCheckInsMap : IEntityTypeConfiguration<GuiaStatusCheckIns>
    {
        public void Configure (EntityTypeBuilder<GuiaStatusCheckIns> builder)
        {
            builder.ToTable ("BENEFICIARIO_CHECKIN_STATUS");
            builder.Property(g => g.Id).HasColumnName("BENEFICIARIO_CHECKIN_ID").IsRequired();
            builder.Property(g => g.Descricao).HasColumnName("BENEFICIARIO_CHECKIN_DESCRICAO").HasMaxLength(50).HasColumnType("varchar(50)").IsRequired();

            builder.HasKey(g => g.Id);

            builder.HasData(
                new GuiaStatusCheckIns {Id = 1, Descricao = "VALIDADO"},
                new GuiaStatusCheckIns {Id = 2, Descricao = "NÃO VALIDADO"},
                new GuiaStatusCheckIns {Id = 3, Descricao = "NÃO RESPONDEU A TEMPO - TIMEOUT"},
                new GuiaStatusCheckIns {Id = 4, Descricao = "ATENDIMENTO MENOR - ACOMPANHANTE SEM PLANO"}
            );
        }
    }
}