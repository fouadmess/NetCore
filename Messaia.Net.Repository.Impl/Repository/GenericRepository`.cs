///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 07:25:41
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Repository.Impl
{
    using Messaia.Net.Data;
    using Messaia.Net.Model;

    /// <summary>
    /// Generic repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public class GenericRepository<TEntity> : GenericRepository<int, TEntity> where TEntity : class, IEntity<int>
    {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContext">The dbContext object</param>
        /// </summary>
        public GenericRepository(IDbContext dbContext) : base(dbContext) { }

        #endregion
    }
}