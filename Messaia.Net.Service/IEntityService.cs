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
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Messaia.Net.Observable;
    using Messaia.Net.Pagination;
    using Messaia.Net.Repository;

    /// <summary>
    /// The IEntityService interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEntityService<TEntity> : IObservableService<ICommand, float> where TEntity : class
    {
        #region Methods

        /// <summary>
        /// Finds an entity with the given primary key values. If an entity with the given
        /// primary key values is being tracked by the context, then it is returned immediately
        /// without making a request to the database. Otherwise, a query is made to the dataabse
        /// for an entity with the given primary key values and this entity, if found, is
        /// attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="keyValues">The primary keys values.</param>
        /// <returns></returns>
        Task<TEntity> FindAsync(params object[] keyValues);

        /// <summary>
        /// Returns a single entity based on the specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="predicate">The predicate to apply to the query.</param>
        /// <param name="notifyObservers">If true, the registred obervers will be notified</param>
        /// <param name="trackable">If true, the entity will be tracked</param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool notifyObservers = true, bool trackable = true);

        /// <summary>
        /// Gets all entities as List<TEntity>.
        /// </summary>
        /// <paramref name="predicate"/>
        /// <paramref name="notifyObservers"/>
        /// <paramref name="trackable"/>
        /// <returns>Type: IEnumerable<TEntity></returns>
        List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, bool notifyObservers = true, bool trackable = true);

        /// <summary>
        /// Gets all entities as List<TEntity>.
        /// </summary>
        /// <paramref name="predicate"/>
        /// <paramref name="notifyObservers"/>
        /// <paramref name="trackable"/>
        /// <returns>Type: IEnumerable<TEntity></returns>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool notifyObservers = true, bool trackable = true);

        /// <summary>
        /// Gets all entities as asyncly.
        /// </summary>
        /// <paramref name="filter"/>
        /// <paramref name="notifyObservers"/>
        /// <paramref name="trackable"/>
        /// <returns>Type: Task<IList<TEntity>></returns>
        Task<List<TEntity>> GetListAsync(IFilter<TEntity> filter = null, bool notifyObservers = true, bool trackable = true);

        /// <summary>
        /// Gets pagination
        /// </summary>
        /// <paramref name="page"/>
        /// <paramref name="pageSize"/>
        /// <paramref name="filter"/>
        /// <paramref name="notifyObservers"/>
        /// <paramref name="trackable"/>
        /// <returns>Type: IEnumerable<TEntity></returns>
        IPagination<TEntity> GetList(int page, int pageSize, IFilter<TEntity> filter = null, bool notifyObservers = true, bool trackable = true);

        /// <summary>
        /// Returns a number that represents how many elements in the specified sequence satisfy a condition.
        /// </summary>
        /// <paramref name="filter"/>
        /// <returns>Type: Task<int></returns>
        Task<int> CountAsync(IFilter<TEntity> filter = null);

        /// <summary>
        /// Returns a number that represents how many elements in the specified sequence satisfy a condition.
        /// </summary>
        /// <paramref name="filter"/>
        /// <returns>Type: Task<int></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///  Inserts an entity into the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <param name="notifyObservers">If true, the registred obervers will be notified</param>
        /// <param name="updateOnConflict">Updates the entity on conflicts</param>
        /// <param name="cancellationToken">
        ///     Type: System.Threading.CancellationToken
        ///     A CancellationToken to observe while waiting for the task to complete.
        /// </param>        /// 
        /// <returns>
        ///     Type: System.Threading.Tasks.Task<ServiceResult>
        ///     A task that represents the asynchronous create operation.
        /// </returns>
        Task<ServiceResult> CreateAsync(TEntity entity, bool notifyObservers = true, bool updateOnConflict = false, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///  Inserts an entity into the database.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        /// <param name="cancellationToken">
        ///     Type: System.Threading.CancellationToken
        ///     A CancellationToken to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     Type: System.Threading.Tasks.Task<ServiceResult>
        ///     A task that represents the asynchronous create operation.
        /// </returns>
        Task<ServiceResult> CreateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///  Updates an entity in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="cancellationToken">
        ///     Type: System.Threading.CancellationToken
        ///     A CancellationToken to observe while waiting for the task to complete.
        /// </param>
        /// <param name="notifyObservers">If true, the registred obervers will be notified</param>
        /// <returns>
        ///     Type: System.Threading.Tasks.Task<ServiceResult>
        ///     A task that represents the asynchronous update operation.
        /// </returns>
        Task<ServiceResult> UpdateAsync(TEntity entity, bool notifyObservers = true, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///  Deletes an entity from the database.
        /// </summary>
        /// <param name="entity">The entity to Delete.</param>
        /// <param name="cancellationToken">
        ///     Type: System.Threading.CancellationToken
        ///     A CancellationToken to observe while waiting for the task to complete.
        /// </param>
        /// <param name="notifyObservers">If true, the registred obervers will be notified</param>
        /// <returns>
        ///     Type: System.Threading.Tasks.Task<ServiceResult>
        ///     A task that represents the asynchronous delete operation.
        /// </returns>
        Task<ServiceResult> DeleteAsync(TEntity entity, bool notifyObservers = true, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///  Deletes an entity from the database.
        /// </summary>
        /// <param name="predicate">The predicate to apply to the query.</param>
        /// <param name="cancellationToken">
        ///     Type: System.Threading.CancellationToken
        ///     A CancellationToken to observe while waiting for the task to complete.
        /// </param>
        /// <param name="notifyObservers">If true, the registred obervers will be notified</param>
        /// <returns>
        ///     Type: System.Threading.Tasks.Task<ServiceResult>
        ///     A task that represents the asynchronous delete operation.
        /// </returns>
        Task<ServiceResult> DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool notifyObservers = true, CancellationToken cancellationToken = default(CancellationToken));

        #region Helpers

        /// <summary>
        /// Reloads the entity from the database overwriting any property values with values
        /// from the database.
        /// The entity will be in the Microsoft.EntityFrameworkCore.EntityState.Unchanged
        /// state after calling this method, unless the entity does not exist in the database,
        /// in which case the entity will be Microsoft.EntityFrameworkCore.EntityState.Detached.
        /// Finally, calling Reload on an Microsoft.EntityFrameworkCore.EntityState.Added
        /// entity that does not exist in the database is a no-op. Note, however, that an
        /// Added entity may not yet have had its permanent key value created.
        /// </summary>
        /// <param name="entity">The entity to reload</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
        Task ReloadAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Detaches the entity from the current context
        /// </summary>
        /// <param name="entity">The entity to detach</param>
        void Detach(TEntity entity);

        #endregion

        #endregion
    }
}