///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 09:30:48
///   Copyright (©)   2017, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Security
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using Messaia.Net.Model;

    /// <summary>
    /// Helper functions for configuring security services.
    /// </summary>
    public class SecurityBuilder
    {
        #region Fields


        #endregion

        #region Properties

        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> services are attached to.
        /// </summary>
        /// <value>
        /// The <see cref="IServiceCollection"/> services are attached to.
        /// </value>
        public IServiceCollection Services { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="SecurityBuilder"/> class.
        /// </summary>
        public SecurityBuilder(IServiceCollection Services)
        {
            this.Services = Services;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds base CRUD authorization handlers to make a decision if authorization is allowed.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public SecurityBuilder AddAuthorizationHandlers<TEntity>() where TEntity : class, IEntity<int>, new()
        {
            Services
                .AddTransient(typeof(IAuthorizationHandler), typeof(CreateHandlerBase<TEntity>))
                .AddTransient(typeof(IAuthorizationHandler), typeof(ReadHandlerBase<TEntity>))
                .AddTransient(typeof(IAuthorizationHandler), typeof(UpdateHandlerBase<TEntity>))
                .AddTransient(typeof(IAuthorizationHandler), typeof(DeleteHandlerBase<TEntity>));

            return this;
        }

        /// <summary>
        /// Adds base CRUD authorization handlers to make a decision if authorization is allowed.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public SecurityBuilder AddAuthorizationHandlers(Type entityType)
        {
            Services
                .AddTransient(typeof(IAuthorizationHandler), typeof(CreateHandlerBase<>).MakeGenericType(entityType))
                .AddTransient(typeof(IAuthorizationHandler), typeof(ReadHandlerBase<>).MakeGenericType(entityType))
                .AddTransient(typeof(IAuthorizationHandler), typeof(UpdateHandlerBase<>).MakeGenericType(entityType))
                .AddTransient(typeof(IAuthorizationHandler), typeof(DeleteHandlerBase<>).MakeGenericType(entityType));

            return this;
        }

        #endregion
    }
}