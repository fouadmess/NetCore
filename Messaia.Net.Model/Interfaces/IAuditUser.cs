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
    /// <summary>
    /// IAuditUser class.
    /// </summary>
    public interface IAuditUser : IEntity<int>
    {
        /// <summary>
        /// Gets or sets the UserName
        /// </summary>
        string UserName { get; set; }
    }
}