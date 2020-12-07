
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using TestWebApi.Infrastructure;
using TestWebApi.Infrastructure.Repositories;
using TestWebApi.Services;

namespace TestWebApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<DefaultDbContext>(opt => opt.UseInMemoryDatabase("SomeModelDB"), ServiceLifetime.Singleton, ServiceLifetime.Singleton);

            services.AddScoped<ISomeModelService, SomeModelService>();
            services.AddScoped<ISomeModelRepository, SomeModelRepository>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
