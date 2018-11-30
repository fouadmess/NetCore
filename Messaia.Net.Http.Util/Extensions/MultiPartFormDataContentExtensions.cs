///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 19:56:18
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace System.Net.Http
{
    using System;
    using System.IO;
    using System.Linq;
    using Messaia.Net.Http.Util;

    /// <summary>
    /// Extensions for <see cref="MultipartFormDataContent"/>.
    /// </summary>
    public static class MultiPartFormDataContentExtensions
    {
        /// <summary>
        /// Adds an object to the form data
        /// </summary>
        /// <param name="form"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public static MultipartFormDataContent AddObject(this MultipartFormDataContent form, object payload = null)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            /* Convert the object to key-value pairs and them to the form as string content */
            new RouteValueDictionary(payload)
                .Flatten()
                .Where(x => x.Key != null && x.Value != null)
                .All(x =>
                {
                    form.Add(new StringContent(x.Value.ToString()), x.Key);
                    return true;
                });

            return form;
        }

        /// <summary>
        /// Adds a file to the form data
        /// </summary>
        /// <param name="form"></param>
        /// <param name="fileName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static MultipartFormDataContent AddFile(this MultipartFormDataContent form, string fileName, string name = null)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            return form.AddFile(new FileInfo(fileName), name);
        }

        /// <summary>
        /// Adds a file to the form data
        /// </summary>
        /// <param name="form"></param>
        /// <param name="fileInfo"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static MultipartFormDataContent AddFile(this MultipartFormDataContent form, FileInfo fileInfo, string name = null)
        {
            if (fileInfo == null)
            {
                throw new ArgumentNullException(nameof(fileInfo));
            }

            /* Check if the file is valid and exists */
            if (!fileInfo.Exists)
            {
                throw new ArgumentException($"File '{fileInfo.FullName}' not found!");
            }

            /* Add the file to the form */
            form.Add(new StreamContent(fileInfo.OpenRead()), name: name ?? "file", fileName: fileInfo.Name);

            return form;
        }
    }
}