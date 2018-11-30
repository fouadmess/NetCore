///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.ViewModel
{
    using System;

    /// <summary>
    /// The AuditEntityViewModel class
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class AuditEntityViewModel<TKey> : BaseEntityViewModel<TKey>, IAuditEntityViewModel
    {
        /// <summary>
        /// The CreatedBy Property
        /// </summary>
        public virtual string CreatedBy { get; set; }

        /// <summary>
        /// The CreatedDate Property
        /// </summary>
        [ReadOnly]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// The UpdatedByUser Property
        /// </summary>
        public virtual string UpdatedBy { get; set; }

        /// <summary>
        /// The UpdatedDate Property
        /// </summary>
        [ReadOnly]
        public DateTime? UpdatedDate { get; set; }
    }
}