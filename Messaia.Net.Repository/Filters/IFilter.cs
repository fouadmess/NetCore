///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Repository
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// The IFilter interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IFilter<TEntity> where TEntity : class
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Search
        /// </summary>
        string Search { get; set; }

        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        int? Id { get; set; }

        /// <summary>
        /// Gets or sets the Ids
        /// </summary>
        int[] Ids { get; set; }

        /// <summary>
        /// Gets or sets the SortBy
        /// </summary>
        string SortBy { get; set; }

        /// <summary>
        /// Gets or sets the SortOrder
        /// </summary>
        SortOrder? SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the Predicate
        /// </summary>
        Expression<Func<TEntity, bool>> Predicate { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles and builds the filter.
        /// </summary>
        /// <param name="query">The query object</param>
        /// <returns></returns>
        IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query);

        /// <summary>
        /// Applys OrderBy to the specified query.
        /// </summary>
        /// <param name="query">The query source</param>
        /// <returns></returns>
        IQueryable<TEntity> ApplyOrder(IQueryable<TEntity> query);

        #endregion
    }
}