///-----------------------------------------------------------------
///   Author:         fouad
///   AuthorUrl:      http://veritas-data.de
///   Date:           25.01.2019 13:45:12
///   Copyright (©)   2019, VERITAS DATA GmbH, all Rights Reserved. 
///                   No part of this document may be reproduced 
///                   without VERITAS DATA GmbH's express consent. 
///-----------------------------------------------------------------
namespace System.Collections.Generic
{
    /// <summary>
    /// The IEnumerableExtensions class
    /// </summary>    
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Performs the specified action on each element of the IEnumerable<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumeration"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }
        /// <summary>
        /// Performs the specified action on each element of the IEnumerable<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumeration"></param>
        /// <param name="action"></param>

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<int, T> action)
        {
            int i = 0;
            foreach (T item in enumeration)
            {
                action(i, item);
                i++;
            }
        }
    }
}