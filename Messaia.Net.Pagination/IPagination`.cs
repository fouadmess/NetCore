///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 23:16:08
///   Copyright (©)   2017, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Pagination
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// IPagination class.
    /// </summary>
    public interface IPagination<TEntity> : IPagination where TEntity : class
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Items
        /// </summary>
        new IList<TEntity> Items { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Builds the pagination
        /// </summary>
        /// <param name="query"></param>
        void Build(IQueryable<TEntity> query);

        #endregion
    }
}