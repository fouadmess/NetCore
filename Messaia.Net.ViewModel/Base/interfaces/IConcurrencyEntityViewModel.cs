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
    /// <summary>
    /// The IConcurrencyEntityViewModel interface
    /// </summary>
    public interface IConcurrencyEntityViewModel
    {
        /// <summary>
        /// Gets or sets the Version
        /// </summary>
        int Version { get; set; }

        /// <summary>
        /// Gets or sets the ConcurrencyStamp
        /// A random value that should change whenever a role is persisted to the store
        /// </summary>
        string ConcurrencyStamp { get; set; }
    }
}