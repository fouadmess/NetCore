///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 01:23:46
///   Copyright (©)   2017, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Observable
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Messaia.Net.Common;

    /// <summary>
    /// Base observer interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEntityObserver<TEntity> : IObserver<IEntityCommand<TEntity>> where TEntity : class
    {
        #region Sync

        /// <summary>
        /// Occurs after the entity is created
        /// Override this method if you are interested in AfterRead events.
        /// </summary>
        /// <param name="entity"></param>
        void OnAfterRead(TEntity entity);

        /// <summary>
        /// Occurs before the entity is created
        /// Override this method if you are interested in BeforeCreate events.
        /// </summary>
        /// <param name="entity"></param>
        void OnBeforeCreate(TEntity entity);

        /// <summary>
        /// Occurs after the entity is created
        /// Override this method if you are interested in AfterCreate events.
        /// </summary>
        /// <param name="entity"></param>
        void OnAfterCreate(TEntity entity);

        /// <summary>
        /// Occurs before the entity is updated
        /// Override this method if you are interested in BeforeUpdate events.
        /// </summary>
        /// <param name="entity"></param>
        void OnBeforeUpdate(TEntity entity, List<Variance> changes);

        /// <summary>
        /// Occurs after the entity is updated
        /// Override this method if you are interested in AfterUpdate events.
        /// </summary>
        /// <param name="entity"></param>
        void OnAfterUpdate(TEntity entity, List<Variance> changes);

        /// <summary>
        /// Occurs before the entity is deleted
        /// Override this method if you are interested in BeforeDelete events.
        /// </summary>
        /// <param name="entity"></param>
        void OnBeforeDelete(TEntity entity);

        /// <summary>
        /// Occurs after the entity is deleted
        /// Override this method if you are interested in AfterDelete events.
        /// </summary>
        /// <param name="entity"></param>
        void OnAfterDelete(TEntity entity);

        #endregion

        #region Async

        /// <summary>
        /// Occurs after the entity is created
        /// Override this method if you are interested in AfterRead events.
        /// </summary>
        /// <param name="entity"></param>
        Task OnAfterReadAsync(TEntity entity);

        /// <summary>
        /// Occurs before the entity is created
        /// Override this method if you are interested in BeforeCreate events.
        /// </summary>
        /// <param name="entity"></param>
        Task OnBeforeCreateAsync(TEntity entity);

        /// <summary>
        /// Occurs after the entity is created
        /// Override this method if you are interested in AfterCreate events.
        /// </summary>
        /// <param name="entity"></param>
        Task OnAfterCreateAsync(TEntity entity);

        /// <summary>
        /// Occurs before the entity is updated
        /// Override this method if you are interested in BeforeUpdate events.
        /// </summary>
        /// <param name="entity"></param>
        Task OnBeforeUpdateAsync(TEntity entity, List<Variance> changes);

        /// <summary>
        /// Occurs after the entity is updated
        /// Override this method if you are interested in AfterUpdate events.
        /// </summary>
        /// <param name="entity"></param>
        Task OnAfterUpdateAsync(TEntity entity, List<Variance> changes);

        /// <summary>
        /// Occurs before the entity is deleted
        /// Override this method if you are interested in BeforeDelete events.
        /// </summary>
        /// <param name="entity"></param>
        Task OnBeforeDeleteAsync(TEntity entity);

        /// <summary>
        /// Occurs after the entity is deleted
        /// Override this method if you are interested in AfterDelete events.
        /// </summary>
        /// <param name="entity"></param>
        Task OnAfterDeleteAsync(TEntity entity);

        #endregion
    }
}