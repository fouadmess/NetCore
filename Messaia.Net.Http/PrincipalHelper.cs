///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 10:26:39
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Http
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    /// <summary>
    /// PrincipalHelper class.
    /// </summary>
    public static class PrincipalHelper
    {
        #region Properties

        /// <summary>
        /// Gets or sets the User
        /// </summary>
        public static ClaimsPrincipal User { get { return HttpContextHelper.HttpContext?.User; } }

        /// <summary>
        /// Gets or sets the UserId
        /// </summary>
        public static int UserId
        {
            get
            {
                int.TryParse(User?.Claims?
                    .Where(x => x.Type.Equals(ClaimTypes.NameIdentifier) || x.Type.Equals("sub")).Select(x => x.Value)
                    .FirstOrDefault(), out int userId);

                return userId;
            }
        }

        /// <summary>
        /// Gets or sets the Roles
        /// </summary>
        public static List<string> Roles
        {
            get
            {
                return User?.Claims?
                    .Where(x => x.Type.Equals(ClaimTypes.Role) || x.Type.Equals("role"))
                    .Select(x => x.Value)
                    .ToList();
            }
        }

        /// <summary>
        /// Gets or sets the Permissions
        /// </summary>
        public static List<string> Permissions
        {
            get
            {
                return User?.Claims?
                    .Where(x => x.Type.Equals("permissions"))
                    .Select(x => x.Value)
                    .ToList();
            }
        }

        #endregion
    }
}