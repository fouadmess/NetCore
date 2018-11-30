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
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Messaia.Net.Http;
    using Messaia.Net.Model;

    /// <summary>
    /// A base generic read handler.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class ReadHandlerBase<TEntity> : AuthorizationHandlerBase<ReadRequirement, TEntity>
        where TEntity : class, IEntity<int>, new()
    {
        /// <summary>
        /// Makes a decision if authorization is allowed based on a specific requirement and resource.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <param name="resource"></param>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ReadRequirement requirement, TEntity resource)
        {
            /* Check if the user has read permissions */
            this.Success(context, requirement, resource, GlobalPermissionClaims.Read);

            /* 
            If the user is permistted to read this resource, 
            add all permissions of this resource to response headers 
            */
            if (context.HasSucceeded)
            {
                /* Get user permissions for current resource */
                var permissions = new Dictionary<string, bool>
                {
                    { GlobalPermissionClaims.Create, this.IsPermitted(GlobalPermissionClaims.Create) },
                    { GlobalPermissionClaims.Read, true },
                    { GlobalPermissionClaims.Update, this.IsPermitted(GlobalPermissionClaims.Update) },
                    { GlobalPermissionClaims.Delete, this.IsPermitted(GlobalPermissionClaims.Delete) }
                }
                .Where(x => x.Value)
                .Select(x => x.Key);

                /* Add permissios to response headers */
                HttpContextHelper.HttpContext.Response.Headers["Permissions"] = string.Join(";", permissions);
            }

            return Task.FromResult(0);
        }
    }
}