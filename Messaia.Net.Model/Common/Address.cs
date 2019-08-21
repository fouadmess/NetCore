///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 02:01:32
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Model
{
    /// <summary>
    /// Address class.
    /// </summary>
    public class Address<TKey> : IEntity<TKey>
    {
        /// <summary>
        /// Gets or sets the Id
        /// Entity Id
        /// </summary>
        public virtual TKey Id { get; set; }

        /// <summary>
        /// Gets or sets the Salutation
        /// </summary>
        public virtual Salutation Salutation { get; set; }

        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the FirstName
        /// </summary>
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the LastName
        /// </summary>
        public virtual string LastName { get; set; }

        /// <summary>
        /// Gets or sets the Address1
        /// </summary>
        public virtual string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the Address2
        /// </summary>
        public virtual string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the Address3
        /// </summary>
        public virtual string Address3 { get; set; }

        /// <summary>
        /// Gets or sets the PostalCode
        /// </summary>
        public virtual string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the City
        /// </summary>
        public virtual string City { get; set; }

        /// <summary>
        /// Gets or sets the State
        /// </summary>
        public virtual string State { get; set; }

        /// <summary>
        /// Gets or sets the Country
        /// </summary>
        public virtual string Country { get; set; }

        /// <summary>
        /// Clones this object
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}