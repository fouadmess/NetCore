///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Model
{
    using System;

    /// <summary>
    /// A generic base Entity class.
    /// </summary>
    /// <typeparam name="TKey">The primary key type</typeparam>
    public abstract class BaseEntity<TKey> : IEntity<TKey>, IConcurrencyEntity
    {
        /// <summary>
        /// Gets or sets the Id
        /// Entity Id
        /// </summary>
        public virtual TKey Id { get; set; }

        /// <summary>
        /// Gets or sets the Version
        /// </summary>
        public virtual int Version { get; set; }

        /// <summary>
        /// Gets or sets the ConcurrencyStamp
        /// A random value that should change whenever a role is persisted to the store
        /// </summary>
        public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    }
}