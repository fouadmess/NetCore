///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 23:01:42
///   Copyright (©)   2017, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Service
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents the result of a service operation.
    /// </summary>
    public class ServiceResult
    {
        #region Fields

        /// <summary>
        /// Flag indicating that the operation succeeded
        /// </summary>
        private static readonly ServiceResult _success = new ServiceResult { Succeeded = true };

        /// <summary>
        /// Error container
        /// </summary>
        private List<ServiceError> _errors = new List<ServiceError>();

        #endregion

        #region Properties

        /// <summary>
        /// Flag indicating whether if the operation succeeded or not.
        /// </summary>
        /// <value>True if the operation succeeded, otherwise false.</value>
        public bool Succeeded { get; protected set; }

        /// <summary>
        /// An <see cref="IEnumerable{T}"/> of <see cref="ServiceError"/>s containing an errors
        /// that occurred during the service operation.
        /// </summary>
        /// <value>An <see cref="IEnumerable{T}"/> of <see cref="ServiceError"/>s.</value>
        public IEnumerable<ServiceError> Errors => _errors;

        /// <summary>
        /// Returns an <see cref="ServiceResult"/> indicating a successful service operation.
        /// </summary>
        /// <returns>An <see cref="ServiceResult"/> indicating a successful operation.</returns>
        public static ServiceResult Success => _success;

        #endregion

        #region Methods

        /// <summary>
        /// Creates an <see cref="ServiceResult"/> indicating a failed service operation, with a list of <paramref name="errors"/> if applicable.
        /// </summary>
        /// <param name="errors">An optional array of <see cref="ServiceError"/>s which caused the operation to fail.</param>
        /// <returns>An <see cref="ServiceResult"/> indicating a failed service operation, with a list of <paramref name="errors"/> if applicable.</returns>
        public static ServiceResult Failed(params ServiceError[] errors)
        {
            var result = new ServiceResult { Succeeded = false };
            if (errors != null)
            {
                result._errors.AddRange(errors);
            }
            return result;
        }

        /// <summary>
        /// Converts the value of the current <see cref="ServiceResult"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the current <see cref="ServiceResult"/> object.</returns>
        /// <remarks>
        /// If the operation was successful the ToString() will return "Succeeded" otherwise it returned 
        /// "Failed : " followed by a comma delimited list of error codes from its <see cref="Errors"/> collection, if any.
        /// </remarks>
        public override string ToString()
        {
            return Succeeded ? "Succeeded" : string.Format("{0} : {1}", "Failed", string.Join(",", Errors.Select(x => x.Code).ToList()));
        }

        #endregion
    }
}