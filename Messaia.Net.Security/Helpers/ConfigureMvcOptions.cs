///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 14:07:24
///   Copyright (©)   2017, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Security
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// ConfigureMvcOptions class.
    /// </summary>
    internal class ConfigureMvcOptions : IConfigureOptions<MvcOptions>
    {
        /// <summary>
        /// Invoked to configure a TOptions instance.
        /// </summary>
        /// <param name="options"></param>
        public void Configure(MvcOptions options)
        {
            options.Filters.Add(new SecurityExceptionFilter());
        }
    }
}