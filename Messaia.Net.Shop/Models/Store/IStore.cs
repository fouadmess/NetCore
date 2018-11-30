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
    /// <summary>
    /// IStore interface
    /// </summary>
    public interface IStore
    {
        /// <summary>
        /// Get the available products count
        /// </summary>
        /// <param name="product">The product instance</param>
        /// <returns>int</returns>
        int GetAvailable(IProduct product);

        /// <summary>
        /// Update the product store
        /// </summary>
        /// <param name="product">The product instance</param>
        /// <param name="amount">The amount to add/sub, use negative values to sub.</param>
        /// <returns>boolean</returns>
        bool Update(IProduct product, int amount);
    }
}