///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 10:20:02
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace System
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// TypeExtensions class.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Checks if a type is assignable to the specified generic type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="genericType"></param>
        /// <returns></returns>
        public static bool IsAssignableToGenericType(this Type type, Type genericType)
        {
            foreach (var it in type.GetInterfaces())
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            if (type.BaseType == null)
            {
                return false;
            }

            return IsAssignableToGenericType(type.BaseType, genericType);
        }

        /// <summary>
        /// Checks whether a type is an AnonymousType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsAnonymousType(this Type type)
        {
            var hasCompilerGeneratedAttribute = type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Count() > 0;
            var nameContainsAnonymousType = type.FullName.Contains("AnonymousType");
            var isAnonymousType = hasCompilerGeneratedAttribute && nameContainsAnonymousType;
            return isAnonymousType;
        }


        /// <summary>
        /// Searches for the public generic method with the specified name.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="parameterTypes"></param>
        /// <returns></returns>
        public static MethodInfo GetGenericMethod(this Type type, string name, Type[] parameterTypes)
        {
            /* Get all methods */
            var methods = type.GetMethods();
            foreach (var method in methods.Where(m => m.Name == name))
            {
                if (method.GetParameters().Select(p => p.ParameterType).SequenceEqual(parameterTypes, new SimpleTypeComparer()))
                {
                    return method;
                }
            }

            return null;
        }

        /// <summary>
        /// Simple method comparer
        /// </summary>
        private class SimpleTypeComparer : IEqualityComparer<Type>
        {
            /// <summary>
            /// Determines whether two object instances are equal.
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public bool Equals(Type x, Type y)
            {
                return x.Assembly == y.Assembly && x.Namespace == y.Namespace && x.Name == y.Name;
            }

            /// <summary>
            /// Returns a hash code for the specified object.
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public virtual int GetHashCode(Type obj)
            {
                throw new NotImplementedException();
            }
        }
    }
}