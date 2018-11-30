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
    /// ICart interface
    /// </summary>
    public interface ICart : IEntity<int>, IAuditEntity
    {
        /// <summary>
        /// Gets or sets the CartItems
        /// </summary>
        ICollection<ICartItem> Items { get; set; }
    }
}