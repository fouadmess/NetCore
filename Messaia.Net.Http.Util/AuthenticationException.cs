///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 20:27:47
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Http.Util
{
    using System;

    /// <summary>
    /// AuthenticationException class.
    /// </summary>
    public class AuthenticationException : Exception
    {
        /// <summary>
        /// Initializes an instance of the <see cref="AuthenticationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AuthenticationException(string message) : base(message) { }

        /// <summary>
        /// Initializes an instance of the <see cref="AuthenticationException"/> class.
        /// </summary>
        /// <param name="innerExeption">The exception that is the cause of the current exception, or a null reference</param>
        public AuthenticationException(string message, Exception innerExeption) : base(message, innerExeption) { }
    }
}