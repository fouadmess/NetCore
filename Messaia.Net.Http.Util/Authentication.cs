///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 20:33:31
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Http.Util
{
    /// <summary>
    /// Authentication class.
    /// </summary>
    public class Authentication
    {
        /// <summary>
        /// Gets or sets the AuthUrl
        /// </summary>
        public string AuthUrl { get; set; }

        /// <summary>
        /// Gets or sets the AuthClient
        /// </summary>
        public string AuthClient { get; set; }

        /// <summary>
        /// Gets or sets the AuthSecret
        /// </summary>
        public string AuthSecret { get; set; }

        /// <summary>
        /// Gets or sets the ApiScope
        /// </summary>
        public string ApiScope { get; set; }

        /// <summary>
        /// Gets or sets the UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the Password
        /// </summary>
        public string Password { get; set; }
    }
}