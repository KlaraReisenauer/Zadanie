using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ninject;
using Ninject.Web.AspNetCore;
using Ninject.Web.AspNetCore.Hosting;
using Ninject.Web.Common.SelfHost;
using ZadanieAPI.Data.Repositories.Interfaces;
using ZadanieAPI.Data.Repositories;

namespace ZadanieAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostConfiguration = new AspNetCoreHostConfiguration(args)
                .UseStartup<Startup>() // TODO: .UseWebHostBuilder(CreateWebHostBuilder) ??
                .UseKestrel()
                .BlockOnStart();

            var host = new NinjectSelfHostBootstrapper(CreateKernel, hostConfiguration);
            host.Start();
        }

        public static IKernel CreateKernel()
        {
            var settings = new NinjectSettings();
            settings.LoadExtensions = false;

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
        //public static IWebHostBuilder CreateWebHostBuilder()
        //{
        //    return new DefaultWebHostConfiguration(null)
        //        .ConfigureAll()
        //        .GetBuilder();
        //}
    }
}
