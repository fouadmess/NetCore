///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Http
{
    using Microsoft.Net.Http.Headers;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// The FileHelper class
    /// </summary>
    public static class FileHelper
    {
        #region Properties

        /// <summary>
        /// Gets or sets the BasePath
        /// </summary>
        public static string BasePath { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Configures the FileHelper
        /// </summary>
        /// <param name="basePath"></param>
        public static void Configure(string basePath)
        {
            BasePath = basePath;
        }

        /// <summary>
        /// Saves a file to the disk.
        /// </summary>
        /// <param name="folder">The name of the sub folder</param>
        /// <param name="allowedExtensions">Allowed sxtensions</param>
        /// <param name="preserveOriginalName">If true preserve the file original name</param>
        /// <returns></returns>
        public static List<UploadDetail> Upload(string folder, string[] allowedExtensions, bool preserveOriginalName = false)
        {
            var filesUploaded = new List<UploadDetail>();

            /* Iterate the file list */
            var files = HttpContextHelper.HttpContext?.Request?.Form?.Files;
            foreach (var file in files)
            {
                /* Get file name and extension */
                var fileName = Path.GetFileName(
                    ContentDispositionHeaderValue
                        .Parse(file.ContentDisposition)
                        .FileName
                        .ToString()
                        .Trim('"')
                );

                string fileExtension = Path.GetExtension(file.FileName);
                if (allowedExtensions != null)
                {
                    if (!allowedExtensions.Contains(fileExtension?.ToLower()))
                    {
                        throw new ExtensionNotAllowedException($"File extension '{fileExtension}' not allowed!");
                    }
                }

                /* Combine the file path */
                var newFileName = preserveOriginalName ? file.FileName : string.Format("{0}{1}", Guid.NewGuid().ToString(), fileExtension);
                var relativeFilePath = Path.Combine(BasePath ?? string.Empty, folder);
                var filePath = Path.Combine(relativeFilePath, newFileName);

                /* Create directory, if not already exists */
                if (!Directory.Exists(relativeFilePath))
                {
                    Directory.CreateDirectory(relativeFilePath);
                }

                /* Copy the file to the disk */
                using (var fileStream = File.Create(filePath))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }

                filesUploaded.Add(new UploadDetail
                {
                    Directory = Path.Combine(BasePath ?? string.Empty, folder),
                    Path = filePath,
                    Name = newFileName,
                    OriginalName = fileName
                });
            }

            return filesUploaded;
        }

        #endregion
    }
}