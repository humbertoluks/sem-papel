using Domain.Models;
using Domain.Notifications;
using Microsoft.EntityFrameworkCore;
using Repository.Maps;

namespace Repository.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Student> ModelExamples { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("api");
            builder.ApplyConfiguration(new StudentMap());
            
            builder.Ignore<Notification>();
            
            //base.OnModelCreating(builder);
        }
    }
}