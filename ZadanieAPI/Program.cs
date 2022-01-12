using Microsoft.AspNetCore.Hosting;
using Ninject;
using Ninject.Web.AspNetCore;
using Ninject.Web.AspNetCore.Hosting;
using Ninject.Web.Common.SelfHost;
using ZadanieAPI.Repositories;
using ZadanieAPI.Repositories.Interfaces;

namespace ZadanieAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostConfiguration = new AspNetCoreHostConfiguration(args)
                .UseStartup<Startup>()
                .UseKestrel()
                .BlockOnStart();

            var host = new NinjectSelfHostBootstrapper(CreateKernel, hostConfiguration);
            host.Start();
        }

        public static IKernel CreateKernel()
        {
            var settings = new NinjectSettings
            {
                LoadExtensions = false
            };

            var kernel = new AspNetCoreKernel(settings);
            kernel.Load(typeof(AspNetCoreHostConfiguration).Assembly);

            RegisterServices(kernel);

            return kernel;
        }

        public static void RegisterServices(AspNetCoreKernel kernel)
        {
            kernel.Bind<IEmployeeRepository>().To<EmployeeRepository>();
            kernel.Bind<IPastEmployeeRepository>().To<PastEmployeeRepository>();
            kernel.Bind<IPositionRepository>().To<PositionRepository>();
        }
    }
}
