using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infraestructure;

namespace Data
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceData(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<MileageDBContext>(options =>
               options.UseSqlServer( configuration.GetConnectionString("MileageDb")));

            services.AddTransient<IMileageRepository, MileageRepository>();
        }
    }
}
