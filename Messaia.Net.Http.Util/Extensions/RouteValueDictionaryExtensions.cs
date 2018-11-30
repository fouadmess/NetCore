///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 20:54:17
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Http.Util
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// RouteValueDictionaryExtensions class.
    /// </summary>
    public static class RouteValueDictionaryExtensions
    {
        #region Fields

        /// <summary>
        /// A list of built-in types
        /// </summary>
        private static List<Type> systemTypes = Assembly.GetExecutingAssembly().GetType().Module.Assembly.GetExportedTypes().ToList();

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public static RouteValueDictionary Flatten(this RouteValueDictionary routeValues)
        {
            var newRouteValues = new RouteValueDictionary();
            foreach (var key in routeValues.Keys)
            {
                /* Get and check the value against null */
                var value = routeValues[key];
                if (value == null)
                {
                    continue;
                }

                /* Check if the type is primitive */
                if (value is IEnumerable && !(value is string))
                {
                    int index = 0;
                    foreach (object val in (IEnumerable)value)
                    {
                        if (val is string || val.GetType().IsPrimitive)
                        {
                            newRouteValues.Add(string.Format("{0}[{1}]", key, index++), val);
                        }
                        else
                        {
                            val.GetType()
                                .GetProperties()
                                .ToList()
                                .ForEach(x => newRouteValues.Add(string.Format("{0}[{1}].{2}", key, index++, x.Name), x.GetValue(val)));
                        }
                    }
                }
                else
                {
                    newRouteValues.Add(key, value);
                }
            }

            return newRouteValues.FlattenNavigations();
        }

        /// <summary>
        /// Flatten of complex objects
        /// </summary>
        /// <param name="routeValues"></param>
        /// <param name="newValues"></param>
        /// <param name="sec"></param>
        /// <returns></returns>
        public static RouteValueDictionary FlattenNavigations(this RouteValueDictionary routeValues, RouteValueDictionary newValues = null, string sec = null)
        {
            newValues = newValues ?? new RouteValueDictionary();
            foreach (var key in routeValues.Keys)
            {
                /* Get and check the value against null */
                var value = routeValues[key];
                if (value == null)
                {
                    continue;
                }

                /* Check if the type of the property is not a built-in type */
                if (!systemTypes.Contains(value.GetType()))
                {
                    FlattenNavigations(new RouteValueDictionary(value), newValues, sec != null ? $"{sec}.{key}" : key);
                }
                else
                {
                    newValues.Add(sec != null ? $"{sec}.{key}" : key, value);
                }
            }

            return newValues;
        }
    }
}