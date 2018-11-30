///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 15:07:28
///   Copyright (©)   2017, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Security
{
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;
    using Messaia.Net.Http;
    using Messaia.Net.Model;

    /// <summary>
    /// AuthorizationHandlerBase class.
    /// </summary>
    public abstract class AuthorizationHandlerBase<TRequirement, TEntity> : AuthorizationHandler<TRequirement, TEntity>
        where TEntity : class, IEntity<int>, new()
        where TRequirement : IAuthorizationRequirement
    {
        #region Properties

        /// <summary>
        /// Gets or sets the EntityName
        /// </summary>
        public virtual string EntityName { get; protected set; } = typeof(TEntity).Name;

        #endregion

        #region Methods

        /// <summary>
        /// Checks permission of current user
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        protected bool IsPermitted(string permission) => AuthorizationHelper.IsPermitted(permission, $"{permission}{this.EntityName}");

        /// <summary>
        ///  Called to mark the specified requirement as being successfully evaluated.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        protected Task Success(AuthorizationHandlerContext context, TRequirement requirement, TEntity resource, string permission)
        {
            /* Check common permissions */
            if (this.IsPermitted(permission))
            {
                context.Succeed(requirement);
            }

            /* Check if the user has owner permission and is the the owner of the resource */
            if (this.IsPermitted(GlobalPermissionClaims.Owner) && (resource as IAuditEntity)?.CreatedBy == PrincipalHelper.User?.Identity.Name)
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }

        #endregion
    }
}