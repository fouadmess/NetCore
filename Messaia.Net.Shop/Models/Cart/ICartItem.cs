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
    /// ICartItem interface
    /// </summary>
    public interface ICartItem : IEntity<int>, IAuditEntity
    {
        /// <summary>
        /// Gets or sets the Quantity
        /// </summary>
        int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the SubTotal
        /// </summary>
        float SubTotal { get; }

        /// <summary>
        /// Gets or sets the SubTotalNet
        /// </summary>
        float SubTotalNet { get; }

        /// <summary>
        /// Gets or sets the SubTotalPoints
        /// </summary>
        int SubTotalPoints { get; }

        /// <summary>
        /// Gets or sets the ProductId
        /// </summary>
        int? ProductId { get; set; }

        /// <summary>
        /// Gets or sets the Product
        /// </summary>
        IProduct Product { get; set; }

        /// <summary>
        /// Gets or sets the ProductId
        /// </summary>
        int? CartId { get; set; }

        /// <summary>
        /// Gets or sets the Cart
        /// </summary>
        ICart Cart { get; set; }
    }
}