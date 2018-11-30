///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Messaia.Net.Common;
    using Messaia.Net.Observable;
    using Messaia.Net.Pagination;

    /// <summary>
    /// Repository interface
    /// </summary>
    /// <typeparam name="TEntity">Entity type this repository works on</typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Queryable
        /// </summary>
        IQueryable<TEntity> Queryable { get; }

        /// <summary>
        /// Gets or sets the Observers
        /// </summary>
        IPriorityQueue<IObserver<ICommand>> Observers { get; set; }

        #endregion

        #region Methods

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
        void Reload(TEntity entity);

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

        /// <summary>
        /// Specifies related entities to include in the query results. The navigation property
        /// to be included is specified starting with the type of entity being queried (TEntity).
        /// If you wish to include additional types based on the navigation properties of
        /// the type being included, then chain a call to ThenInclude
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="navigationPropertyPath">Properties to include</param>
        /// <returns></returns>
        IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] navigationPropertyPath);

        /// <summary>
        /// Returns a querable
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        IQueryable<TEntity> GetSingleQueryable();

        /// <summary>
        /// Returns a querable
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <paramref name="filter"/>
        /// <returns></returns>
        IQueryable<TEntity> GetAllQueryable(IFilter<TEntity> filter = null);

        /// <summary>
        /// Finds an entity with the given primary key values. If an entity with the given
        /// primary key values is being tracked by the context, then it is returned immediately
        /// without making a request to the database. Otherwise, a query is made to the dataabse
        /// for an entity with the given primary key values and this entity, if found, is
        /// attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="keyValues">The primary keys values.</param>
        /// <returns></returns>
        TEntity Find(params object[] keyValues);

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
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="notifyObservers">If true, the subscribed observers will be notified</param>
        /// <param name="trackable">If true, the entity will be tracked</param>
        /// <returns></returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate, bool notifyObservers = true, bool trackable = true);

        /// <summary>
        /// Returns a single entity based on the specified predicate.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="notifyObservers">If true, the subscribed observers will be notified</param>
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
        /// Get all entities as IEnumerable<TEntity>.
        /// </summary>
        /// <paramref name="filter"/>
        /// <paramref name="notifyObservers"/>
        /// <paramref name="trackable"/>
        /// <returns>Type: IEnumerable<TEntity></returns>
        List<TEntity> GetList(IFilter<TEntity> filter = null, bool notifyObservers = true, bool trackable = true);

        /// <summary>
        /// Get all entities asyncly.
        /// </summary>
        /// <paramref name="filter"/>
        /// <paramref name="notifyObservers"/>
        /// <paramref name="trackable"/>
        /// <returns>Type: Task<IList<TEntity>></returns>
        Task<List<TEntity>> GetListAsync(IFilter<TEntity> filter = null, bool notifyObservers = true, bool trackable = true);

        /// <summary>
        /// Returns a number that represents how many elements in the specified sequence satisfy a condition.
        /// </summary>
        /// <paramref name="filter"/>
        /// <paramref name="notifyObservers"/>
        /// <returns>Type: Task<int></returns>
        int Count(IFilter<TEntity> filter = null, bool notifyObservers = true);

        /// <summary>
        /// Returns a number that represents how many elements in the specified sequence satisfy a condition.
        /// </summary>
        /// <paramref name="filter"/>
        /// <paramref name="notifyObservers"/>
        /// <returns>Type: Task<int></returns>
        int Count(Expression<Func<TEntity, bool>> predicate, bool notifyObservers = true);

        /// <summary>
        /// Returns a number that represents how many elements in the specified sequence satisfy a condition.
        /// </summary>
        /// <paramref name="filter"/>
        /// <paramref name="notifyObservers"/>
        /// <returns>Type: Task<int></returns>
        Task<int> CountAsync(IFilter<TEntity> filter = null, bool notifyObservers = true);

        /// <summary>
        /// Returns a number that represents how many elements in the specified sequence satisfy a condition.
        /// </summary>
        /// <paramref name="filter"/>
        /// <paramref name="notifyObservers"/>
        /// <returns>Type: Task<int></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, bool notifyObservers = true);

        /// <summary>
        /// Gets pagination
        /// </summary>
        /// <paramref name="page"/>
        /// <paramref name="pageSize"/>
        /// <paramref name="notifyObservers"/>
        /// <paramref name="trackable"/>
        /// <returns>Type: IEnumerable<TEntity></returns>
        IPagination<TEntity> GetList(int page, int pageSize, IFilter<TEntity> filter = null, bool notifyObservers = true, bool trackable = true);

        /// <summary>
        ///  Begins tracking the given entity in the Microsoft.Data.Entity.EntityState.Added state such 
        ///  that it will be inserted into the database when Microsoft.Data.Entity.DbContext.SaveChanges is called.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>Type: TEntity</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        ///  Begins tracking the given entity in the Microsoft.Data.Entity.EntityState.Added state such 
        ///  that it will be inserted into the database when Microsoft.Data.Entity.DbContext.SaveChanges is called.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>Type: TEntity</returns>
        TEntity AddOrUpdate(TEntity entity);

        /// <summary>
        ///  Begins tracking the given entities in the Microsoft.Data.Entity.EntityState.Added state such 
        ///  that it will be inserted into the database when Microsoft.Data.Entity.DbContext.SaveChanges is called.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        /// <returns>Type: TEntity</returns>
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        ///  Begins tracking the given entities in the Microsoft.Data.Entity.EntityState.Added state such 
        ///  that it will be inserted into the database when Microsoft.Data.Entity.DbContext.SaveChanges is called.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
        /// <returns>Type: TEntity</returns>
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///  Begins tracking the given entity in the Microsoft.Data.Entity.EntityState.Modified state such 
        ///  that it will be update in the database when Microsoft.Data.Entity.DbContext.SaveChanges is called.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>Type: Variance - The changed properties</returns>
        List<Variance> Edit(TEntity entity);

        /// <summary>
        ///  Begins tracking the given entity in the Microsoft.Data.Entity.EntityState.Deleted state such 
        ///  that it will be delete in the database when Microsoft.Data.Entity.DbContext.SaveChanges is called.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(TEntity entity);

        #region Helpers

        /// <summary>
        /// Subscribes observers
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        IDisposable Subscribe(IObserver<ICommand> observer);

        /// <summary>
        /// Subscribes observers
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        IDisposable Subscribe(IObserver<ICommand> observer, float priority = float.MaxValue);

        #endregion

        #endregion
    }
}