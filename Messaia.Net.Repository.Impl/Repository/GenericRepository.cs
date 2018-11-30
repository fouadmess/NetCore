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
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Messaia.Net.Common;
    using Messaia.Net.Data;
    using Messaia.Net.Model;
    using Messaia.Net.Observable;
    using Messaia.Net.Observable.Impl;
    using Messaia.Net.Pagination;

    /// <summary>
    /// Generic repository
    /// </summary>
    /// <typeparam name="TKey">Primray key type</typeparam>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public class GenericRepository<TKey, TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity<TKey>
    {
        #region Fields

        /// <summary>
        /// The DbContext object
        /// </summary>
        protected DbContext dbContext;

        /// <summary>
        /// The dbSet field
        /// </summary>
        protected readonly DbSet<TEntity> dbSet;

        /// <summary>
        /// The types of properties beeing tracked by the EF
        /// </summary>
        protected Type[] trackedTypes = new Type[]
        {
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(string),
            typeof(decimal),
            typeof(double),
            typeof(short),
            typeof(bool),
            typeof(DateTime),
            typeof(TimeSpan),
            typeof(Enum)
        };

        #endregion

        #region Propperties

        /// <summary>
        /// Gets or sets the DbContext
        /// </summary>
        public DbContext DbContext { get { return this.dbContext; } }

        /// <summary>
        /// Gets or sets the MyProperty
        /// </summary>
        public DbSet<TEntity> DbSet { get { return this.dbSet; } }

        /// <summary>
        /// Gets or sets the Queryable
        /// </summary>
        public IQueryable<TEntity> Queryable { get { return this.dbSet?.AsQueryable(); } }

        /// <summary>
        /// Gets or sets the Observers
        /// </summary>
        public IPriorityQueue<IObserver<ICommand>> Observers { get; set; } = new PriorityQueue<IObserver<ICommand>>();

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContext">The dbContext object</param>
        /// </summary>
        public GenericRepository(IDbContext dbContext)
        {
            this.dbContext = dbContext as DbContext ?? throw new ArgumentNullException(nameof(dbContext));

            /* Set dbSet object */
            this.dbSet = this.dbContext.Set<TEntity>();
        }

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
        public virtual void Reload(TEntity entity)
        {
            this.dbContext.Entry(entity).Reload();
        }

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
            await this.dbContext.Entry(entity).ReloadAsync();
        }

        /// <summary>
        /// Detaches the entity from the current context
        /// </summary>
        /// <param name="entity">The entity to detach</param>
        public virtual void Detach(TEntity entity)
        {
            this.dbContext.Entry(entity).State = EntityState.Detached;
        }

        /// <summary>
        /// Specifies related entities to include in the query results. The navigation property
        /// to be included is specified starting with the type of entity being queried (TEntity).
        /// If you wish to include additional types based on the navigation properties of
        /// the type being included, then chain a call to ThenInclude
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="navigationPropertyPath">Properties to include</param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] navigationPropertyPath)
        {
            var query = this.Queryable;
            if (navigationPropertyPath != null)
            {
                query = navigationPropertyPath.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }

        /// <summary>
        /// Finds an entity with the given primary key values. If an entity with the given
        /// primary key values is being tracked by the context, then it is returned immediately
        /// without making a request to the database. Otherwise, a query is made to the dataabse
        /// for an entity with the given primary key values and this entity, if found, is
        /// attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="keyValues">The primary keys values.</param>
        /// <returns></returns>
        public TEntity Find(params object[] keyValues)
        {
            /* Notify subscribed observers */
            this.Notify(new BeforeReadCommand<TEntity>());

            /* Fetch the entity */
            var entity = this.DbSet.Find(keyValues);

            /* Notify subscribed observers */
            if (entity != null)
            {
                this.Notify(new AfterReadCommand<TEntity> { Entity = entity });
            }

            return entity;
        }

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
            /* Notify subscribed observers */
            this.Notify(new BeforeReadCommand<TEntity>());

            /* Fetch the entity */
            var entity = await this.DbSet.FindAsync(keyValues);

            /* Notify subscribed observers */
            if (entity != null)
            {
                this.Notify(new AfterReadCommand<TEntity> { Entity = entity });
            }

            return entity;
        }

        /// <summary>
        /// Returns a single entity based on the specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="notifyObservers">If true, the subscribed observers will be notified</param>
        /// <param name="trackable">If true, the entity will be tracked</param>
        /// <returns></returns>
        public TEntity Get(Expression<Func<TEntity, bool>> predicate, bool notifyObservers = true, bool trackable = true)
        {
            var query = this.GetSingleQueryable();
            if (!trackable)
            {
                query = query.AsNoTracking();
            }

            /* Notify subscribed observers */
            if (notifyObservers)
            {
                var command = new BeforeReadCommand<TEntity>() { Query = query };
                this.Notify(command);
                query = command.Query;
            }

            /* Fetch the entity */
            var entity = query.FirstOrDefault(predicate);

            /* Notify subscribed observers */
            if (entity != null && notifyObservers)
            {
                this.Notify(new AfterReadCommand<TEntity> { Entity = entity });
            }

            return entity;
        }

        /// <summary>
        /// Returns a single entity based on the specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="notifyObservers">If true, the subscribed observers will be notified</param>
        /// <param name="trackable">If true, the entity will be tracked</param>
        /// <returns></returns>
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool notifyObservers = true, bool trackable = true)
        {
            var query = this.GetSingleQueryable();
            if (!trackable)
            {
                query = query.AsNoTracking();
            }

            /* Notify subscribed observers */
            if (notifyObservers)
            {
                var command = new BeforeReadCommand<TEntity>() { Query = query };
                this.Notify(command);
                query = command.Query;
            }

            /* Fetch the entity */
            var entity = await query.FirstOrDefaultAsync(predicate);

            /* Notify subscribed observers */
            if (entity != null && notifyObservers)
            {
                this.Notify(new AfterReadCommand<TEntity> { Entity = entity });
            }

            return entity;
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
            var query = this.GetAllQueryable();
            if (!trackable)
            {
                query = query.AsNoTracking();
            }

            /* Notify subscribed observers */
            if (notifyObservers)
            {
                var command = new BeforeReadListCommand<TEntity> { Query = query };
                this.Notify(command);
                query = command.Query;
            }

            /* Get all entities */
            var entities = query.Where(predicate).ToList();

            /* Notify subscribed observers */
            if (entities.Count > 0 && notifyObservers)
            {
                this.Notify(new AfterReadListCommand<TEntity> { Entities = entities });
            }

            return entities;
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
            var query = this.GetAllQueryable();
            if (!trackable)
            {
                query = query.AsNoTracking();
            }

            /* Notify subscribed observers */
            if (notifyObservers)
            {
                var command = new BeforeReadListCommand<TEntity> { Query = query };
                this.Notify(command);
                query = command.Query;
            }

            /* Get all entities */
            var entities = await query.Where(predicate).ToListAsync();

            /* Notify subscribed observers */
            if (entities.Count > 0 && notifyObservers)
            {
                this.Notify(new AfterReadListCommand<TEntity> { Entities = entities });
            }

            return entities;
        }

        /// <summary>
        /// Gets all entities as List<TEntity>.
        /// </summary>
        /// <paramref name="filter"/>
        /// <paramref name="notifyObservers"/>
        /// <paramref name="trackable"/>
        /// <returns>Type: IEnumerable<TEntity></returns>
        public virtual List<TEntity> GetList(IFilter<TEntity> filter = null, bool notifyObservers = true, bool trackable = true)
        {
            var query = this.GetAllQueryable();
            if (!trackable)
            {
                query = query.AsNoTracking();
            }

            /* Notify subscribed observers */
            if (notifyObservers)
            {
                var command = new BeforeReadListCommand<TEntity> { Query = query };
                this.Notify(command);
                query = command.Query;
            }

            /* Get all entities */
            var entities = this.ApplyFilter(query, filter).ToList();

            /* Notify subscribed observers */
            if (entities.Count > 0 && notifyObservers)
            {
                this.Notify(new AfterReadListCommand<TEntity> { Entities = entities });
            }

            return entities;
        }

        /// <summary>
        /// Get all entities asyncly.
        /// </summary>
        /// <paramref name="filter"/>
        /// <paramref name="notifyObservers"/>
        /// <paramref name="trackable"/>
        /// <returns>Type: Task<IList<TEntity>></returns>
        public virtual async Task<List<TEntity>> GetListAsync(IFilter<TEntity> filter = null, bool notifyObservers = true, bool trackable = true)
        {
            var query = this.GetAllQueryable();
            if (!trackable)
            {
                query = query.AsNoTracking();
            }

            /* Notify subscribed observers */
            if (notifyObservers)
            {
                var command = new BeforeReadListCommand<TEntity> { Query = query };
                this.Notify(command);
                query = command.Query;
            }

            /* Get all entities */
            var entities = await this.ApplyFilter(query, filter).ToListAsync();

            /* Notify subscribed observers */
            if (entities.Count > 0 && notifyObservers)
            {
                this.Notify(new AfterReadListCommand<TEntity> { Entities = entities });
            }

            return entities;
        }

        /// <summary>
        /// Gets pagination
        /// </summary>
        /// <paramref name="page"/>
        /// <paramref name="pageSize"/>
        /// <param name="notifyObservers">If true, the subscribed observers will be notified</param>
        /// <param name="trackable">If true, the entity will be tracked</param>
        /// <returns>Type: IEnumerable<TEntity></returns>
        public virtual IPagination<TEntity> GetList(int page, int pageSize, IFilter<TEntity> filter = null, bool notifyObservers = true, bool trackable = true)
        {
            var query = this.GetAllQueryable();
            if (!trackable)
            {
                query = query.AsNoTracking();
            }

            /* Notify subscribed observers */
            if (notifyObservers)
            {
                var command = new BeforeReadListCommand<TEntity> { Query = query };
                this.Notify(command);
                query = command.Query;
            }

            /* Build pagination */
            var pagination = new Pagination<TEntity>(page, pageSize);
            pagination.Build(this.ApplyFilter(query, filter));

            /* Notify subscribed observers */
            if (pagination.Items.Count > 0 && notifyObservers)
            {
                this.Notify(new AfterReadListCommand<TEntity> { Entities = pagination.Items });
            }

            return pagination;
        }

        /// <summary>
        /// Returns a number that represents how many elements in the specified sequence satisfy a condition.
        /// </summary>
        /// <paramref name="filter"/>
        /// <paramref name="notifyObservers"/>
        /// <returns>Type: Task<int></returns>
        public int Count(IFilter<TEntity> filter = null, bool notifyObservers = true)
        {
            var query = this.Queryable;

            /* Notify subscribed observers */
            if (notifyObservers)
            {
                var command = new BeforeCountCommand<TEntity> { Query = query };
                this.Notify(command);
                query = command.Query;
            }

            return this.ApplyFilter(query, filter).Count();
        }

        /// <summary>
        /// Returns a number that represents how many elements in the specified sequence satisfy a condition.
        /// </summary>
        /// <paramref name="predicate"/>
        /// <paramref name="notifyObservers"/>
        /// <returns>Type: Task<int></returns>
        public int Count(Expression<Func<TEntity, bool>> predicate, bool notifyObservers = true)
        {
            var query = this.Queryable.Where(predicate);

            /* Notify subscribed observers */
            if (notifyObservers)
            {
                var command = new BeforeCountCommand<TEntity> { Query = query };
                this.Notify(command);
                query = command.Query;
            }

            return query.Count();
        }

        /// <summary>
        /// Returns a number that represents how many elements in the specified sequence satisfy a condition.
        /// </summary>
        /// <paramref name="filter"/>
        /// <paramref name="notifyObservers"/>
        /// <returns>Type: Task<int></returns>
        public async Task<int> CountAsync(IFilter<TEntity> filter = null, bool notifyObservers = true)
        {
            var query = this.Queryable;

            /* Notify subscribed observers */
            if (notifyObservers)
            {
                var command = new BeforeCountCommand<TEntity> { Query = query };
                this.Notify(command);
                query = command.Query;
            }

            return await this.ApplyFilter(query, filter).CountAsync();
        }

        /// <summary>
        /// Returns a number that represents how many elements in the specified sequence satisfy a condition.
        /// </summary>
        /// <paramref name="predicate"/>
        /// <paramref name="notifyObservers"/>
        /// <returns>Type: Task<int></returns>
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, bool notifyObservers = true)
        {
            var query = this.Queryable.Where(predicate);

            /* Notify subscribed observers */
            if (notifyObservers)
            {
                var command = new BeforeCountCommand<TEntity> { Query = query };
                this.Notify(command);
                query = command.Query;
            }

            return await query.CountAsync();
        }

        /// <summary>
        /// Prepares a query for a single entity
        /// </summary>
        /// <param name="navigationPropertyPath">Properties to include</param>
        /// <returns>Type: IQueryable<TEntity></returns>
        public virtual IQueryable<TEntity> GetSingleQueryable()
        {
            return this.Queryable;
        }

        /// <summary>
        /// Returns a querable
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <paramref name="filter"/>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetAllQueryable(IFilter<TEntity> filter = null)
        {
            return this.ApplyFilter(this.Queryable, filter);
        }

        /// <summary>
        ///  Begins tracking the given entity in the Microsoft.Data.Entity.EntityState.Added state such 
        ///  that it will be inserted into the database when Microsoft.Data.Entity.DbContext.SaveChanges is called.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>Type: TEntity</returns>
        public virtual TEntity Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return this.dbSet.Add(entity)?.Entity;
        }

        /// <summary>
        ///  Begins tracking the given entity in the Microsoft.Data.Entity.EntityState.Added state such 
        ///  that it will be inserted into the database when Microsoft.Data.Entity.DbContext.SaveChanges is called.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>Type: TEntity</returns>
        public virtual TEntity AddOrUpdate(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.dbContext.AddOrUpdate(entity);
            return entity;
        }

        /// <summary>
        ///  Begins tracking the given entities in the Microsoft.Data.Entity.EntityState.Added state such 
        ///  that it will be inserted into the database when Microsoft.Data.Entity.DbContext.SaveChanges is called.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        /// <returns>Type: TEntity</returns>
        public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            this.dbSet.AddRange(entities);
            return entities;
        }

        /// <summary>
        ///  Begins tracking the given entities in the Microsoft.Data.Entity.EntityState.Added state such 
        ///  that it will be inserted into the database when Microsoft.Data.Entity.DbContext.SaveChanges is called.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
        /// <returns>Type: TEntity</returns>
        public virtual async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            await this.dbSet.AddRangeAsync(entities, cancellationToken);
            return entities;
        }

        /// <summary>
        ///  Begins tracking the given entity in the Microsoft.Data.Entity.EntityState.Modified state such 
        ///  that it will be update in the database when Microsoft.Data.Entity.DbContext.SaveChanges is called.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>Type: Dictionary - The changed properties</returns>
        public virtual List<Variance> Edit(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            /* Begins tracking the given entity in the <see cref="EntityState.Modified"/> */
            this.dbContext.Update(entity);

            /* Track changed properties */
            return this.dbContext.GetChangedValues();
        }

        /// <summary>
        ///  Begins tracking the given entity in the Microsoft.Data.Entity.EntityState.Deleted state such 
        ///  that it will be delete in the database when Microsoft.Data.Entity.DbContext.SaveChanges is called.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.dbContext.Entry(entity).State = EntityState.Deleted;
        }

        #region Helpers

        /// <summary>
        /// Applies filters to the specified query.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query, IFilter<TEntity> filter)
        {
            /* Apply the filter if any */
            if (filter != null)
            {
                /* Add search filter */
                query = filter.ApplyFilter(query);

                /* Apply order, if any */
                query = filter.ApplyOrder(query);
            }

            return query;
        }

        /// <summary>
        /// Subscribes observers
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public IDisposable Subscribe(IObserver<ICommand> observer)
        {
            return Subscribe(observer, float.MaxValue);
        }

        /// <summary>
        /// Subscribes observers
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public IDisposable Subscribe(IObserver<ICommand> observer, float priority = float.MaxValue)
        {
            /* Check whether observer is already registered. If not, add it */
            if (!Observers.Contains(observer))
            {
                Observers.Enqueue(observer, priority);
            }

            return new Unsubscriber<ICommand>(Observers, observer);
        }

        /// <summary>
        /// Notifiey the subscribed observers
        /// </summary>
        /// <param name="command"></param>
        protected void Notify(ICommand command)
        {
            foreach (var observer in Observers)
            {
                observer.OnNext(command);
            }
        }

        #endregion

        #endregion
    }
}