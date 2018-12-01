///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           10.04.2017 09:20:22
///   Copyright (©)   2017, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Microsoft.Extensions.DependencyInjection
{
    using ServiceTest.Services;
    using Messaia.Net.Service;
    using Messaia.Net.Service.Impl;

    /// <summary>
    /// An extension helper class to add services DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds entity services to the DI container
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            /* Entity services */
            services
                .AddScoped(typeof(IEntityService<>), typeof(EntityService<>))
                .AddScoped<IProductService, ProductService>();

            return services;

        }
    }
}