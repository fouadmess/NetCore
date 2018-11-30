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
    using Messaia.Net.Api;

    /// <summary>
    /// An extension helper class to add filters DI
    /// </summary>
    public static class FilterCollectionExtensions
    {
        /// <summary>
        /// Adds the filters to the DI container
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddFilters(this IServiceCollection services)
        {
            return services
                    .AddScoped<ValidateModelStateAttribute>();
        }
    }
}