///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 14:59:56
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Observable.Impl
{
    /// <summary>
    /// PriorityQueueNode class.
    /// </summary>
    public class PriorityQueueNode<TItem, TPriority> : GenericPriorityQueueNode<TPriority>
    {
        #region Fields


        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Item
        /// </summary>
        public TItem Item { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="PriorityQueueNode"/> class.
        /// </summary>
        /// <param name="data">The item object</param>
        public PriorityQueueNode(TItem item)
        {
            Item = item;
        }

        #endregion

        #region Methods


        #endregion
    }
}