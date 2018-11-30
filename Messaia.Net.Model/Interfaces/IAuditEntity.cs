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
    /// The IAuditableEntity interface
    /// </summary>
    public interface IAuditEntity
    {
        /// <summary>
        /// Gets or sets the CreatedBy
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate
        /// </summary>
        DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedBy
        /// </summary>
        string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedDate
        /// </summary>
        DateTime? UpdatedDate { get; set; }
    }
}