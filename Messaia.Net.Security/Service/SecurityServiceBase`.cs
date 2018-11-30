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
    using Messaia.Net.Service;

    /// <summary>
    /// The SecurityServiceBase class
    /// </summary>
    public class SecurityServiceBase<TEntity, TService> : SecurityServiceBase<TEntity>
        where TEntity : class
        where TService : IEntityService<TEntity>
    {
        #region Properties
        
        /// <summary>
        /// Gets or sets the service
        /// </summary>
        public virtual TService Service { get; private set; }
        
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="SecurityService"/> class.
        /// </summary>
        /// <param name="authorizationService">The AuthorizationService instance</param>
        /// <param name="service">The service instance</param>
        public SecurityServiceBase(IAuthorizationService authorizationService, TService service)
            :base(authorizationService)
        {
            this.Service = service;
        }

        #endregion

        #region Methods
        
        #endregion
    }
}