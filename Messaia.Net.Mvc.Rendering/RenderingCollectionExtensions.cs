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
    using Messaia.Net.Mvc.Rendering;

    /// <summary>
    /// An extension helper class to add utilities DI
    /// </summary>
    public static class RenderingCollectionExtensions
    {
        /// <summary>
        /// Adds the renderer services to the DI container
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRendering(this IServiceCollection services)
        {
            return services
                    .AddScoped<IViewRenderer, RazorRenderer>()
                    .AddScoped<IComponentRenderer, RazorRenderer>();
        }
    }
}