///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Repository.Impl
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Linq.Expressions;
    using Messaia.Net.Model;

    /// <summary>
    /// The Filter class
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class FilterBase<TEntity> : IFilter<TEntity> where TEntity : class, IEntity<int>, new()
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Search
        /// </summary>
        public virtual string Search { get; set; }

        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public virtual int? Id { get; set; }

        /// <summary>
        /// Gets or sets the Ids
        /// </summary>
        public virtual int[] Ids { get; set; }

        /// <summary>
        /// Gets or sets the Order
        /// </summary>
        public virtual string SortBy { get; set; }

        /// <summary>
        /// Gets or sets the SortOrder
        /// </summary>
        public virtual SortOrder? SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the Predicate
        /// </summary>
        public virtual Expression<Func<TEntity, bool>> Predicate { get; set; } = PredicateBuilder.True<TEntity>();

        /// <summary>
        /// Gets the EntityName
        /// </summary>
        public virtual string EntityName { get { return typeof(TEntity).Name; } }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles and builds the filter.
        /// </summary>
        /// <param name="query">The query object</param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query)
        {
            /* Add Search predicate */
            query = query.Where(this.BuildSearchPredicate());

            /* Filter by id */
            if (this.Id != null)
            {
                query = query.Where(x => this.Id == x.Id);
            }

            /* Filter by ids */
            if (this.Ids != null)
            {
                query = query.Where(x => this.Ids.Contains(x.Id));
            }

            return query;
        }

        /// <summary>
        /// Applys OrderBy to the specified query.
        /// </summary>
        /// <param name="query">The query source</param>
        public virtual IQueryable<TEntity> ApplyOrder(IQueryable<TEntity> query)
        {
            if (!string.IsNullOrWhiteSpace(this.SortBy))
            {
                /* Descending order should start with a '-' (i.e: -Id) */
                var sortOrder = this.SortOrder ?? (this.SortBy.StartsWith("-") ? Repository.SortOrder.DESC : Repository.SortOrder.ASC);

                /* Apply order using dynamic linq */
                return query.OrderBy($"{this.SortBy.Replace("-", "")} {sortOrder.ToString()}");
            }

            return query;
        }

        #region Helpers

        /// <summary>
        /// A helper method to create a predicate for the search words.
        /// </summary>
        /// <returns></returns>
        protected virtual Expression<Func<TEntity, bool>> BuildSearchPredicate()
        {
            return PredicateBuilder.True<TEntity>();
        }

        #endregion

        #endregion
    }
}