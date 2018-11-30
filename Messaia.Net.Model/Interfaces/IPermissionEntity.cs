///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// IPermissionEntity class.
    /// </summary>
    public interface IPermissionEntity
	{
        /// <summary>
        /// Gets or sets the Permissions
        /// </summary>
        IDictionary<string, bool> Permissions { get; set; }
    }
}