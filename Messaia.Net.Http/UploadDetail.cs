///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 14:32:56
///   Copyright (©)   2017, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Http
{
    using System.IO;

    /// <summary>
    /// UploadDetail class.
    /// </summary>
    public class UploadDetail
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Directory
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// Gets or sets the Path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the OriginalName
        /// </summary>
        public string OriginalName { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a FileInfo using the current file path
        /// </summary>
        /// <returns></returns>
        public FileInfo ToFileInfo() => new FileInfo(this.Path);

        #endregion
    }
}