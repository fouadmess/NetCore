///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 16:35:59
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.MailService
{
    using System.Collections.Generic;
    using System.Net.Mail;

    /// <summary>
    /// IMailService class.
    /// </summary>
    public interface IMailService
    {
        #region Methods

        /// <summary>
        /// Sends a message using a builder
        /// </summary>
        /// <param name="message">The mail builder</param>
        /// <returns></returns>
        void SendAsync(MailBuilder builder);

        /// <summary>
        /// Sends the specified message
        /// </summary>
        /// <param name="message">The mime message to send</param>
        /// <param name="callbacks">A list of callback method to register to the send compeleted handler</param>
        /// <returns></returns>
        void SendAsync(MailMessage message, ICollection<SendCompletedEventHandler> callbacks = null);

        #endregion
    }
}