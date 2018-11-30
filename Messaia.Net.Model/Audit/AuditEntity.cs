///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Model
{
    using System;

    /// <summary>
    /// AuditEntity class.
    /// </summary>
    /// <typeparam name="TKey">The primary key type</typeparam>
    public class AuditEntity<TKey> : BaseEntity<TKey>, IAuditEntity
    {
        /// <summary>
        /// Gets or sets the CreatedBy
        /// </summary>
        public virtual string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate
        /// </summary>
        public virtual DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedByUser
        /// </summary>
        public virtual string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedDate
        /// </summary>
        public virtual DateTime? UpdatedDate { get; set; }
    }
}