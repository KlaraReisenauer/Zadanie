using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ninject.Web.AspNetCore;
using Ninject.Web.AspNetCore.Hosting;
using ZadanieAPI.Database.Models;

namespace ZadanieAPI
{
    public class Startup : AspNetCoreStartupBase
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration,
            IServiceProviderFactory<NinjectServiceProviderBuilder> providerFactory)
        : base(providerFactory)
        {
            Configuration = configuration;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddDbContext<CoreDbContext>(op => op.UseSqlServer(Configuration.GetConnectionString("Default"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddAutoMapper(typeof(Startup));

            services.AddCors(c =>
            {
                c.AddPolicy(name: "AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddControllers();
        }

        public override void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandler("/error");

            var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

            app.UseRouting();

            app.UseCors("AllowOrigin");
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
