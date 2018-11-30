///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using System.Linq;
    using System.Reflection;
    using Messaia.Net.Model;
    using Messaia.Net.Security;

    /// <summary>
    /// An extension helper class to add security-services DI
    /// </summary>
    public static class SecurityCollectionExtensions
    {
        /// <summary>
        /// Adds the security-services to the DI container
        /// </summary>
        /// <param name="services"></param>
        /// <param name="handlerNamespaces">The namespace of the authorization handlers</param>
        /// <returns></returns>
        public static SecurityBuilder AddSecurity(this IServiceCollection services, params string[] handlerNamespaces)
        {
            /* Add generic security service */
            services
                .AddScoped(typeof(ISecurityService<>), typeof(SecurityServiceBase<>))
                .AddSingleton<IConfigureOptions<MvcOptions>, ConfigureMvcOptions>();

            /* Add authorization handler in the specified namespace */
            new string[] { typeof(CreateHandlerBase<>).Namespace }.Concat(handlerNamespaces)
                .Select(name => Assembly.Load(new AssemblyName(name)))
                .SelectMany(a => a.ExportedTypes)
                .Where(x => typeof(IAuthorizationHandler).IsAssignableFrom(x))
                .Where(x => !x.GetTypeInfo().IsGenericType)
                .ToList()
                .ForEach(x =>
                {
                    services.AddTransient(typeof(IAuthorizationHandler), x);
                });

            /* Create a security builder instance */
            var securityBuilder = new SecurityBuilder(services);

            /* Register handler for models in the specified namespaces */
            handlerNamespaces
                .Select(name => Assembly.Load(new AssemblyName(name)))
                .SelectMany(a => a.ExportedTypes)
                .Where(x => typeof(IEntity<int>).IsAssignableFrom(x))
                .Where(x => !x.GetTypeInfo().IsGenericType)
                .ToList()
                .ForEach(x =>
                {
                    securityBuilder.AddAuthorizationHandlers(x);
                });

            return securityBuilder;
        }
    }
}