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
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Messaia.Net.Model;

    /// <summary>
    /// The User class
    /// </summary>
    public class User : IdentityUser<int>, IAuditUser, IAuditEntity
    {
        /// <summary>
        /// A Backing Field for display name
        /// </summary>
        private string _displayName;

        /// <summary>
        /// Gets or sets the Salutation
        /// </summary>
        public virtual Salutation Salutation { get; set; }

        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets or sets the FirstName
        /// </summary>
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the MiddleName
        /// </summary>
        public virtual string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the LastName
        /// </summary>
        public virtual string LastName { get; set; }

        /// <summary>
        /// Gets or sets the DisplayName
        /// </summary>
        public virtual string DisplayName
        {
            get { return $"{FirstName} {LastName}"; }
            set { this._displayName = $"{FirstName} {LastName}"; }
        }

        /// <summary>
        /// Gets or sets the MobilePhoneNumber
        /// </summary>
        public virtual string MobilePhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the Birthday
        /// </summary>
        public virtual DateTime? Birthday { get; set; }

        /// <summary>
        /// Gets or sets the Picture
        /// </summary>
        public virtual string Picture { get; set; }

        /// <summary>
        /// Gets or sets the Address
        /// </summary>
        public virtual Address Address { get; set; }

        /// <summary>
        /// Gets or sets the CreatedBy
        /// </summary>
        public virtual string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate
        /// </summary>
        public virtual DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the UpdatedBy
        /// </summary>
        public virtual string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedDate
        /// </summary>
        public virtual DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the Enabled
        /// </summary>
        public virtual bool Enabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the Version
        /// </summary>
        public virtual int Version { get; set; }

        /// <summary>
        /// Gets or sets the Password
        /// </summary>
        [NotMapped]
        public virtual string Password { get; set; }

        /// <summary>
        /// Gets or sets the RoleNames
        /// </summary>
        [NotMapped]
        public string[] RoleNames { get; set; }

        /// <summary>
        /// Gets or sets the IsCurrent
        /// </summary>
        [NotMapped]
        public bool IsCurrent { get; set; }

        /// <summary>
        /// Gets or sets the Params
        /// </summary>
        [NotMapped]
        public dynamic Params { get; set; }

        /// <summary>
        /// Navigation property for the roles this user belongs to.
        /// </summary>
        public virtual ICollection<UserRole> Roles { get; } = new List<UserRole>();

        /// <summary>
        /// Navigation property for the claims this user possesses.
        /// </summary>
        public virtual ICollection<UserClaim> Claims { get; } = new List<UserClaim>();

        /// <summary>
        /// Navigation property for this users login accounts.
        /// </summary>
        public virtual ICollection<UserLogin> Logins { get; } = new List<UserLogin>();

        /// <summary>
        /// Navigation property for this users login tokens.
        /// </summary>
        public virtual ICollection<UserToken> Tokens { get; } = new List<UserToken>();
    }
}