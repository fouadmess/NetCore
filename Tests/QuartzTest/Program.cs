using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Threading;

namespace QuartzTest
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Add services to DI */
            var provider = new ServiceCollection()
                .AddQuartz(nameof(QuartzTest))
                .BuildServiceProvider();

            /* Get the scheduler */
            var scheduler = provider.GetRequiredService<IScheduler>();

            /* Schedule the job */
            var started = scheduler.ScheduleAsync<HelloJob>("HelloJob", "OCR", new { Name = "Fouad" }, y =>
                y.StartNow()
                .WithSimpleSchedule(z =>
                    z.WithIntervalInSeconds(2).RepeatForever()
                )
            ).GetAwaiter().GetResult();

            /* Add some logs, if started */
            if (started)
            {
                Console.WriteLine("Job started");
            }

            Thread.Sleep(3000);

            scheduler.StopJob("HelloJob", "OCR").GetAwaiter().GetResult();

            Console.ReadKey();
        }
    }
}
