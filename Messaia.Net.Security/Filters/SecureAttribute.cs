///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 12:15:50
///   Copyright (©)   2017, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Security
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Linq;
    using Messaia.Net.Observable;
    using Messaia.Net.Service;

    /// <summary>
    /// SecureAttribute class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SecureAttribute : Attribute, IActionFilter
    {
        #region Fields


        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the ServiceType
        /// </summary>
        public Type ServiceType { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="SecureAttribute"/> class.
        /// </summary>
        public SecureAttribute(Type serviceType)
        {
            this.ServiceType = serviceType;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called after the action executes, before the action result.
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            /* Get security service from DI container */
            var securityService = context.HttpContext?.RequestServices?.GetService(this.ServiceType);
            if (securityService != null)
            {
                /* Get the entity service  */
                var service = context.Controller?
                    .GetType()
                    .GetProperties()
                    .FirstOrDefault(x => IsAssignableToGenericType(x.PropertyType, typeof(IEntityService<>)))?
                    .GetValue(context.Controller);

                if (service == null)
                {
                    throw new Exception($"'{this.ServiceType.Name}' could not be injected.");
                }

                /* Invoke the subscribe method of the entity service */
                service
                    .GetType()
                    .GetMethod(nameof(IEntityService<object>.Subscribe), new Type[] { typeof(IObserver<ICommand>) }, null)?
                    .Invoke(service, new object[] { securityService });
            }
        }

        /// <summary>
        /// Called before the action executes, after model binding is complete.
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context) { }

        /// <summary>
        /// Checks if a the specified type is assiegnable to the specified generic type
        /// </summary>
        /// <param name="givenType"></param>
        /// <param name="genericType"></param>
        /// <returns></returns>
        private bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            /* Get interface types of the given type */
            var interfaceTypes = givenType.GetInterfaces();
            foreach (var interfaceType in interfaceTypes)
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            /* Check if the type has a parent */
            if (givenType.BaseType == null)
            {
                return false;
            }

            /* Call this method recursivly to check the base type */
            return IsAssignableToGenericType(givenType.BaseType, genericType);
        }

        #endregion
    }
}