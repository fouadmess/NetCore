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
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using Messaia.Net.Model;
    using Messaia.Net.Repository;

    /// <summary>
    /// The AuditEntityFilter class
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class AuditEntityFilter<TEntity> : FilterBase<TEntity>, IAuditEntityFilter<TEntity> where TEntity : class, IAuditEntity, IEntity<int>, new()
    {
        #region Properties

        /// <summary>
        /// Gets or sets the CreatedByUserId
        /// </summary>
        public int? CreatedByUserId { get; set; }

        /// <summary>
        /// Gets or sets the CreatedByUser
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedByUser
        /// </summary>
        public int? UpdatedByUserId { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedByUser
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedDate
        /// </summary>
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDateFrom
        /// </summary>
        public DateTime? CreatedDateFrom { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDateTo
        /// </summary>
        public DateTime? CreatedDateTo { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedDateFrom
        /// </summary>
        public DateTime? UpdatedDateFrom { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedDateTo
        /// </summary>
        public DateTime? UpdatedDateTo { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles and builds the filter.
        /// </summary>
        /// <param name="query">The query object</param>
        /// <returns></returns>
        public override IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query)
        {
            /* Get predicate from base class */
            query = base.ApplyFilter(query);

            if (!string.IsNullOrWhiteSpace(this.CreatedBy))
            {
                query = query.Where(x => EF.Functions.Like(x.CreatedBy, $"%{this.CreatedBy}%"));
            }

            if (!string.IsNullOrWhiteSpace(this.UpdatedBy))
            {
                query = query.Where(x => EF.Functions.Like(x.UpdatedBy, $"%{this.UpdatedBy}%"));
            }

            if (this.CreatedDate != null)
            {
                query = query.Where(x => x.CreatedDate.Date == this.CreatedDate);
            }

            if (this.UpdatedDate != null)
            {
                query = query.Where(x => x.UpdatedDate.Value.Date == this.UpdatedDate);
            }

            if (this.CreatedDateFrom != null)
            {
                query = query.Where(x => x.CreatedDate.Date >= CreatedDateFrom);
            }

            if (this.CreatedDateTo != null)
            {
                query = query.Where(x => x.CreatedDate.Date <= CreatedDateTo);
            }

            if (this.UpdatedDateFrom != null)
            {
                query = query.Where(x => x.UpdatedDate.Value.Date >= UpdatedDateFrom);
            }

            if (this.UpdatedDateTo != null)
            {
                query = query.Where(x => x.UpdatedDate.Value.Date <= UpdatedDateTo);
            }

            return query;
        }

        #endregion
    }
}