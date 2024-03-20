using CLOUPARD_Test_task.DAL;
using Microsoft.EntityFrameworkCore;

namespace CLOUPARD_Test_task.Extensions
{
    public static class ConfigureServicesExtensions
    {
        public static IServiceCollection ConfigureDatabaseContext(this IServiceCollection services)
        {
            var builder = WebApplication.CreateBuilder();

            services.AddDbContext<TestDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TestConnection")));

            return services;
        }
    }
}
