
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TestWebApi.Infrastructure;
using TestWebApi.Infrastructure.Repositories;

namespace TestWebApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<DefaultDbContext>(opt => opt.UseInMemoryDatabase("SomeModelDB"), ServiceLifetime.Singleton, ServiceLifetime.Singleton);

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
