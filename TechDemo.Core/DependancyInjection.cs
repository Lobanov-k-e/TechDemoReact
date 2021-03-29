using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TechDemo.Core.Infrastructure.Persisence;
using TechDemo.Core.Infrastructure.Services;

namespace TechDemo.Core
{
    public static class DependancyInjection
    {
        public static void AddDemoCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));


            services.AddScoped<IAppContext>(provider => provider.GetService<ApplicationContext>());
            services.AddTransient<IUserService, UserService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

    }
}
