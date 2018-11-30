///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           27.01.2018 18:17:18
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Shop.Impl
{
    using Messaia.Net.Model;

    /// <summary>
    /// CartItem class.
    /// </summary>
    public class CartItem : AuditEntity, ICartItem
    {
        /// <summary>
        /// Gets or sets the Quantity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the SubTotal
        /// </summary>
        public float SubTotal { get; set; }

        /// <summary>
        /// Gets or sets the SubTotalNet
        /// </summary>
        public float SubTotalNet { get; set; }

        /// <summary>
        /// Gets or sets the SubTotalPopublic ints
        /// </summary>
        public int SubTotalPoints { get; set; }

        /// <summary>
        /// Gets or sets the ProductId
        /// </summary>
        public int? ProductId { get; set; }

        /// <summary>
        /// Gets or sets the Product
        /// </summary>
        public IProduct Product { get; set; }

        /// <summary>
        /// Gets or sets the ProductId
        /// </summary>
        public int? CartId { get; set; }

        /// <summary>
        /// Gets or sets the Cart
        /// </summary>
        public ICart Cart { get; set; }
    }
}