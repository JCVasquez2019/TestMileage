using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infraestructure;

namespace Service
{
    public static class ServiceRegistration
    {
        public static void AddServiceData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IMileageService, MileageService>();
        }
    }
}
