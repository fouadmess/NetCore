///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 14:14:45
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Observable
{
    using System;

    /// <summary>
    /// ArgumentOrderAttribute class.
    /// </summary>
    public class ArgumentOrderAttribute : Attribute
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Order
        /// </summary>
        public int Order { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="ArgumentOrderAttribute"/> class.
        /// </summary>
        public ArgumentOrderAttribute(int order)
        {
            this.Order = order;
        }

        #endregion        
    }
}