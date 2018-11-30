///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 09:50:17
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Identity
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using System.Security.Claims;

    /// <summary>
    /// RoleStore class.
    /// </summary>
    public class RoleStore : RoleStore<Role, IdentityDbContext, int, UserRole, RoleClaim>
    {
        #region Fields


        #endregion

        #region Properties


        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="RoleStore"/> class.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="describer"></param>
        public RoleStore(IdentityDbContext context, IdentityErrorDescriber describer = null) : base(context, describer) { }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a entity representing a role claim.
        /// </summary>
        /// <param name="role">The associated role.</param>
        /// <param name="claim">The associated claim.</param>
        /// <returns>The role claim entity.</returns>
        protected override RoleClaim CreateRoleClaim(Role role, Claim claim)
        {
            return new RoleClaim { RoleId = role.Id, ClaimType = claim.Type, ClaimValue = claim.Value };
        }

        #endregion
    }
}