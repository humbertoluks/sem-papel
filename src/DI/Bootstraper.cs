using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using Repository;
using Repository.Interfaces;
using Repository.Data;
using Service;
using Service.Interfaces;

namespace DI
{
    public class Bootstraper
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(_ => configuration);

            //services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase(databaseName: "Database").EnableSensitiveDataLogging(), ServiceLifetime.Scoped);
            services.AddDbContext<DataContext>( opt => opt.UseSqlServer(connectionString: configuration.GetConnectionString("database")) );
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient(typeof(IGuiaRepository), typeof(GuiaRepository));
            services.AddTransient(typeof(IGuiaNumeroRepository), typeof(GuiaNumeroRepository));

            services.AddTransient(typeof(IGuiaService), typeof(GuiaService));
            services.AddTransient(typeof(IAssociadoService), typeof(AssociadoService));
            services.AddTransient(typeof(IPrestadorService), typeof(PrestadorService));
        }
    }
}