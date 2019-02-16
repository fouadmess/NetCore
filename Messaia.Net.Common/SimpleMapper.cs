///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 00:56:39
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Common
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// SimpleMapper class.
    /// </summary>
    public static class SimpleMapper
    {
        /// <summary>
        /// Maps an object to another
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source">The source to map</param>
        /// <param name="customMapping">Apply custom mapping</param>
        public static TDestination Map<TSource, TDestination>(TSource source, Action<TSource, TDestination> customMapping = null) where TSource : class, new() where TDestination : class, new()
        {
            if (source == null)
            {
                return null;
            }

            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;

            /* Get the readable properties of this source object */
            var srcFields = (
                from PropertyInfo aProp in typeof(TSource).GetProperties(flags)
                where aProp.CanRead
                select new { aProp.Name, Type = Nullable.GetUnderlyingType(aProp.PropertyType) ?? aProp.PropertyType }
            ).ToList();

            /* Get the writeable properties of this object */
            var trgFields = (
                from PropertyInfo aProp in typeof(TDestination).GetProperties(flags)
                where aProp.CanWrite
                select new { aProp.Name, Type = Nullable.GetUnderlyingType(aProp.PropertyType) ?? aProp.PropertyType }
            ).ToList();

            /* Common fields where name and type same*/
            var commonFields = srcFields.Intersect(trgFields).ToList();

            var dest = new TDestination();

            /* Assign the values */
            foreach (var aField in commonFields)
            {
                typeof(TDestination).GetProperty(aField.Name).SetValue(dest, source.GetType().GetProperty(aField.Name).GetValue(source, null), null);
            }

            /* Apply custom mapping */
            customMapping?.Invoke(source, dest);

            return dest;
        }
    }
}