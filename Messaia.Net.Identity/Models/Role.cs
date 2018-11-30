///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Identity
{
    using Messaia.Net.Model;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The Role class
    /// </summary>
    public class Role : IdentityRole<int>, IEntity<int>, IAuditEntity
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets or sets the CreatedBy
        /// </summary>
        public virtual string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate
        /// </summary>
        public virtual DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedBy
        /// </summary>
        public virtual string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedDate
        /// </summary>
        public virtual DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the Locked
        /// </summary>
        public virtual bool Locked { get; set; }

        /// <summary>
        /// Gets or sets the Version
        /// </summary>
        public virtual int Version { get; set; }

        /// <summary>
        /// Navigation property for the users this role belongs to.
        /// </summary>
        public virtual ICollection<UserRole> Users { get; } = new List<UserRole>();

        /// <summary>
        /// Navigation property for the claims this role possesses.
        /// </summary>
        public virtual ICollection<RoleClaim> Claims { get; } = new List<RoleClaim>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="Role"/> class.
        /// </summary>
        public Role() { }

        /// <summary>
        /// Initializes an instance of the <see cref="Role"/> class.
        /// </summary>
        /// <param name="name">The role name.</param>
        public Role(string name)
        {
            this.Name = name;
        }

        #endregion
    }
}