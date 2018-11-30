///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Observable
{
    using System.Collections.Generic;

    /// <summary>
    /// ICollectionCommand interface
    /// </summary>
    public interface ICollectionCommand<TEntity> : ICommand where TEntity : class
    {
        /// <summary>
        /// Gets or sets the Entity
        /// </summary>
        ICollection<TEntity> Entities { get; set; }
    }
}