///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 23:05:16
///   Copyright (©)   2017, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Service
{
    using System;

    /// <summary>
    /// Encapsulates an error from the service subsystem.
    /// </summary>
    public class ServiceError
    {
        #region Properties

        /// <summary>
        /// Gets or sets the code for this error.
        /// </summary>
        /// <value>
        /// The code for this error.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the description for this error.
        /// </summary>
        /// <value>
        /// The description for this error.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Exception
        /// </summary>
        public Exception Exception { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="ServiceError"/> class.
        /// </summary>
        public ServiceError() { }

        /// <summary>
        /// Initializes an instance of the <see cref="ServiceError"/> class.
        /// </summary>
        public ServiceError(string code, string description) : this(code, description, null) { }

        /// <summary>
        /// Initializes an instance of the <see cref="ServiceError"/> class.
        /// </summary>
        public ServiceError(string code, string description, Exception exception)
        {
            this.Code = code;
            this.Description = description;
            this.Exception = exception;
        }

        #endregion
    }
}