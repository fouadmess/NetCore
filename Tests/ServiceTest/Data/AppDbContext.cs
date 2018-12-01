///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           24.02.2018 13:19:34
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace ServiceTest.Data
{
    using Microsoft.EntityFrameworkCore;
    using ServiceTest.Models;
    using Messaia.Net.Data;

    /// <summary>
    /// AppDbContext class.
    /// </summary>
    public class AppDbContext : DbContext, IDbContext
    {
        /// <summary>
        /// Gets or sets the Products
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the Colors
        /// </summary>
        public DbSet<Color> Colors { get; set; }

        /// <summary>
        /// Initializes an instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DbContext class using the specified options.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}