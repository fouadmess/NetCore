///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Security
{
    using Microsoft.AspNetCore.Authorization;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Messaia.Net.Http;

    /// <summary>
    /// A helper class to handle authorization.
    /// </summary>
    public static class AuthorizationHelper
    {
        #region Properties

        /// <summary>
        /// The authorizationService field
        /// </summary>
        private static IAuthorizationService authorizationService = HttpContextHelper.GetService<IAuthorizationService>();

        /// <summary>
        /// Gets or sets the User
        /// </summary>
        public static ClaimsPrincipal User { get { return PrincipalHelper.User; } }

        /// <summary>
        /// Gets or sets the UserId
        /// </summary>
        public static int UserId { get { return PrincipalHelper.UserId; } }

        /// <summary>
        /// Gets or sets the IsAuthenticated
        /// </summary>
        public static bool IsAuthenticated { get { return User != null && UserId > 0; } }

        /// <summary>
        /// Gets or sets the IsAdmin
        /// </summary>
        public static bool IsAdmin
        {
            get
            {
                return new string[] { "Admin", "Administrator" }.Any(x => (bool)User?.HasClaim(ClaimsTypes.Permissions, x));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if a user meets a specific requirement for the specified resource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        public static async Task<AuthorizationResult> IsAuthorizedAsync(object resource, IAuthorizationRequirement requirement)
        {
            return await authorizationService?.AuthorizeAsync(User, resource, requirement);
        }

        /// <summary>
        /// Checks if a user meets a specific requirement for the specified resource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        public static bool IsAuthorized(object resource, IAuthorizationRequirement requirement)
        {
            return IsAuthorizedAsync(resource, requirement).Result.Succeeded;
        }

        /// <summary>
        /// Checks if the authenticated user has the specified permission claims.
        /// </summary>
        /// <param name="permissions">The permissions to check</param>
        /// <returns>Type: boolean</returns>
        public static bool IsPermitted(params string[] permissions)
        {
            /* Not authenticated */
            if (User == null)
            {
                return false;
            }

            /* Permission needed */
            if (IsAdmin || (permissions == null || permissions.Length == 0))
            {
                return true;
            }

            return User.Claims.Any(x => x.Type == ClaimsTypes.Permissions && permissions.Any(y => y.Equals(x.Value)));
        }

        #endregion
    }
}