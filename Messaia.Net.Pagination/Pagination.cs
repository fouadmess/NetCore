///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Pagination
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The Pagination class
    /// </summary>
    public class Pagination<TEntity> : IPagination<TEntity> where TEntity : class
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Items
        /// </summary>
        public IList<TEntity> Items { get; set; } = new List<TEntity>();

        /// <summary>
        /// Gets or sets the Items
        /// </summary>
        IList IPagination.Items { get; set; }

        /// <summary>
        /// Gets or sets the Page
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Gets or sets the PageSize
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// Gets or sets the Total
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the PageCount
        /// </summary>
        public int PageCount { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="Pagination"/> class.
        /// </summary>
        public Pagination()
        {
            this.Page = 1;
            this.PageSize = 20;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="Pagination"/> class.
        /// </summary>
        /// <param name="query">The query object</param>
        /// <param name="page">The page number</param>
        /// <param name="limit">The pagination limit</param>
        public Pagination(int page, int pageSize)
        {
            this.Page = Math.Max(1, page);
            this.PageSize = Math.Max(1, pageSize);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Builds the pagination
        /// </summary>
        /// <param name="query"></param>
        public void Build(IQueryable<TEntity> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            this.Total = query.Count();
            this.PageCount = (int)Math.Ceiling(this.Total / (double)this.PageSize);
            this.Items = query.Skip((this.Page - 1) * this.PageSize).Take(this.PageSize).ToList();
        }

        #endregion
    }
}