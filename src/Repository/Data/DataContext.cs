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
            builder.HasDefaultSchema("ATENDIMENTO");
            builder.ApplyConfiguration(new GuiaMap());
            builder.ApplyConfiguration(new GuiaOrigemMap());
            builder.ApplyConfiguration(new GuiaStatusCheckInsMap());
            builder.ApplyConfiguration(new GuiaStatusMap());
            builder.ApplyConfiguration(new GuiaTipoMap());
            //builder.Ignore<>();
            
            base.OnModelCreating(builder);
        }
    }
}