/////-----------------------------------------------------------------
/////   Author:         Messaia
/////   AuthorUrl:      http://messaia.com
/////   Date:           27.01.2018
/////   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
/////                   Licensed under the Apache License, Version 2.0. 
/////                   See License.txt in the project root for license information.
/////-----------------------------------------------------------------
//namespace Messaia.Net.Shop.Impl
//{
//    using System.Collections.Generic;
//    using Messaia.Net.Model;

//    /// <summary>
//    /// IProductGroup interface
//    /// </summary>
//    public interface ProductGroup : IEntity<int>, IAuditEntity
//    {
//        /// <summary>
//        /// Gets or sets the ParentGroupId
//        /// </summary>
//        int? ParentGroupId { get; set; }

//        /// <summary>
//        /// Gets or sets the Name
//        /// </summary>
//        string Name { get; set; }

//        /// <summary>
//        /// Gets or sets the Alias
//        /// </summary>
//        string Alias { get; set; }

//        /// <summary>
//        /// Gets or sets the Description
//        /// </summary>
//        string Description { get; set; }

//        /// <summary>
//        /// Gets or sets the Price
//        /// </summary>
//        float Price { get; set; }

//        /// <summary>
//        /// Gets or sets the PriceNet
//        /// </summary>
//        float PriceNet { get; set; }

//        /// <summary>
//        /// Gets or sets the ListPrice
//        /// </summary>
//        float ListPrice { get; set; }

//        /// <summary>
//        /// Gets or sets the Tax
//        /// </summary>
//        int Tax { get; set; }

//        /// <summary>
//        /// Gets or sets the Points
//        /// </summary>
//        int Points { get; set; }

//        /// <summary>
//        /// Gets or sets the Published
//        /// </summary>
//        bool Enabled { get; set; }

//        /// <summary>
//        /// Gets or sets the ParentGroup
//        /// </summary>
//        ProductGroup ParentGroup { get; set; }

//        /// <summary>
//        /// Gets or sets the Products
//        /// </summary>
//        ICollection<Product> Products { get; set; }
//    }
//}
