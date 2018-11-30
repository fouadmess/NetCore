///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 06:54:41
///   Copyright (©)   2017, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.MailService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;

    /// <summary>
    /// MailBuilder class.
    /// </summary>
    public class MailBuilder
    {
        #region Fields

        /// <summary>
        /// The sender name and address
        /// </summary>
        public MailAddress Sender { get; private set; }

        /// <summary>
        /// The recipeints addresses
        /// </summary>
        public MailAddressCollection Recipients { get; private set; } = new MailAddressCollection();

        /// <summary>
        /// The carbon copy addresses
        /// </summary>
        public MailAddressCollection CcRecipients { get; private set; } = new MailAddressCollection();

        /// <summary>
        /// The blind carbon copy addresses
        /// </summary>
        public MailAddressCollection BccRecipients { get; private set; } = new MailAddressCollection();

        /// <summary>
        /// The reply to list
        /// </summary>
        public MailAddressCollection ReplyToList { get; private set; } = new MailAddressCollection();

        /// <summary>
        /// The mail subject
        /// </summary>
        public string SubjectText { get; private set; }

        /// <summary>
        /// The mail body as html
        /// </summary>
        public string HtmlBody { get; private set; }

        /// <summary>
        /// Gets or sets the MailPriority
        /// </summary>
        public MailPriority MailPriority { get; set; } = MailPriority.Normal;

        /// <summary>
        /// The mail attachments
        /// </summary>
        public ICollection<Attachment> MailAttachments { get; private set; } = new List<Attachment>();

        /// <summary>
        /// Send completed handlers
        /// </summary>
        public ICollection<SendCompletedEventHandler> SendCompletedCallbacks { get; private set; } = new List<SendCompletedEventHandler>();

        #endregion

        #region Methods

        /// <summary>
        /// Sets sender address and name
        /// </summary>
        /// <param name="address">The address of the sender</param>
        /// <param name="name">The name of the sender</param>
        /// <returns></returns>
        public MailBuilder From(string address, string name = null)
        {
            this.Sender = new MailAddress(address, name, Encoding.UTF8);
            return this;
        }

        /// <summary>
        /// Adds a recipient address and name
        /// </summary>
        /// <param name="address">The address of the recipient</param>
        /// <param name="name">The name of the recipient</param>
        /// <returns></returns>
        public MailBuilder To(string address, string name = null)
        {
            this.Recipients.Add(new MailAddress(address, name, Encoding.UTF8));
            return this;
        }

        /// <summary>
        /// Adds a carbon copy address and name
        /// </summary>
        /// <param name="address">The address of the recipient</param>
        /// <param name="name">The name of the recipient</param>
        /// <returns></returns>
        public MailBuilder Cc(string address, string name = null)
        {
            this.CcRecipients.Add(new MailAddress(address, name, Encoding.UTF8));
            return this;
        }

        /// <summary>
        /// Adds a blind carbon copy address and name
        /// </summary>
        /// <param name="address">The address of the recipient</param>
        /// <param name="name">The name of the recipient</param>
        /// <returns></returns>
        public MailBuilder Bcc(string address, string name = null)
        {
            this.BccRecipients.Add(new MailAddress(address, name, Encoding.UTF8));
            return this;
        }

        /// <summary>
        /// Adds a list of addresses to reply to for the mail message.
        /// </summary>
        /// <param name="address">The address of the recipient</param>
        /// <param name="name">The name of the recipient</param>
        /// <param name="reset">Resets the reply to list</param>
        /// <returns></returns>
        public MailBuilder ReplyTo(string address, string name = null, bool reset = false)
        {
            if (reset)
            {
                this.ReplyToList.Clear();
            }

            this.ReplyToList.Add(new MailAddress(address, name, Encoding.Default));
            return this;
        }

        /// <summary>
        /// Sets mail subject
        /// </summary>
        /// <param name="subject">The subject</param>
        /// <returns></returns>
        public MailBuilder Subject(string subject)
        {
            this.SubjectText = subject;
            return this;
        }

        /// <summary>
        /// Sets mail body
        /// </summary>
        /// <param name="body">The body</param>
        /// <returns></returns>
        public MailBuilder Body(string body)
        {
            this.HtmlBody = body;
            return this;
        }

        /// <summary>
        /// Sets mail subject
        /// </summary>
        /// <param name="subject">The subject</param>
        /// <returns></returns>
        public MailBuilder Priority(MailPriority priority)
        {
            this.MailPriority = priority;
            return this;
        }

        /// <summary>
        /// Sets mail attachments
        /// </summary>
        /// <param name="attachments">The attachments</param>
        /// <returns></returns>
        public MailBuilder Attachments(ICollection<Attachment> attachments)
        {
            this.MailAttachments = attachments;
            return this;
        }

        /// <summary>
        /// Sets mail attachments
        /// </summary>
        /// <param name="attachment">The attachment</param>
        /// <returns></returns>
        public MailBuilder Attachment(Attachment attachment)
        {
            this.MailAttachments.Add(attachment);
            return this;
        }

        /// <summary>
        /// Adds mail attachments
        /// </summary>
        /// <param name="filepath">The path file</param>
        /// <param name="name">The name of the attachmant</param>
        /// <returns></returns>
        public MailBuilder Attachment(string filepath, string name = null)
        {
            var attachment = new Attachment(filepath);
            if (!string.IsNullOrWhiteSpace(name))
            {
                attachment.Name = name;
            }

            this.MailAttachments.Add(attachment);
            return this;
        }

        public MailBuilder Callback(SendCompletedEventHandler handler)
        {
            this.SendCompletedCallbacks.Add(handler);
            return this;
        }

        /// <summary>
        /// Builds the mime message
        /// </summary>
        /// <returns></returns>
        public MailMessage Build()
        {
            /* Construct the message */
            var message = new MailMessage()
            {
                Subject = this.SubjectText,
                From = this.Sender,
                Body = this.HtmlBody,
                Priority = this.MailPriority,
                BodyEncoding = Encoding.UTF8,
                SubjectEncoding = Encoding.UTF8
            };

            /* Add recipients */
            this.Recipients.ToList().ForEach(x => message.To.Add(x));

            /* Add carbon copy recipients */
            this.CcRecipients.ToList().ForEach(x => message.CC.Add(x));

            /* Add blind carbon copy recipients */
            this.BccRecipients.ToList().ForEach(x => message.Bcc.Add(x));

            /* Add reply to */
            this.ReplyToList.ToList().ForEach(x => message.ReplyToList.Add(x));

            /* Add attachments to the body builder, if any */
            this.MailAttachments?.ToList().ForEach(x => message.Attachments.Add(x));

            return message;
        }

        #endregion
    }
}