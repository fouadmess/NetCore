///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           27.01.2018
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Shop
{
    using System.Collections.Generic;
    using Messaia.Net.Model;

    /// <summary>
    /// ICategory interface
    /// </summary>
    public interface ICategory : IEntity<int>, IAuditEntity
    {
        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the Enabled
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the Products
        /// </summary>
        ICollection<IProduct> Products { get; set; }
    }
}