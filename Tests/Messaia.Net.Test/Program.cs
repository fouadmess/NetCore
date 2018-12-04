using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.Net.Mail;
using Messaia.Net.Identity;
using Messaia.Net.MailService;
using Messaia.Net.Repository;
using Messaia.Net.Service;

namespace Messaia.Net.Test
{
    class Program
    {
        public interface IUserService : IEntityService<User> { }

        static void Main(string[] args)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                //.SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            var services = new ServiceCollection()
                .AddOptions()
                .AddLogging()
                .AddDatabase(configuration)
                .AddRepository<ShopDbContext>()
                .AddSingleton<DatabaseInitializer>()
                //.AddShop<ShopDbContext>()
                .AddMailService(configuration.GetSection("mail"));

            services.AddIdentity<ShopDbContext>(configuration.GetSection("Identity"));

            var provider = services.BuildServiceProvider();

            //var dbInit = provider.GetRequiredService<DatabaseInitializer>();
            //dbInit.Init();

            //var unitOfWork = provider.GetService<IUnitOfWork>();
            //var productRepo = provider.GetService<IProductRepository>();

            //productRepo.Add(new Product
            //{
            //    Name = "Product1",
            //    CreatedByUserId = 1
            //});
            //unitOfWork.SaveChanges();

            new Program().TestMail(provider);

            Console.WriteLine("Done!");
            Console.ReadKey();
        }

        /// <summary>
        /// TestMail
        /// </summary>
        /// <param name="provider"></param>
        private void TestMail(IServiceProvider provider)
        {
            var mailService = provider.GetService<IMailService>();

            //mailService.SendAsync(new string[] { "fouad.messaia@gmail.com", "fouadmess@gmail.com" }, "Subject", "Body");

            var message = new MailMessage("fouad.messaia@gmail.com", "fouadmess@gmail.com", "Mail als Anhang mit Anhang", "Bodyyyyyyyyy");
            //message.Attachments.Add(new Attachment(@"D:\Temp\1.pdf"));
            var attachment = message.ToAttachment();

            try
            {
                mailService.SendAsync(new MailBuilder()
                    .To("fouad.messaia@gmail.com")
                    .Cc("fouadmess@gmail.com")
                    .Priority(MailPriority.High)
                    .Subject("Über")
                    .Body(@"<html><body><h1 style=""color: red"">Hurraa 1</h1><body></html>")
                    //.Attachment(@"D:\Temp\Report.pdf")
                    .Attachment(attachment)
                    .Callback(HandleCallback)
                );

                //if (false)
                //{
                //    mailService.SendAsync(new MailBuilder()
                //        .To("fouad.messaia@gmail.com")
                //        .Bcc("fouadmess@gmail.com")
                //        .Priority(MailPriority.Low)
                //        .Subject("My Subject2")
                //        .Body(@"<html><body><h1 style=""color: red"">Hurraa 2</h1><body></html>")
                //        //.Attachment(@"D:\Temp\Report.pdf")
                //        .Attachment(attachment)
                //        .Callback(HandleCallback)
                //    );

                //    mailService.SendAsync(new MailBuilder()
                //        .To("fouad.messaia@gmail.com")
                //        //.To("fouadmess@gmail.com")
                //        .Subject("My Subject3")
                //        .Body(@"<html><body><h1 style=""color: red"">Hurraa 3</h1><body></html>")
                //        //.Attachment(@"D:\Temp\Report.pdf")
                //        .Attachment(attachment)
                //        .Callback(HandleCallback)
                //    );
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void HandleCallback(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine("Jaaaaaaa Huraaa");
        }
    }
}