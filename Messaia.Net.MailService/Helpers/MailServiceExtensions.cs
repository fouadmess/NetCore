///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 18:23:56
///   Copyright (©)   2017, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.MailService
{
    using System;
    using System.Linq;
    using System.Net.Mail;

    /// <summary>
    /// MailServiceExtensions class.
    /// </summary>
    public static class MailServiceExtensions
    {
        /// <summary>
        /// Sends an email asynchronously
        /// </summary>
        /// <param name="email">The receiver email address</param>
        /// <param name="subject">The mail subject</param>
        /// <param name="body">The mail body</param>
        /// <param name="attachments">The Mail attachments</param>
        public static void SendAsync(this IMailService mailService, string email, string subject, string body, params Attachment[] attachments)
        {
            mailService.SendAsync(new string[] { email }, subject, body, attachments);
        }

        /// <summary>
        /// Sends an email asynchronously
        /// </summary>
        /// <param name="emails">The recipient email addresses</param>
        /// <param name="subject">The mail subject</param>
        /// <param name="body">The mail body</param>
        /// <param name="attachments">The Mail attachments</param>
        public static void SendAsync(this IMailService mailService, string[] emails, string subject, string body, params Attachment[] attachments)
        {
            if (emails == null || emails.Length == 0)
            {
                throw new ArgumentNullException(nameof(emails));
            }

            /* Create a new mail builder */
            var builder = new MailBuilder()
                .Subject(subject)
                .Body(body)
                .Attachments(attachments);

            /* Add recipients */
            emails?.ToList().ForEach(x => builder.To(x));

            /* Send the message asyncly. */
            mailService.SendAsync(builder);
        }
    }
}