///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 02:50:28
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.EntityFrameworkCore;
    using Messaia.Net.Repository;
    using Messaia.Net.Repository.Impl;

    /// <summary>
    /// Extension methods for setting up unit of work and related services in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class RepositoryCollectionExtensions
    {
        /// <summary>
        /// Registers the unit of work and the gegenric repository given context 
        /// as a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The services available in the application.</param>
        /// <returns>An <see cref="IdentityBuilder"/> for creating and configuring the identity system.</returns>
        public static IServiceCollection AddRepository<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext
        {
            return services
                .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddScoped<IUnitOfWork, UnitOfWork<TDbContext>>();
        }
    }
}