///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 10:58:04
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Common
{
    using System;

    /// <summary>
    /// Variance class
    /// </summary>
    public class Variance
    {
        /// <summary>
        /// Gets or sets the Object
        /// </summary>
        public object Object { get; set; }

        /// <summary>
        /// Gets or sets the ObjectType
        /// </summary>
        public Type ObjectType { get; set; }

        /// <summary>
        /// Gets or sets the PropertyType
        /// </summary>
        public Type PropertyType { get; set; }

        /// <summary>
        /// Gets or sets the NavigationName
        /// </summary>
        public string NavigationName { get; set; }

        /// <summary>
        /// Gets or sets the PropertyName
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the OriginalValue
        /// </summary>
        public object OriginalValue { get; set; }

        /// <summary>
        /// Gets or sets the CurrentValue
        /// </summary>
        public object CurrentValue { get; set; }
    }
}