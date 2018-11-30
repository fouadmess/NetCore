///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 11:58:36
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Service
{
    using System;

    /// <summary>
    /// ServiceException class.
    /// </summary>
    public class ServiceException : Exception
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Code
        /// </summary>
        public string Code { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="ServiceException"/> class.
        /// </summary>
        public ServiceException() { }

        /// <summary>
        /// Initializes an instance of the <see cref="ServiceException"/> class.
        /// </summary>
        public ServiceException(string code, string message, Exception innerException) : base(message, innerException)
        {
            this.Code = code;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="ServiceException"/> class.
        /// </summary>
        public ServiceException(string code, string message) : this(code, message, null) { }

        /// <summary>
        /// Initializes an instance of the <see cref="ServiceException"/> class.
        /// </summary>
        public ServiceException(string message) : this(null, message, null) { }

        /// <summary>
        /// Initializes an instance of the <see cref="ServiceException"/> class.
        /// </summary>
        public ServiceException(string message, Exception innerException) : this(null, message, innerException) { }

        #endregion
    }
}