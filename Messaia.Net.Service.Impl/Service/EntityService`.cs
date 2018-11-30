///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Service.Impl
{
    using Messaia.Net.Repository;

    /// <summary>
    /// The EntityService interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EntityService<TEntity> : EntityService<TEntity, IGenericRepository<TEntity>> where TEntity : class, new()
    {
        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="EntityService"/> class.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="unitOfWork"></param>
        public EntityService(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork) { }

        #endregion
    }
}