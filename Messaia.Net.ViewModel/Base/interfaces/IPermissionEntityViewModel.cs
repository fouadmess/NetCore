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
    /// The IPermissionEntityViewModel interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPermissionEntityViewModel
    {
        /// <summary>
        /// Gets or sets the Permissions
        /// </summary>
        IDictionary<string, bool> Permissions { get; set; }
    }
}