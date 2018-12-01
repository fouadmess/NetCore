using ServiceTest.Models;
using System.Collections.Generic;
///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           24.02.2018 13:33:23
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace ServiceTest.Data
{
    /// <summary>
    /// DatabaseInitializer class.
    /// </summary>
    public class DatabaseInitializer
    {
        #region Fields


        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the DbContext
        /// </summary>
        public AppDbContext DbContext { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="UserController"/> class.
        /// </summary>
        public DatabaseInitializer(AppDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initilizes the database.
        /// </summary>
        /// <returns></returns>
        public bool Init(bool deleteDatabase = false)
        {
            var database = this.DbContext.Database;
            if (database != null)
            {
                if (deleteDatabase)
                {
                    database.EnsureDeleted();
                }

                /* Create database in */
                if (database.EnsureCreated())
                {
                    this.DbContext.Add(new Product
                    {
                        Name = "Product-1",
                        Colors = new List<Color> { new Color { Name = "Color-1-1" }, new Color { Name = "Color-1-2" } }
                    });

                    this.DbContext.Add(new Product
                    {
                        Name = "Product-2",
                        Colors = new List<Color> { new Color { Name = "Color-2-1" }, new Color { Name = "Color-2-2" } }
                    });

                    this.DbContext.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}