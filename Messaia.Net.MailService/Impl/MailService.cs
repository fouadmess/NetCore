///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 16:40:57
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.MailService
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;

    /// <summary>
    /// MailService class.
    /// </summary>
    public class MailService : IMailService
    {
        #region Fields

        /// <summary>
        /// The logger instance
        /// </summary>
        protected readonly ILogger logger;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the MailOptions
        /// </summary>
        public MailOptions MailOptions { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="MailService"/> class.
        /// <paramref name="mailOptions"/>
        /// </summary>
        public MailService(IOptions<MailOptions> mailOptions, ILogger<MailService> logger)
        {
            this.MailOptions = mailOptions.Value;
            this.logger = logger;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="MailService"/> class.
        /// <paramref name="mailOptions"/>
        /// </summary>
        public MailService(MailOptions mailOptions)
        {
            this.MailOptions = mailOptions ?? throw new ArgumentNullException(nameof(mailOptions));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends a message using a builder
        /// </summary>
        /// <param name="message">The mail builder</param>
        /// <returns></returns>
        public void SendAsync(MailBuilder builder)
        {
            if (MailOptions?.Smtp == null)
            {
                throw new ArgumentNullException(nameof(MailOptions.Smtp));
            }

            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            /* Add sender from the mail options, if not set */
            if (builder.Sender == null)
            {
                builder.From(MailOptions.Smtp.FromMail, MailOptions.From);
            }

            /* Build message */
            var message = builder.Build();
            message.IsBodyHtml = this.MailOptions.IsBodyHtml;

            /* Send the message asyncly. */
            this.SendAsync(message, builder.SendCompletedCallbacks);
        }

        /// <summary>
        /// Sends the specified message
        /// </summary>
        /// <param name="message">The mime message to send</param>
        /// <param name="callbacks">A list of callback method to register to the send compeleted handler</param>
        /// <returns></returns>
        public void SendAsync(MailMessage message, ICollection<SendCompletedEventHandler> callbacks = null)
        {
            /* Create an instance of the smtp client */
            var client = new SmtpClient(this.MailOptions.Smtp.Server, this.MailOptions.Smtp.Port)
            {
                EnableSsl = this.MailOptions.Smtp.UseSSL
            };

            /* Add reply to, if any */
            if (!string.IsNullOrWhiteSpace(this.MailOptions.ReplyTo) && message.ReplyToList.Count == 0)
            {
                message.ReplyToList.Add(new MailAddress(this.MailOptions.ReplyTo));
            }

            /* Authenticate using the specified user name and password */
            if (this.MailOptions.Authenticate)
            {
                client.Credentials = new NetworkCredential(this.MailOptions.Smtp.UserName, this.MailOptions.Smtp.Password);
            }

            /* Add send complete callback methods */
            if (callbacks != null && callbacks.Count > 0)
            {
                callbacks.ToList().ForEach(x => client.SendCompleted += x);
            }

            /* Set the method that is called back when the send operation ends. */
            client.SendCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    this.logger?.LogError("Email could not be sent: {ERROR}", e.Error.ToString());
                }

                message.Dispose();
                client.Dispose();
            };

            /*
            The userState can be any object that allows your callback method 
            to identify this send operation.
            */
            client.SendAsync(message, Guid.NewGuid());
        }

        #endregion
    }
}