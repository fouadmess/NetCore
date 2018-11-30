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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// OwnableEntity class.
    /// </summary>
    /// <typeparam name="TKey">The primary key type</typeparam>
    public class OwnableEntity<TKey> : AuditEntity<TKey>, IOwnableEntity, IPermissionEntity
    {
        /// <summary>
        /// Gets or sets the OwnedByUser
        /// </summary>
        public virtual IAuditUser OwnedByUser { get; private set; }

        /// <summary>
        /// Gets or sets the OwnedByUserId
        /// </summary>
        public virtual int? OwnedByUserId { get; set; }

        /// <summary>
        /// Gets or sets the OwnedDate
        /// </summary>
        public virtual DateTime OwnedDate { get; set; }

        /// <summary>
        /// Gets or sets the Permissions
        /// </summary>
        [NotMapped]
        public virtual IDictionary<string, bool> Permissions { get; set; } = new Dictionary<string, bool>();
    }
}