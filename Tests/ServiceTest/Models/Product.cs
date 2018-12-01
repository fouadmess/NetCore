///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           24.02.2018 13:17:46
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace ServiceTest.Models
{
    using System.Collections.Generic;
    using Messaia.Net.Model;

    /// <summary>
    /// Product class.
    /// </summary>
    public class Product : IEntity<int>
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Colors
        /// </summary>
        public ICollection<Color> Colors { get; set; }
    }
}