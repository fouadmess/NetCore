///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Security
{
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;
    using Messaia.Net.Model;

    /// <summary>
    /// A base generic update handler.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class UpdateHandlerBase<TEntity> : AuthorizationHandlerBase<UpdateRequirement, TEntity>
        where TEntity : class, IEntity<int>, new()
    {
        /// <summary>
        /// Makes a decision if authorization is allowed based on a specific requirement and resource.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <param name="resource"></param>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UpdateRequirement requirement, TEntity resource)
        {
            return this.Success(context, requirement, resource, GlobalPermissionClaims.Update);
        }
    }
}