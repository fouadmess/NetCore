///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 17:36:55
///   Copyright (©)   2017, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Api
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// ReflectionExtensions class.
    /// </summary>
    internal static class ReflectionExtensions
    {
        public static IEnumerable<Type> GetParentTypes(this Type type)
        {
            /* Is there any base type? */
            if ((type == null) || (type.GetTypeInfo().BaseType == null))
            {
                yield break;
            }

            /* Return all implemented or inherited interfaces */
            foreach (var i in type.GetInterfaces())
            {
                yield return i;
            }

            /* Return all inherited types */
            var currentBaseType = type.GetTypeInfo().BaseType;
            while (currentBaseType != null)
            {
                yield return currentBaseType;
                currentBaseType = currentBaseType.GetTypeInfo().BaseType;
            }
        }
    }
}