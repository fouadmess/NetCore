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
    using System.Collections.Generic;

    /// <summary>
    /// The OwnableEntityViewModel class
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class OwnableEntityViewModel<TKey> : AuditEntityViewModel<TKey>, IOwnableEntityViewModel, IPermissionEntityViewModel
    {
        /// <summary>
        /// The OwnedByUser Property
        /// </summary>
        [ReadOnly]
        public virtual AuditUserViewModel OwnedByUser { get; set; }
        
        /// <summary>
        /// Gets or sets the OwnedByUserId
        /// </summary>
        public int? OwnedByUserId { get; set; }

        /// <summary>
        /// Gets or sets the Permissions
        /// </summary>
        [ReadOnly]
        public IDictionary<string, bool> Permissions { get; set; } = new Dictionary<string, bool>();
    }
}