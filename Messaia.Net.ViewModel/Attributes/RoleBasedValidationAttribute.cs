///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 20:28:44
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.ViewModel
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Security.Claims;

    /// <summary>
    /// RoleBasedValidationAttribute class.
    /// </summary>
    public abstract class RoleBasedValidationAttribute : SelfContainedValidationAttribute
    {
        #region Fields

        /// <summary>
        /// The user roles
        /// </summary>
        private string roles;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the authorized roles.
        /// </summary>
        /// <value>
        /// The roles string.
        /// </value>
        /// <remarks>Multiple role names can be specified using the comma character as a separator.</remarks>
        public string Roles
        {
            get { return roles ?? string.Empty; }
            set
            {
                roles = value;
                RoleList = SplitString(value);
            }
        }

        /// <summary>
        /// Gets or sets the RoleList
        /// </summary>
        protected string[] RoleList { get; private set; } = new string[0];

        /// <summary>
        /// Gets or sets the UserRoles
        /// </summary>
        public string[] UserRoles
        {
            get
            {

                return null;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the attribute requires validation context.
        /// </summary>
        /// <returns><c>true</c> if the attribute requires validation context; otherwise, <c>false</c>.</returns>
        public override bool RequiresValidationContext { get { return true; } }

        #endregion

        /// <summary>
        /// Initializes an instance of the <see cref="RoleBasedValidationAttribute"/> class.
        /// </summary>
        /// <param name="roles">The roles to check</param>
        public RoleBasedValidationAttribute(string roles)
        {
            this.Roles = roles;
        }

        #region Helpers

        /// <summary>
        /// Splits the string on commas and removes any leading/trailing whitespace from each result item.
        /// </summary>
        /// <param name="original">The input string.</param>
        /// <returns>An array of strings parsed from the input <paramref name="original"/> string.</returns>
        internal string[] SplitString(string original)
        {
            if (string.IsNullOrEmpty(original))
            {
                return new string[0];
            }

            return (from piece in original.Split(',')
                    let trimmed = piece.Trim()
                    where !string.IsNullOrEmpty(trimmed)
                    select trimmed
                ).ToArray();
        }

        /// <summary>
        /// Gets all roles for current user
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        internal string[] GetUserRoles(ValidationContext validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }

            /* Get http context */
            var httpContext = (validationContext.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor)?.HttpContext;
            if (httpContext == null)
            {
                return new string[0];
            }

            return httpContext.User?.Claims?
                .Where(x => x.Type.Equals(ClaimTypes.Role) || x.Type.Equals("role"))
                .Select(x => x.Value)
                .ToArray();
        }

        #endregion
    }
}