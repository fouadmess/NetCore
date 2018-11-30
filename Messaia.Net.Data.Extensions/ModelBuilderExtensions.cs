///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 10:51:28
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Microsoft.EntityFrameworkCore
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// ModelBuilderExtensions class.
    /// </summary>
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Applies configurations that is defined in an <see cref="IEntityTypeConfiguration<>"/> instance.
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="namespaces">The namespace</param>
        public static void ApplyConfigurations(this ModelBuilder modelBuilder, params string[] namespaces)
        {
            namespaces
                .Select(name => Assembly.Load(new AssemblyName(name)))
                .SelectMany(a => a.ExportedTypes)
                .Where(x => x.IsAssignableToGenericType(typeof(IEntityTypeConfiguration<>)))
                .ToList()
                .ForEach(x =>
                {
                    var genericMethod = typeof(ModelBuilder)
                        .GetGenericMethod(nameof(ModelBuilder.ApplyConfiguration), new Type[] { typeof(IEntityTypeConfiguration<>) })?
                        .MakeGenericMethod(x.GetInterfaces()?.FirstOrDefault()?.GenericTypeArguments?.FirstOrDefault())?
                        .Invoke(modelBuilder, new object[] { Activator.CreateInstance(x) });
                });
        }
    }
}