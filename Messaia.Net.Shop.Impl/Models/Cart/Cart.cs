///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           27.01.2018 17:41:19
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Shop.Impl
{
    using System.Collections.Generic;
    using Messaia.Net.Model;

    /// <summary>
    /// Cart class.
    /// </summary>
    public class Cart : AuditEntity, ICart
    {
        /// <summary>
        /// Gets or sets the CartItems
        /// </summary>
        public ICollection<ICartItem> Items { get; set; }
    }
}