﻿///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Observable.Impl
{
    /// <summary>
    /// BaseCommand class
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseCommand<TEntity> : IEntityCommand<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets or sets the Entity
        /// </summary>
        [ArgumentOrder(0)]
        public TEntity Entity { get; set; }
    }
}