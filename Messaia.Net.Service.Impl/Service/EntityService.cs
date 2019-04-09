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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Messaia.Net.Observable;
    using Messaia.Net.Observable.Impl;
    using Messaia.Net.Pagination;
    using Messaia.Net.Repository;
    using Messaia.Net.Service;

    /// <summary>
    /// The EntityService interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TRepository"></typeparam>
    public class EntityService<TEntity, TRepository> : IEntityService<TEntity>
        where TEntity : class, new()
        where TRepository : IGenericRepository<TEntity>
    {
        #region Fields

        /// <summary>
        /// Gets or sets the Repository
        /// </summary>
        protected readonly TRepository repository;

        /// <summary>
        /// Gets or sets the UnitOfWork
        /// </summary>
        protected readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The observers list
        /// </summary>
        private readonly PriorityQueue<IObserver<ICommand>> observers = new PriorityQueue<IObserver<ICommand>>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="GenericService"/> class.
        /// <param name="repository">An instance of the repository.</param>
        /// <param name="unitOfWork">Unit of work for concrete implementation of data mapper.</param>
        /// </summary>
        public EntityService(TRepository repository, IUnitOfWork unitOfWork)
        {
            /* Check the repository object */
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        #endregion

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
        public async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await this.repository.FindAsync(keyValues);
        }

        /// <summary>
        /// Returns a single entity based on the specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="notifyObservers">If true, the registred obervers will be notified</param>
        /// <param name="trackable">If true, the entity will be tracked</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool notifyObservers = true, bool trackable = true)
        {
            return await this.repository.GetAsync(predicate, notifyObservers, trackable);
        }

        /// <summary>
        /// Gets all entities as List<TEntity>.
        /// </summary>
        /// <paramref name="predicate"/>
        /// <paramref name="notifyObservers"/>
        /// <paramref name="trackable"/>
        /// <returns>Type: IEnumerable<TEntity></returns>
        public virtual List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, bool notifyObservers = true, bool trackable = true)
        {
            return this.repository.GetList(predicate, notifyObservers, trackable);
        }

        /// <summary>
        /// Gets all entities as List<TEntity>.
        /// </summary>
        /// <paramref name="predicate"/>
        /// <paramref name="notifyObservers"/>
        /// <paramref name="trackable"/>
        /// <returns>Type: IEnumerable<TEntity></returns>
        public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool notifyObservers = true, bool trackable = true)
        {
            return await this.repository.GetListAsync(predicate, notifyObservers, trackable);
        }

        /// <summary>
        /// Get all entities as asyncly.
        /// </summary>
        /// <paramref name="filter"/>
        /// <paramref name="notifyObservers"/>
        /// <paramref name="trackable"/>
        /// <returns>Type: Task<IList<TEntity>></returns>
        public virtual async Task<List<TEntity>> GetListAsync(IFilter<TEntity> filter = null, bool notifyObservers = true, bool trackable = true)
        {
            return await this.repository.GetListAsync(filter, notifyObservers, trackable);
        }

        /// <summary>
        /// Gets pagination
        /// </summary>
        /// <paramref name="page"/>
        /// <paramref name="pageSize"/>
        /// <paramref name="filter"/>
        /// <paramref name="notifyObservers"/>
        /// <paramref name="trackable"/>
        /// <returns>Type: IEnumerable<TEntity></returns>
        public virtual IPagination<TEntity> GetList(int page, int pageSize, IFilter<TEntity> filter = null, bool notifyObservers = true, bool trackable = true)
        {
            return this.repository.GetList(page, pageSize, filter, notifyObservers, trackable);
        }

        /// <summary>
        /// Returns a number that represents how many elements in the specified sequence satisfy a condition.
        /// </summary>
        /// <paramref name="filter"/>
        /// <returns>Type: Task<int></returns>
        public async Task<int> CountAsync(IFilter<TEntity> filter = null)
        {
            return await this.repository.CountAsync(filter);
        }

        /// <summary>
        /// Returns a number that represents how many elements in the specified sequence satisfy a condition.
        /// </summary>
        /// <paramref name="filter"/>
        /// <returns>Type: Task<int></returns>
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.repository.CountAsync(predicate);
        }

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
        public virtual async Task<ServiceResult> CreateAsync(TEntity entity, bool notifyObservers = true, bool updateOnConflict = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            /* Check the entity object */
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            /* Notify subscribed observers */
            if (notifyObservers)
            {
                this.Notify(new BeforeCreateCommand<TEntity> { Entity = entity });
            }

            /* Begins tracking the given entity in the Microsoft.Data.Entity.EntityState.Added state */
            if (updateOnConflict)
            {
                this.repository.AddOrUpdate(entity);
            }
            else
            {
                this.repository.Add(entity);
            }

            /* Commit the changes */
            await this.unitOfWork.SaveChangesAsync(cancellationToken);

            /* Notify subscribed observers */
            if (notifyObservers)
            {
                this.Notify(new AfterCreateCommand<TEntity> { Entity = entity });
            }

            return ServiceResult.Success;
        }

        /// <summary>
        ///  Inserts multiple entities into the database.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        public virtual async Task<ServiceResult> CreateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            /* Check the entity object */
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            var step = 1000;
            var count = entities.Count();
            for (int i = 0; i < count; i += step)
            {
                /* Begins tracking the given entity in the Microsoft.Data.Entity.EntityState.Added state */
                await this.repository.AddRangeAsync(entities.Skip(i).Take(step).ToArray(), cancellationToken);

                /* Commit the changes */
                await this.unitOfWork.SaveChangesAsync();
            }

            return ServiceResult.Success;
        }

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
        ///     Type: System.Threading.Tasks.Task<Int32>
        ///     A task that represents the asynchronous update operation.
        /// </returns>
        public virtual async Task<ServiceResult> UpdateAsync(TEntity entity, bool notifyObservers = true, CancellationToken cancellationToken = default(CancellationToken))
        {
            /* Check the entity object */
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            /* Begins tracking the given entity in the Microsoft.Data.Entity.EntityState.Modified state */
            var changes = this.repository.Edit(entity);

            /* Notify subscribed observers */
            if (notifyObservers)
            {
                this.Notify(new BeforeUpdateCommand<TEntity> { Entity = entity, Changes = changes });
            }

            /* Commit the changes */
            await this.unitOfWork.SaveChangesAsync(cancellationToken);

            /* Notify subscribed observers */
            if (notifyObservers)
            {
                this.Notify(new AfterUpdateCommand<TEntity> { Entity = entity, Changes = changes });
            }

            return ServiceResult.Success;
        }

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
        ///     Type: System.Threading.Tasks.Task<Int32>
        ///     A task that represents the asynchronous delete operation.
        /// </returns>
        public virtual async Task<ServiceResult> DeleteAsync(TEntity entity, bool notifyObservers = true, CancellationToken cancellationToken = default(CancellationToken))
        {
            /* Check the entity object */
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            /* Notify subscribed observers */
            if (notifyObservers)
            {
                this.Notify(new BeforeDeleteCommand<TEntity> { Entity = entity });
            }

            /* Begins tracking the given entity in the Microsoft.Data.Entity.EntityState.Modified state */
            this.repository.Delete(entity);

            /* Commit the changes */
            await this.unitOfWork.SaveChangesAsync(cancellationToken);

            /* Notify subscribed observers */
            if (notifyObservers)
            {
                this.Notify(new AfterDeleteCommand<TEntity> { Entity = entity });
            }

            return ServiceResult.Success;
        }

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
        ///     Type: System.Threading.Tasks.Task<Int32>
        ///     A task that represents the asynchronous delete operation.
        /// </returns>
        public virtual async Task<ServiceResult> DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool notifyObservers = true, CancellationToken cancellationToken = default(CancellationToken))
        {
            /* Fetch the entity */
            var entity = await this.repository.GetAsync(predicate);
            if (entity == null)
            {
                return ServiceResult.Failed(new ServiceError(nameof(DeleteAsync), "Entity not found"));
            }

            /* Notify subscribed observers */
            if (notifyObservers)
            {
                this.Notify(new BeforeDeleteCommand<TEntity> { Entity = entity });
            }

            /* Begins tracking the given entity in the Microsoft.Data.Entity.EntityState.Modified state */
            this.repository.Delete(entity);

            /* Commit the changes */
            await this.unitOfWork.SaveChangesAsync(cancellationToken);

            /* Notify subscribed observers */
            if (notifyObservers)
            {
                this.Notify(new AfterDeleteCommand<TEntity> { Entity = entity });
            }

            return ServiceResult.Success;
        }

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
        public virtual async Task ReloadAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            await this.repository.ReloadAsync(entity);
        }

        /// <summary>
        /// Detaches the entity from the current context
        /// </summary>
        /// <param name="entity">The entity to detach</param>
        public virtual void Detach(TEntity entity)
        {
            this.repository.Detach(entity);
        }

        /// <summary>
        /// Notifies 
        /// </summary>
        /// <param name="command"></param>
        protected void Notify(ICommand command)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(command);
            }
        }

        /// <summary>
        /// Subscribes observers
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public IDisposable Subscribe(IObserver<ICommand> observer)
        {
            return this.Subscribe(observer, float.MaxValue);
        }

        /// <summary>
        /// Subscribes observers
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public IDisposable Subscribe(IObserver<ICommand> observer, float priority = float.MaxValue)
        {
            /* Check whether observer is already registered. If not, add it */
            if (!observers.Contains(observer))
            {
                observers.Enqueue(observer, priority);
            }

            /* Subscribe the observer in the repository */
            this.repository.Subscribe(observer, priority);

            return new Unsubscriber<ICommand>(observers, observer);
        }

        #endregion

        #endregion
    }
}