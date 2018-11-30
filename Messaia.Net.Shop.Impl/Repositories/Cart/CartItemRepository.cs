///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           27.01.2018 17:39:37
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Shop.Impl
{
    using Messaia.Net.Data;
    using Messaia.Net.Repository.Impl;

    /// <summary>
    /// Repository implemetation for the <see cref="Cart"/> model class.
    /// </summary>
    public class CartItemRepository : GenericRepository<int, ICartItem>, ICartItemRepository
    {
        #region Fields


        #endregion

        #region Properties


        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="CartItemRepository"/> class.
        /// </summary>
        public CartItemRepository(IDbContext dbContext) : base(dbContext) { }

        #endregion

        #region Methods


        #endregion
    }
}