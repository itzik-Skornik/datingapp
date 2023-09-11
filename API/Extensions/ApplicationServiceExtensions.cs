

using API.Data;
using API.interfaces;
using API.services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
        {
            services.AddDbContext<DataContext>(opt =>
 {
     opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
 });
            services.AddScoped<iTokenService, TokenService>();
            services.AddCors();
            return services;
        }
    }
}