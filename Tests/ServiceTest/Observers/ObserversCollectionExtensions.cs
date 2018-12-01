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
    using ServiceTest.Observers;

    /// <summary>
    /// An extension helper class to add observers DI
    /// </summary>
    public static class ObserversCollectionExtensions
    {
        /// <summary>
        /// Adds observers to the DI container
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddObservers(this IServiceCollection services)
        {
            return services
                    .AddScoped<IProductObserver, ProductObserver>();
        }
    }
}