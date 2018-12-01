///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           24.02.2018 13:53:15
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace ServiceTest.Observers
{
    using ServiceTest.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Messaia.Net.Common;
    using Messaia.Net.Observable.Impl;

    /// <summary>
    /// ProductObserver class.
    /// </summary>
    public class ProductObserver : BaseObserver<Product>, IProductObserver
    {
        #region Fields


        #endregion

        #region Properties


        #endregion

        #region Constructors


        #endregion

        #region Methods

        /// <summary>
        /// Occurs before the entity list is read
        /// Override this method if you are interested in BeforeRead events.
        /// </summary>
        /// <param name="query"></param>
        public override IQueryable<Product> OnBeforeReadList(IQueryable<Product> query)
        {
            return query;
        }

        /// <summary>
        /// Occurs after the entity list is read
        /// Override this method if you are interested in AfterRead events.
        /// </summary>
        /// <param name="entities"></param>
        public override void OnAfterReadList(ICollection<Product> entities)
        {
            Task.FromResult(0);
        }

        /// <summary>
        /// Occurs before the entity is read
        /// Override this method if you are interested in BeforeRead events.
        /// </summary>
        /// <param name="entity"></param>
        public override IQueryable<Product> OnBeforeRead(IQueryable<Product> query)
        {
            return query;
        }

        /// <summary>
        /// Occurs after the entity is read
        /// Override this method if you are interested in AfterRead events.
        /// </summary>
        /// <param name="entity"></param>
        public override void OnAfterRead(Product entity)
        {
            Task.FromResult(0);
        }

        /// <summary>
        /// Occurs before the entity is created
        /// Override this method if you are interested in BeforeCreate events.
        /// </summary>
        /// <param name="entity"></param>
        public override void OnBeforeCreate(Product entity)
        {
            Task.FromResult(0);
        }

        /// <summary>
        /// Occurs after the entity is created
        /// Override this method if you are interested in AfterCreate events.
        /// </summary>
        /// <param name="entity"></param>
        public override void OnAfterCreate(Product entity)
        {
            Task.FromResult(0);
        }

        /// <summary>
        /// Occurs before the entity is updated
        /// Override this method if you are interested in BeforeUpdate events.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="changes"></param>
        public override void OnBeforeUpdate(Product entity, List<Variance> changes)
        {
            Task.FromResult(0);
        }

        /// <summary>
        /// Occurs after the entity is updated
        /// Override this method if you are interested in AfterUpdate events.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="changes"></param>
        public override void OnAfterUpdate(Product entity, List<Variance> changes)
        {
            Task.FromResult(0);
        }

        #endregion
    }
}