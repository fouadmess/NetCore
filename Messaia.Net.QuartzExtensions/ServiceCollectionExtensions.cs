///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 01:23:18
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Quartz;
    using Quartz.Impl;
    using System.Linq;
    using System.Reflection;
    using Messaia.Net.QuartzExtensions;

    /// <summary>
    /// ServiceCollectionExtensions class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the Quartz as a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddQuartz(this IServiceCollection services, params string[] namespaces)
        {
            /* Get an instance of the standard factory */
            var schedulerFactory = new StdSchedulerFactory();

            /* Confige the Scheduler */
            services
                .AddSingleton<ISchedulerFactory>(schedulerFactory)
                .TryAddSingleton(serviceProvider =>
                {
                    /* Grab the Scheduler instance from the Factory */
                    var scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

                    /* Set the DI injector */
                    scheduler.JobFactory = new JobFactoryActivator(serviceProvider);

                    return scheduler;
                });

            /* Register jobs */
            namespaces
                .Select(name => Assembly.Load(new AssemblyName(name)))
                .SelectMany(a => a.ExportedTypes)
                .Where(x => typeof(IJob).IsAssignableFrom(x))
                .Where(x => !x.GetTypeInfo().IsGenericType)
                .ToList()
                .ForEach(x =>
                {
                    services.AddTransient(x);
                });

            return services;
        }
    }
}