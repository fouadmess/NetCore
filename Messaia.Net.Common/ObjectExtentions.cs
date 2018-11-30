///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 10:57:08
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// ObjectExtentions class.
    /// </summary>
    public static class ObjectExtentions
    {
        /// <summary>
        /// Compares properties of two objects
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentObject"></param>
        /// <param name="originalObject"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static List<Variance> GetChangedProperties<T>(this T currentObject, T originalObject, params Type[] types)
        {
            var variances = new List<Variance>();

            /* Get the type of the original object */
            var originalObjectType = originalObject.GetType();

            /* Get properties of original object */
            var properties = originalObjectType.GetProperties();

            if (types?.Length > 0)
            {
                properties = properties
                    .Where(x => types.Contains(x.PropertyType) || (types.Contains(typeof(Enum)) && x.PropertyType.IsEnum))
                    .ToArray();
            }

            /* Iterate the properties list */
            foreach (var property in properties)
            {
                /* Get property value of original object */
                var originalValue = property.GetValue(originalObject);

                /* Get property value of current object */
                var currentValue = property.GetValue(currentObject);

                /* Compare original value with current value */
                if (!Equals(property.PropertyType, originalValue?.ToString(), currentValue?.ToString()))
                {
                    variances.Add(new Variance
                    {
                        Object = currentObject,
                        ObjectType = originalObjectType,
                        PropertyName = property.Name,
                        OriginalValue = originalValue,
                        CurrentValue = currentValue
                    });
                }
            }

            return variances;
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="type"></param>
        /// <param name="originalValue"></param>
        /// <param name="currentValue"></param>
        /// <returns></returns>
        public static bool Equals(Type type, string originalValue, string currentValue)
        {
            if (originalValue == null || currentValue == null)
            {
                return originalValue == currentValue;
            }

            /* Compare decimal values */
            if (type == typeof(decimal))
            {
                return decimal.Parse(originalValue) == decimal.Parse(currentValue);
            }

            /* Comapre double values */
            if (type == typeof(double))
            {
                return double.Parse(originalValue) == double.Parse(currentValue);
            }

            return originalValue == currentValue;
        }
    }
}