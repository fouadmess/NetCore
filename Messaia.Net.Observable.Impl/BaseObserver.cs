///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 01:24:22
///   Copyright (©)   2017, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Observable.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Messaia.Net.Common;

    /// <summary>
    /// Base observer class
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseObserver<TEntity> : IBaseObserver, IEntityObserver<TEntity>, IQueryObserver<TEntity>
        where TEntity : class
    {
        #region Properties

        /// <summary>
        /// Gets or sets the CurrentCommand
        /// </summary>
        public Type CurrentCommandType { get; private set; }

        #endregion

        /// <summary>
        /// Provides the observer with new data.
        /// </summary>
        /// <param name="value"></param>
        public virtual void OnNext(ICommand value)
        {
            /* Get the type of the current command */
            this.CurrentCommandType = value.GetType();

            #region Sync

            /* Find the method associated to the current command */
            var method = this.GetType().GetTypeInfo().GetMethod($"On{Regex.Replace(CurrentCommandType.Name, "Command(.*)", "")}");
            if (method != null)
            {
                if (value is IEntityCommand<TEntity> || value is ICollectionCommand<TEntity>)
                {
                    method.Invoke(this, this.GetMethodArguments(value));
                }
                else if (value is IQueryCommand<TEntity>)
                {
                    (value as IQueryCommand<TEntity>).Query = (IQueryable<TEntity>)method.Invoke(this, this.GetMethodArguments(value));
                }
                else
                {
                    method.Invoke(this, null);
                }
            }

            #endregion

            #region Async

            /* Find the method associated to the current command */
            method = this.GetType().GetTypeInfo().GetMethod($"On{Regex.Replace(CurrentCommandType.Name, "Command(.*)", "")}Async");
            if (method != null)
            {
                if (value is IEntityCommand<TEntity> || value is ICollectionCommand<TEntity>)
                {
                    ((Task)method.Invoke(this, this.GetMethodArguments(value))).GetAwaiter().GetResult();
                }
                else if (value is IQueryCommand<TEntity>)
                {
                    (value as IQueryCommand<TEntity>).Query = ((Task<IQueryable<TEntity>>)method.Invoke(this, this.GetMethodArguments(value))).GetAwaiter().GetResult();
                }
                else
                {
                    ((Task)method.Invoke(this, null)).GetAwaiter().GetResult();
                }
            }

            #endregion
        }

        /// <summary>
        /// Provides the observer with new data.
        /// </summary>
        /// <param name="value"></param>
        public virtual void OnNext(IEntityCommand<TEntity> value)
        {
            /* Get the type of the current command */
            this.CurrentCommandType = value.GetType();

            if (value.Entity == null)
            {
                return;
            }

            /* Find the method associated to the current command */
            var method = this.GetType().GetTypeInfo().GetMethod($"On{Regex.Replace(CurrentCommandType.Name, "Command(.*)", "")}");
            if (method != null)
            {
                method.Invoke(this, new object[] { value.Entity });
            }
        }

        /// <summary>
        /// Provides the observer with new data.
        /// </summary>
        /// <param name="value"></param>
        public virtual void OnNext(IQueryCommand<TEntity> value)
        {
            if (value != null)
            {
                value.Query = this.OnQuery(value.Query);
            }
        }

        /// <summary>
        /// Notifies the observer that the provider has finished sending push-based notifications.
        /// </summary>
        public virtual void OnCompleted() { }

        /// <summary>
        /// Notifies the observer that the provider has experienced an error condition.
        /// </summary>
        /// <param name="error"></param>
        public virtual void OnError(Exception error) { }

        #region Sync Methods

        /// <summary>
        /// Occurs before the entity list is read
        /// Override this method if you are interested in BeforeRead events.
        /// </summary>
        /// <param name="query"></param>
        public virtual IQueryable<TEntity> OnBeforeReadList(IQueryable<TEntity> query) { return query; }

        /// <summary>
        /// Occurs after the entity list is read
        /// Override this method if you are interested in AfterRead events.
        /// </summary>
        /// <param name="entities"></param>
        public virtual void OnAfterReadList(ICollection<TEntity> entities) { }

        /// <summary>
        /// Occurs before the entity is read
        /// Override this method if you are interested in BeforeRead events.
        /// </summary>
        /// <param name="query"></param>
        public virtual IQueryable<TEntity> OnBeforeRead(IQueryable<TEntity> query) { return query; }

        /// <summary>
        /// Occurs after the entity is read
        /// Override this method if you are interested in AfterRead events.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void OnAfterRead(TEntity entity) { }

        /// <summary>
        /// Occurs before the entity is created
        /// Override this method if you are interested in BeforeCreate events.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void OnBeforeCreate(TEntity entity) { }

        /// <summary>
        /// Occurs after the entity is created
        /// Override this method if you are interested in AfterCreate events.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void OnAfterCreate(TEntity entity) { }

        /// <summary>
        /// Occurs before the entity is updated
        /// Override this method if you are interested in BeforeUpdate events.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="changes"></param>
        public virtual void OnBeforeUpdate(TEntity entity, List<Variance> changes) { }

        /// <summary>
        /// Occurs after the entity is updated
        /// Override this method if you are interested in AfterUpdate events.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="changes"></param>
        public virtual void OnAfterUpdate(TEntity entity, List<Variance> changes) { }

        /// <summary>
        /// Occurs before the entity is deleted
        /// Override this method if you are interested in BeforeDelete events.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void OnBeforeDelete(TEntity entity) { }

        /// <summary>
        /// Occurs after the entity is deleted
        /// Override this method if you are interested in AfterDelete events.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void OnAfterDelete(TEntity entity) { }

        /// <summary>
        /// Occurs on building a query
        /// </summary>
        /// <param name="query"></param>
        public virtual IQueryable<TEntity> OnQuery(IQueryable<TEntity> query) { return query; }

        /// <summary>
        /// Occurs before the entity count
        /// Override this method if you are interested in BeforeCount events.
        /// </summary>
        /// <param name="query"></param>
        public virtual IQueryable<TEntity> OnBeforeCount(IQueryable<TEntity> query) { return query; }

        #endregion

        #region Async Methods

        /// <summary>
        /// Occurs before the entity list is read
        /// Override this method if you are interested in BeforeRead events.
        /// </summary>
        /// <param name="query"></param>
        public virtual Task<IQueryable<TEntity>> OnBeforeReadListAsync(IQueryable<TEntity> query) { return Task.FromResult(query); }

        /// <summary>
        /// Occurs after the entity list is read
        /// Override this method if you are interested in AfterRead events.
        /// </summary>
        /// <param name="entities"></param>
        public virtual Task OnAfterReadListAsync(ICollection<TEntity> entities) { return Task.CompletedTask; }

        /// <summary>
        /// Occurs before the entity is read
        /// Override this method if you are interested in BeforeRead events.
        /// </summary>
        /// <param name="query"></param>
        public virtual Task<IQueryable<TEntity>> OnBeforeReadAsync(IQueryable<TEntity> query) { return Task.FromResult(query); }

        /// <summary>
        /// Occurs after the entity is read
        /// Override this method if you are interested in AfterRead events.
        /// </summary>
        /// <param name="entity"></param>
        public virtual Task OnAfterReadAsync(TEntity entity) { return Task.CompletedTask; }

        /// <summary>
        /// Occurs before the entity is created
        /// Override this method if you are interested in BeforeCreate events.
        /// </summary>
        /// <param name="entity"></param>
        public virtual Task OnBeforeCreateAsync(TEntity entity) { return Task.CompletedTask; }

        /// <summary>
        /// Occurs after the entity is created
        /// Override this method if you are interested in AfterCreate events.
        /// </summary>
        /// <param name="entity"></param>
        public virtual Task OnAfterCreateAsync(TEntity entity) { return Task.CompletedTask; }

        /// <summary>
        /// Occurs before the entity is updated
        /// Override this method if you are interested in BeforeUpdate events.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="changes"></param>
        public virtual Task OnBeforeUpdateAsync(TEntity entity, List<Variance> changes) { return Task.CompletedTask; }

        /// <summary>
        /// Occurs after the entity is updated
        /// Override this method if you are interested in AfterUpdate events.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="changes"></param>
        public virtual Task OnAfterUpdateAsync(TEntity entity, List<Variance> changes) { return Task.CompletedTask; }

        /// <summary>
        /// Occurs before the entity is deleted
        /// Override this method if you are interested in BeforeDelete events.
        /// </summary>
        /// <param name="entity"></param>
        public virtual Task OnBeforeDeleteAsync(TEntity entity) { return Task.CompletedTask; }

        /// <summary>
        /// Occurs after the entity is deleted
        /// Override this method if you are interested in AfterDelete events.
        /// </summary>
        /// <param name="entity"></param>
        public virtual Task OnAfterDeleteAsync(TEntity entity) { return Task.CompletedTask; }

        /// <summary>
        /// Occurs on building a query
        /// </summary>
        /// <param name="query"></param>
        public virtual Task<IQueryable<TEntity>> OnQueryAsync(IQueryable<TEntity> query) { return Task.FromResult(query); }

        /// <summary>
        /// Occurs before the entity count
        /// Override this method if you are interested in BeforeCount events.
        /// </summary>
        /// <param name="query"></param>
        public virtual Task<IQueryable<TEntity>> OnBeforeCountAsync(IQueryable<TEntity> query) { return Task.FromResult(query); }

        #endregion

        #region Helpers

        /// <summary>
        /// Gets command arguments
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private object[] GetMethodArguments(ICommand command)
        {
            return command
                .GetType()
                .GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(ArgumentOrderAttribute)))
                .OrderBy(x => ((ArgumentOrderAttribute)x.GetCustomAttribute(typeof(ArgumentOrderAttribute))).Order)
                .Select(x => x.GetValue(command))
                .ToArray();
        }

        #endregion
    }
}