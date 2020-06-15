using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Maps;

namespace Repository.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Guia> Guias { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("AtendimentoSemPapel");
            builder.ApplyConfiguration(new GuiaMap());
            
            //builder.Ignore<>();
            //base.OnModelCreating(builder);
        }
    }
}