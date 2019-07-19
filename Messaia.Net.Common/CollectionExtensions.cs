///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           04.01.2019 17:19:43
///   Copyright (©)   2019, VERITAS DATA GmbH, all Rights Reserved. 
///                   No part of this document may be reproduced 
///                   without VERITAS DATA GmbH's express consent. 
///-----------------------------------------------------------------
namespace System.Collections.Generic
{
    /// <summary>
    /// CollectionExtensions class.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default(TValue))
        {
            return dict.TryGetValue(key, out TValue value) ? value : defaultValue;
        }
    }
}