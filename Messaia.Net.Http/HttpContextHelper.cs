///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Http
{
    using Microsoft.AspNetCore.Http;
    using System;

    /// <summary>
    /// The HttpContextHelper class
    /// </summary>
    public static class HttpContextHelper
    {
        #region Fields

        /// <summary>
        /// The httpContextAccessor field
        /// </summary>
        private static IHttpContextAccessor httpContextAccessor;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the HttpContext
        /// </summary>
        public static HttpContext HttpContext { get { return httpContextAccessor?.HttpContext; } }

        #endregion

        #region Methods

        /// <summary>
        /// Initilaizes the IHttpContextAccessor instance
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextHelper.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets a value from the the query collection parsed from Request.QueryString.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetQueryValue(string name)
        {
            return HttpContext?.Request?.Query[name];
        }

        /// <summary>
        /// Gets a value from the the current context.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TType GetContextValue<TType>(string name)
        {
            try
            {
                var value = HttpContext?.Items?[name];
                if (value != null && value.GetType() is IConvertible)
                {
                    return (TType)Convert.ChangeType(value, typeof(TType));
                }

                return (TType)value;
            }
            catch (Exception) { return default(TType); }
        }

        /// <summary>
        /// Adds a value to the the current context.
        /// </summary>
        /// <param name="name"></param>
        public static void AddContextValue(string name, object value)
        {
            if (HttpContext != null && HttpContext.Items != null)
            {
                HttpContext.Items[name] = value;
            }
        }

        /// <summary>
        /// Stores a query value into the cuurent context.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Type: object</returns>
        public static string QueryToContext(string name)
        {
            var value = GetQueryValue(name);
            if (value != null)
            {
                AddContextValue(name, value);
            }

            return value;
        }

        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <typeparam name="TService">The type of the service</typeparam>
        /// <returns></returns>
        public static TService GetService<TService>()
        {
            return (TService)HttpContext?.RequestServices?.GetService(typeof(TService));
        }

        #endregion
    }
}