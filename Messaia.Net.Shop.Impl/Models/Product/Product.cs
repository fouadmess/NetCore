///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           27.01.2018
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Shop.Impl
{
    using Messaia.Net.Model;

    /// <summary>
    /// IProduct interface
    /// </summary>
    public class Product : AuditEntity, IProduct
    {
        /// <summary>
        /// Gets or sets the GroupId
        /// </summary>
        public int? GroupId { get; set; }

        /// <summary>
        /// Gets or sets the CategoryId
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Alias
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets the ItemNumber
        /// </summary>
        public string ItemNumber { get; set; }

        /// <summary>
        /// Gets or sets the Store
        /// </summary>
        public int Store { get; set; }

        /// <summary>
        /// Gets or sets the Price
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// Gets or sets the PriceNet
        /// </summary>
        public float PriceNet { get; set; }

        /// <summary>
        /// Gets or sets the ListPrice
        /// </summary>
        public float ListPrice { get; set; }

        /// <summary>
        /// Gets or sets the Points
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Gets or sets the Pieces
        /// </summary>
        public int Pieces { get; set; }

        /// <summary>
        /// Gets or sets the ShippingCosts
        /// </summary>
        public float ShippingCosts { get; set; }

        /// <summary>
        /// Gets or sets the Tax
        /// </summary>
        public int Tax { get; set; }

        /// <summary>
        /// Gets or sets the TaxAmount
        /// </summary>
        public float TaxAmount { get; }

        /// <summary>
        /// Gets or sets the CartLimit
        /// </summary>
        public int CartLimit { get; set; }

        /// <summary>
        /// Gets or sets the OrderLimit
        /// </summary>
        public int OrderLimit { get; set; }

        /// <summary>
        /// Gets or sets the Icon
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the ImageSmall
        /// </summary>
        public string ImageSmall { get; set; }

        /// <summary>
        /// Gets or sets the ImageMedium
        /// </summary>
        public string ImageMedium { get; set; }

        /// <summary>
        /// Gets or sets the ImageLarge
        /// </summary>
        public string ImageLarge { get; set; }

        /// <summary>
        /// Gets or sets the Color
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the Width
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the Height
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Gets or sets the Length
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// Gets or sets the Weight
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Gets or sets the Enabled
        /// </summary>
        public bool Enabled { get; set; }

        ///// <summary>
        ///// Gets or sets the Group
        ///// </summary>
        //public IProductGroup Group { get; set; }

        ///// <summary>
        ///// Gets or sets the Category
        ///// </summary>
        //public ICategory Category { get; set; }
    }
}