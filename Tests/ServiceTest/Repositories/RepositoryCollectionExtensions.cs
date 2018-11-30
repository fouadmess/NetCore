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
    using Microsoft.EntityFrameworkCore;
    using ServiceTest.Repositories;

    /// <summary>
    /// An extension helper class to add repositories DI
    /// </summary>
    public static class RepositoryCollectionExtensions
    {
        /// <summary>
        /// Adds repositories to the DI container
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRepositories<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext
        {
            return services
                .AddRepository<TDbContext>()
                .AddScoped<IProductRepository, ProductRepository>();
        }
    }
}