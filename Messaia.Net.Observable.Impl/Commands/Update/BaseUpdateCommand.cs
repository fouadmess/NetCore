///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Observable.Impl
{
    using System.Collections.Generic;
    using Messaia.Net.Common;

    /// <summary>
    /// BaseUpdateCommand class
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseUpdateCommand<TEntity> : BaseCommand<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets or sets the Changes
        /// </summary>
        [ArgumentOrder(1)]
        public List<Variance> Changes { get; set; }
    }
}