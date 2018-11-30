///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           02.03.2018 21:35:04
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace ServiceTest
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.Extensions.DependencyInjection;
    using ServiceTest.Data;
    using System;
    using Messaia.Net.Data;

    /// <summary>
    /// ApplicationDbContextFactory class.
    /// </summary>
    public class ApplicationDbContextFactory
    {
        public AppDbContext CreateApplicationDbContext(IServiceCollection services)
        {
            //services.AddDbContext<AppDbContext>(options =>
            //    options
            //        .UseNpgsql("Server=localhost;User Id=postgres;Password=FouadM@1984;Database=EFCore_Service_Test;")
            //        .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
            //);

            //var provider = services.BuildServiceProvider();

            var opt = new DbContextOptionsBuilder<AppDbContext>();
            opt.UseNpgsql("Server=localhost;User Id=postgres;Password=FouadM@1984;Database=EFCore_MN_Test3;");

            return new AppDbContext(opt.Options);
        }
    }
}