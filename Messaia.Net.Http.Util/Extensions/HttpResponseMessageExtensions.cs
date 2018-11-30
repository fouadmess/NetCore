///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 15:59:40
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace System.Net.Http
{
    using Newtonsoft.Json;
    using System.Threading.Tasks;

    /// <summary>
    /// HttpResponseMessageExtensions class.
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Converts the response result to <see cref="TResult"/> type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="httpResponse"></param>
        /// <returns></returns>
        public static async Task<TResult> ToObjectAsync<TResult>(this HttpResponseMessage httpResponse) where TResult : class
        {
            /* Get result as json string */
            var result = await httpResponse.Content?.ReadAsStringAsync();
            if (!string.IsNullOrWhiteSpace(result))
            {
                return JsonConvert.DeserializeObject<TResult>(result);
            }

            return null;
        }
    }
}