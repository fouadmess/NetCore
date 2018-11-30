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
    using AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System;
    using Messaia.Net.Data;
    using Messaia.Net.Identity;

    /// <summary>
    /// An extension helper class to add identity DI
    /// </summary>
    public static class IdentityCollectionExtensions
    {
        /// <summary>
        /// Adds the default identity system configuration.
        /// </summary>
        /// <param name="services">The services available in the application.</param>
        /// <returns>An <see cref="IdentityBuilder"/> for creating and configuring the identity system.</returns>
        public static IdentityBuilder AddIdentity<TDbContext>(this IServiceCollection services)
            where TDbContext : IdentityDbContext
        {
            return services.AddIdentity<TDbContext>(setupAction: null);
        }

        /// <summary>
        /// Adds the default identity system configuration.
        /// </summary>
        /// <param name="services">The services available in the application.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> to use for <see cref="IdentityOptions"/>.</param>
        /// <returns>An <see cref="IdentityBuilder"/> for creating and configuring the identity system.</returns>
        public static IdentityBuilder AddIdentity<TDbContext>(this IServiceCollection services, IConfiguration configuration)
            where TDbContext : IdentityDbContext
        {
            return services.AddIdentity<TDbContext>(x => configuration.Bind(x));
        }

        /// <summary>
        /// Adds the default identity system configuration.
        /// </summary>
        /// <param name="services">The services available in the application.</param>
        /// <returns>An <see cref="IdentityBuilder"/> for creating and configuring the identity system.</returns>
        public static IdentityBuilder AddIdentity<TDbContext>(this IServiceCollection services, Action<IdentityOptions> setupAction)
            where TDbContext : IdentityDbContext
        {
            if (typeof(TDbContext) != typeof(IdentityDbContext))
            {
                services.AddScoped<IdentityDbContext>(p => p.GetService<TDbContext>());
            }

            return services
                .AddScoped<IDbContext>(p => p.GetService<TDbContext>())
                .AddIdentity<User, Role>(setupAction)
                .AddUserStore<UserStore>()
                .AddRoleStore<RoleStore>()
                .AddUserManager<UserManager>()
                .AddRoleManager<RoleManager>()
                .AddSignInManager<SignInManager>()
                .AddDefaultTokenProviders();
        }
    }
}