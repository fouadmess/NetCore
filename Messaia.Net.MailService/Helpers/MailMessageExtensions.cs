///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 04:42:48
///   Copyright (©)   2017, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace System.Net.Mail
{
    using System;
    using System.IO;
    using System.Net.Mime;
    using System.Reflection;

    /// <summary>
    /// MailMessageExtensions class.
    /// </summary>
    public static class MailMessageExtensions
	{
        /// <summary>
        /// Convert a MailMessage to stream
        /// </summary>
        /// <param name="message">The message to convert</param>
        /// <returns></returns>
        public static Stream ToStream(this MailMessage message)
        {
            /* Create an instance of MemoryStream to store the content to */
            var memoryStream = new MemoryStream();

            /* Build binding flags */
            var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;

            /* Get reflection info for MailWriter contructor and constract it with our stream */
            var mailWriter = typeof(SmtpClient).Assembly
                .GetType("System.Net.Mail.MailWriter")?
                .GetConstructor(bindingFlags, null, new Type[] { typeof(Stream) }, null)?
                .Invoke(new object[] { memoryStream });

            /* Get reflection info for Send() method on MailMessage and call it passing in MailWriter */
            typeof(MailMessage)
                .GetMethod("Send", bindingFlags)
                .Invoke(message, bindingFlags, null, new object[] { mailWriter, true, true }, null);

            /* Set the position within the current stream to the begin */
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }

        /// <summary>
        /// Converts a MailMessage to an Attachment object
        /// </summary>
        /// <param name="message">The message to to convert</param>
        /// <returns></returns>
        public static Attachment ToAttachment(this MailMessage message)
        {
            return new Attachment(message.ToStream(), $"{message.Subject}.eml")
            {
                ContentType = new ContentType("message/rfc822"),
                TransferEncoding = TransferEncoding.SevenBit
            };
        }
    }
}