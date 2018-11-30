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
    /// The IOwnableEntity interface
    /// </summary>
    public interface IOwnableEntity
    {
        /// <summary>
        /// Gets or sets the OwnedByUserId
        /// </summary>
        int? OwnedByUserId { get; set; }

        /// <summary>
        /// Gets or sets the OwnedDate
        /// </summary>
        DateTime OwnedDate { get; set; }
    }
}