using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace Repository.Maps
{
    public class StudentMap : IEntityTypeConfiguration<Student>
    {
        public void Configure (EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");

            builder.HasKey  (c => c.Id);
            builder.OwnsOne (c => c.Document).Property(p => p.Number).HasColumnName("CPF").IsRequired().HasMaxLength(11).HasColumnType("varchar(11)");
            builder.OwnsOne (c => c.Email).Property(p => p.Address).HasColumnName("Email").IsRequired().HasMaxLength(50).HasColumnType("varchar(50)");
            builder.OwnsOne (c => c.Name).Property(x => x.Firstname).IsRequired().HasMaxLength(50).HasColumnType("varchar(50)");
            builder.OwnsOne (c => c.Name).Property(x => x.Lastname).IsRequired().HasMaxLength(120).HasColumnType("varchar(120)");
        }
    }
}