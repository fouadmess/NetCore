///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 18:29:59
///   Copyright (©)   2017, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Http
{
    using System;

    /// <summary>
    /// ExtensionNotAllowedException class.
    /// </summary>
    public class ExtensionNotAllowedException : Exception
    {
        /// <summary>
        /// Initializes an instance of the <see cref="ExtensionNotAllowedException"/> class.
        /// </summary>
        public ExtensionNotAllowedException(string message) : base(message) { }
    }
}