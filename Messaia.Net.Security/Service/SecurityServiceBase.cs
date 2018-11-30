///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 04:04:58
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Security
{
    using Microsoft.AspNetCore.Authorization;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Messaia.Net.Common;
    using Messaia.Net.Model;
    using Messaia.Net.Observable.Impl;

    /// <summary>
    /// The SecurityServiceBase class
    /// </summary>
    public class SecurityServiceBase<TEntity> : BaseObserver<TEntity>, IEquatable<SecurityServiceBase<TEntity>>, ISecurityService<TEntity>
        where TEntity : class
    {
        #region Properties

        /// <summary>
        /// The authorizationService field
        /// </summary>
        public virtual IAuthorizationService AuthorizationService { get; private set; }

        /// <summary>
        /// Gets or sets the EntityName
        /// </summary>
        public virtual string EntityName { get; protected set; } = typeof(TEntity).Name;

        /// <summary>
        /// Gets or sets the CurrentDateTime
        /// </summary>
        public virtual DateTime CurrentDateTime { get { return DateTime.Now; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="SecurityService"/> class.
        /// </summary>
        /// <param name="authorizationService">The AuthorizationService instance</param>
        /// <param name="service">The service instance</param>
        public SecurityServiceBase(IAuthorizationService authorizationService)
        {
            this.AuthorizationService = authorizationService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Occurs after the entity is created
        /// Override this method if you are interested in AfterRead events.
        /// </summary>
        /// <param name="entity"></param>
        public override void OnAfterRead(TEntity entity)
        {
            this.AuthorizeSingle(entity, new ReadRequirement());
        }

        /// <summary>
        /// Occurs before the entity is created
        /// Override this method if you are interested in BeforeCreate events.
        /// </summary>
        /// <param name="entity"></param>
        public override void OnBeforeCreate(TEntity entity)
        {
            this.AuthorizeSingle(entity, new CreateRequirement());
        }

        /// <summary>
        /// Occurs before the entity is updated
        /// Override this method if you are interested in BeforeUpdate events.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="changes"></param>
        public override void OnBeforeUpdate(TEntity entity, List<Variance> changes)
        {
            this.AuthorizeSingle(entity, new UpdateRequirement());
        }

        /// <summary>
        /// Occurs before the entity is deleted
        /// Override this method if you are interested in BeforeDelete events.
        /// </summary>
        /// <param name="entity"></param>
        public override void OnBeforeDelete(TEntity entity)
        {
            this.AuthorizeSingle(entity, new DeleteRequirement());
        }

        /// <summary>
        /// Occurs before the entity list is read
        /// Override this method if you are interested in BeforeRead events.
        /// </summary>
        /// <param name="query"></param>
        public override IQueryable<TEntity> OnBeforeReadList(IQueryable<TEntity> query)
        {
            return this.AuthorizeList(query);
        }

        /// <summary>
        /// Occurs on building a query
        /// Override this method if you are interested in IQueryable events.
        /// </summary>
        /// <param name="entity"></param>
        public override IQueryable<TEntity> OnQuery(IQueryable<TEntity> query)
        {
            return this.AuthorizeList(query);
        }

        /// <summary>
        /// Occurs before the entity count
        /// Override this method if you are interested in BeforeCount events.
        /// </summary>
        /// <param name="query"></param>
        public override IQueryable<TEntity> OnBeforeCount(IQueryable<TEntity> query)
        {
            if (!AuthorizationHelper.IsPermitted(GlobalPermissionClaims.Read, GlobalPermissionClaims.Count, $"{EntityName}Count"))
            {
                throw new NotAuthorizedException();
            }

            return query;
        }

        /// <summary>
        /// Checks authorization for a single entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="requirement"></param>
        protected virtual void AuthorizeSingle(TEntity entity, IAuthorizationRequirement requirement)
        {
            if (!this.AuthorizationService.AuthorizeAsync(AuthorizationHelper.User, entity, requirement).Result.Succeeded)
            {
                throw new NotAuthorizedException();
            }
        }

        /// <summary>
        /// Checks authorization for a list of entities.
        /// </summary>
        /// <param name="query"></param>
        protected virtual IQueryable<TEntity> AuthorizeList(IQueryable<TEntity> query)
        {
            /* Check global permissions */
            if (!AuthorizationHelper.IsPermitted(GlobalPermissionClaims.Read, $"Read{EntityName}"))
            {
                var predicate = PredicateBuilder.False<TEntity>();

                /* Check if the entities owned by the current user */
                if (typeof(IOwnableEntity).IsAssignableFrom(query.ElementType))
                {
                    predicate = predicate.Or(x => ((IOwnableEntity)x).OwnedByUserId == AuthorizationHelper.UserId);
                }

                return query.Where(predicate);
            }

            return query;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public virtual bool Equals(SecurityServiceBase<TEntity> other)
        {
            if (other == null)
            {
                return false;
            }

            return (this.GetType().Equals(other.GetType()));
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is SecurityServiceBase<TEntity> securitySrviceObject))
            {
                return false;
            }

            return Equals(securitySrviceObject);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}