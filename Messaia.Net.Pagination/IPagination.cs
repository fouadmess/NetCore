///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 23:16:08
///   Copyright (©)   2017, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Pagination
{
    using System.Collections;

    /// <summary>
    /// IPagination class.
    /// </summary>
    public interface IPagination
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Items
        /// </summary>
        IList Items { get; set; }

        /// <summary>
        /// Gets or sets the Page
        /// </summary>
        int Page { get; set; }

        /// <summary>
        /// Gets or sets the PageSize
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the Page
        /// </summary>
        int Total { get; set; }

        /// <summary>
        /// Gets or sets the Page
        /// </summary>
        int PageCount { get; set; }

        #endregion
    }
}