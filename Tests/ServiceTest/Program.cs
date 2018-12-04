using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceTest.Data;
using ServiceTest.Observers;
using ServiceTest.Services;
using System;
using System.Threading.Tasks;
using Messaia.Net.Repository;
using Messaia.Net.Http.Util;

namespace ServiceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Set up configuration sources. */
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();

            var services = new ServiceCollection()
                .AddOptions()
                .AddLogging()
                .AddDatabase(configuration)
                .AddTransient<ApplicationDbContextFactory>()
                .AddRepositories<AppDbContext>()
                .AddServices()
                .AddObservers()
                .AddSingleton<DatabaseInitializer>();

            services.AddTransient(x => x.GetService<ApplicationDbContextFactory>().CreateApplicationDbContext(services));

            /* Add services to DI */
            var provider = services.BuildServiceProvider();

            /* Init database */
            provider.GetRequiredService<DatabaseInitializer>().Init();

            /* Get product service */
            var productService = provider.GetRequiredService<IProductService>();

            /* Add observer to the service */
            productService.Subscribe(provider.GetRequiredService<IProductObserver>());

            #region Temp

            var auth = HttpHelper.AuthenticateAsync(new Authentication
            {
                AuthUrl = "http://localhost:5001",
                UserName = "f.messaia@veritas-data.de",
                Password = "FMP@ssw0rd",
                ApiScope = "api",
                AuthClient = "ro.client",
                AuthSecret = "96d68320-2000-46ce-1d3d-3dc85b37e8b5",

            }).GetAwaiter().GetResult();

            var productList = productService.GetListAsync(x => x.Id > 0).GetAwaiter().GetResult();

            /* Load a product (AsNoTracking) */
            var product = productService.GetAsync(x => x.Id == 1, true, false).GetAwaiter().GetResult();

            var random = new Random();
            Console.WriteLine($"Old product name: '{product?.Name}'");

            //product.Name = random.Next(1000, 9999).ToString();

            //productService.UpdateAsync(product).GetAwaiter().GetResult();

            ///* Reload the product (AsNoTracking) */
            //product = productService.GetAsync(x => x.Id == 1, true, false).GetAwaiter().GetResult();
            //Console.WriteLine($"New product name: '{product?.Name}'");

            #endregion

            #region Old

            ///* Run async tasks */
            //Task.Run(async () =>
            //{
            //    var productList = await productService.GetListAsync(x => x.Id > 0);

            //    /* Load a product (AsNoTracking) */
            //    var product = await productService.GetAsync(x => x.Id == 1, true, false);

            //    var random = new Random();
            //    Console.WriteLine($"Old product name: '{product?.Name}'");

            //    product.Name = random.Next(1000, 9999).ToString();

            //    await productService.UpdateAsync(product);

            //    /* Reload the product (AsNoTracking) */
            //    product = await productService.GetAsync(x => x.Id == 1, true, false);
            //    Console.WriteLine($"New product name: '{product?.Name}'");

            //}).Wait();

            #endregion

            Console.ReadKey();
        }
    }
}
