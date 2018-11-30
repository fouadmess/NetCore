///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Messaia.Net.Model;
    using Messaia.Net.Repository;

    /// <summary>
    /// ILookupService interface.
    /// </summary>
    public interface ILookupService<TEntity> where TEntity : class, IEntity<int>, new()
    {
        #region Methods

        /// <summary>
        /// Returns a filtred list.
        /// </summary>
        /// <param name="filter">The lookup criteria</param>
        /// <returns></returns>
        Task<List<dynamic>> LookupAsync(IFilter<TEntity> filter);

        #endregion
    }
}