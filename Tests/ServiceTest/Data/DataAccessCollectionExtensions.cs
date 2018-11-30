///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           10.04.2017 09:53:24
///   Copyright (©)   2017, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Microsoft.Extensions.DependencyInjection
{
    using Configuration;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using ServiceTest.Data;
    using Messaia.Net.Data;

    /// <summary>
    /// An extension helper class to add EntityFramework DI
    /// </summary>
    public static class DataAccessCollectionExtensions
    {
        /// <summary>
        /// Adds the EF to the DI container
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfigurationRoot configuration)
        {
            return services
                    .AddScoped<IDbContext>(p => p.GetService<AppDbContext>())
                    .AddSingleton<DatabaseInitializer>();
            //.AddDbContext<AppDbContext>(options =>
            //    options
            //        .UseNpgsql(configuration.GetConnectionString("NpSql"))
            //        //.UseSqlServer(configuration.GetConnectionString("SqlServer"))
            //        .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
            //);
        }
    }
}