///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 18:32:19
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.MailService
{
    /// <summary>
    /// SmtpOptions class.
    /// </summary>
    public class SmtpOptions
    {
        /// <summary>
        /// Gets or sets the Server
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets the Port
        /// </summary>
        public int Port { get; set; } = 25;

        /// <summary>
        /// Gets or sets the UseSSL
        /// </summary>
        public bool UseSSL { get; set; }

        /// <summary>
        /// Gets or sets the FromMail
        /// </summary>
        public string FromMail { get; set; }

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