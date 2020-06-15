using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Repository;
using Repository.Interfaces;
using Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace DI
{
    public class Bootstraper
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(_ => configuration);

            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase(databaseName: "Database").EnableSensitiveDataLogging(), ServiceLifetime.Scoped);
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient(typeof(IGuiaRepository), typeof(GuiaRepository));
        }
    }
}