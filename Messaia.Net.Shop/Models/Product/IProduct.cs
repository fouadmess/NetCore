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
    using Messaia.Net.Model;

    /// <summary>
    /// IProduct interface
    /// </summary>
    public interface IProduct : IEntity<int>, IAuditEntity
    {
        /// <summary>
        /// Gets or sets the GroupId
        /// </summary>
        int? GroupId { get; set; }

        /// <summary>
        /// Gets or sets the CategoryId
        /// </summary>
        int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the Alias
        /// </summary>
        string Alias { get; set; }

        /// <summary>
        /// Gets or sets the ItemNumber
        /// </summary>
        string ItemNumber { get; set; }

        /// <summary>
        /// Gets or sets the Store
        /// </summary>
        int Store { get; set; }

        /// <summary>
        /// Gets or sets the Price
        /// </summary>
        float Price { get; set; }

        /// <summary>
        /// Gets or sets the PriceNet
        /// </summary>
        float PriceNet { get; set; }

        /// <summary>
        /// Gets or sets the ListPrice
        /// </summary>
        float ListPrice { get; set; }

        /// <summary>
        /// Gets or sets the Points
        /// </summary>
        int Points { get; set; }

        /// <summary>
        /// Gets or sets the Pieces
        /// </summary>
        int Pieces { get; set; }

        /// <summary>
        /// Gets or sets the ShippingCosts
        /// </summary>
        float ShippingCosts { get; set; }

        /// <summary>
        /// Gets or sets the Tax
        /// </summary>
        int Tax { get; set; }

        /// <summary>
        /// Gets or sets the TaxAmount
        /// </summary>
        float TaxAmount { get; }

        /// <summary>
        /// Gets or sets the CartLimit
        /// </summary>
        int CartLimit { get; set; }

        /// <summary>
        /// Gets or sets the OrderLimit
        /// </summary>
        int OrderLimit { get; set; }

        /// <summary>
        /// Gets or sets the Icon
        /// </summary>
        string Icon { get; set; }

        /// <summary>
        /// Gets or sets the ImageSmall
        /// </summary>
        string ImageSmall { get; set; }

        /// <summary>
        /// Gets or sets the ImageMedium
        /// </summary>
        string ImageMedium { get; set; }

        /// <summary>
        /// Gets or sets the ImageLarge
        /// </summary>
        string ImageLarge { get; set; }

        /// <summary>
        /// Gets or sets the Color
        /// </summary>
        string Color { get; set; }

        /// <summary>
        /// Gets or sets the Width
        /// </summary>
        double Width { get; set; }

        /// <summary>
        /// Gets or sets the Height
        /// </summary>
        double Height { get; set; }

        /// <summary>
        /// Gets or sets the Length
        /// </summary>
        double Length { get; set; }

        /// <summary>
        /// Gets or sets the Weight
        /// </summary>
        double Weight { get; set; }

        /// <summary>
        /// Gets or sets the Enabled
        /// </summary>
        bool Enabled { get; set; }

        ///// <summary>
        ///// Gets or sets the Group
        ///// </summary>
        //IProductGroup Group { get; set; }

        ///// <summary>
        ///// Gets or sets the Category
        ///// </summary>
        //ICategory Category { get; set; }
    }
}
