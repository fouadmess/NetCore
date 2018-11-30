///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 18:21:04
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.MailService
{
    /// <summary>
    /// MailOptions class.
    /// </summary>
    public class MailOptions
    {
        /// <summary>
        /// Gets or sets the From
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Gets or sets the IsBodyHtml
        /// </summary>
        public bool IsBodyHtml { get; set; } = true;

        /// <summary>
        /// Gets or sets the Authenticate
        /// </summary>
        public bool Authenticate { get; set; } = true;

        /// <summary>
        /// Gets or sets the ReplyTo
        /// </summary>
        public string ReplyTo { get; set; }

        /// <summary>
        /// Gets or sets the Service
        /// </summary>
        public SmtpOptions Smtp { get; set; }
    }
}