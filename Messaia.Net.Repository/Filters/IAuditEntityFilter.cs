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

    /// <summary>
    /// The IAuditEntityFilter interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IAuditEntityFilter<TEntity> : IFilter<TEntity> where TEntity : class
    {
        #region Properties

        /// <summary>
        /// Gets or sets the CreatedByUserId
        /// </summary>
        int? CreatedByUserId { get; set; }

        /// <summary>
        /// Gets or sets the CreatedByUser
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedByUserId
        /// </summary>
        int? UpdatedByUserId { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedByUser
        /// </summary>
        string UpdatedBy { get; set; }
        
        /// <summary>
        /// Gets or sets the CreatedDate
        /// </summary>
        DateTime? CreatedDate { get; set; }
        
        /// <summary>
        /// Gets or sets the UpdatedDate
        /// </summary>
        DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDateFrom
        /// </summary>
        DateTime? CreatedDateFrom { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDateTo
        /// </summary>
        DateTime? CreatedDateTo { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedDateFrom
        /// </summary>
        DateTime? UpdatedDateFrom { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedDateTo
        /// </summary>
        DateTime? UpdatedDateTo { get; set; }

        #endregion
    }
}