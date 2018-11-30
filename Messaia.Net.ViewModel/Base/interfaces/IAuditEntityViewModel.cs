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
    /// The IAuditableIEntityViewModel interface
    /// </summary>
    public interface IAuditEntityViewModel
    {
        /// <summary>
        /// The CreatedByUser Property
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// The CreatedDate Property
        /// </summary>
        DateTime CreatedDate { get; set; }

        /// <summary>
        /// The UpdatedByUser Property
        /// </summary>
        string UpdatedBy { get; set; }

        /// <summary>
        /// The UpdatedDate Property
        /// </summary>
        DateTime? UpdatedDate { get; set; }
    }
}