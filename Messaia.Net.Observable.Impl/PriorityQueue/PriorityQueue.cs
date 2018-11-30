///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 14:50:12
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Observable.Impl
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// PriorityQueue class.
    /// </summary>
    public class PriorityQueue<TItem, TPriority> : IPriorityQueue<TItem, TPriority>
        where TPriority : IComparable<TPriority>
    {
        #region Fields

        /// <summary>
        /// The intila size of the queue
        /// </summary>
        private const int INITIAL_QUEUE_SIZE = 10;

        /// <summary>
        /// The query object
        /// </summary>
        private readonly GenericPriorityQueue<PriorityQueueNode<TItem, TPriority>, TPriority> queue;

        #endregion

        #region Properties

        /// <summary>
        /// Returns the head of the queue, without removing it (use Dequeue() for that).
        /// Throws an exception when the queue is empty.
        /// O(1)
        /// </summary>
        public TItem First
        {
            get
            {
                lock (queue)
                {
                    if (queue.Count <= 0)
                    {
                        throw new InvalidOperationException("Cannot call .First on an empty queue");
                    }

                    PriorityQueueNode<TItem, TPriority> first = queue.First;
                    return (first != null ? first.Item : default(TItem));
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="PriorityQueue"/> class.
        /// </summary>
        public PriorityQueue()
        {
            this.queue = new GenericPriorityQueue<PriorityQueueNode<TItem, TPriority>, TPriority>(INITIAL_QUEUE_SIZE);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Given an item of type T, returns the exist SimpleNode in the queue
        /// </summary>
        private PriorityQueueNode<TItem, TPriority> GetExistingNode(TItem item)
        {
            var comparer = EqualityComparer<TItem>.Default;
            foreach (var node in queue)
            {
                if (comparer.Equals(node.Item, item))
                {
                    return node;
                }
            }
            throw new InvalidOperationException("Item cannot be found in queue: " + item);
        }

        /// <summary>
        /// Returns the number of nodes in the queue.
        /// O(1)
        /// </summary>
        public int Count
        {
            get
            {
                lock (queue)
                {
                    return queue.Count;
                }
            }
        }

        /// <summary>
        /// Removes every node from the queue.
        /// O(n)
        /// </summary>
        public void Clear()
        {
            lock (queue)
            {
                queue.Clear();
            }
        }

        /// <summary>
        /// Returns whether the given item is in the queue.
        /// O(n)
        /// </summary>
        public bool Contains(TItem item)
        {
            lock (queue)
            {
                var comparer = EqualityComparer<TItem>.Default;
                foreach (var node in queue)
                {
                    if (comparer.Equals(node.Item, item))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Removes the head of the queue (node with minimum priority; ties are broken by order of insertion), and returns it.
        /// If queue is empty, throws an exception
        /// O(log n)
        /// </summary>
        public TItem Dequeue()
        {
            lock (queue)
            {
                if (queue.Count <= 0)
                {
                    throw new InvalidOperationException("Cannot call Dequeue() on an empty queue");
                }

                PriorityQueueNode<TItem, TPriority> node = queue.Dequeue();
                return node.Item;
            }
        }

        /// <summary>
        /// Enqueue a node to the priority queue.  Lower values are placed in front. Ties are broken by first-in-first-out.
        /// This queue automatically resizes itself, so there's no concern of the queue becoming 'full'.
        /// Duplicates are allowed.
        /// O(log n)
        /// </summary>
        public void Enqueue(TItem item, TPriority priority)
        {
            lock (queue)
            {
                PriorityQueueNode<TItem, TPriority> node = new PriorityQueueNode<TItem, TPriority>(item);
                if (queue.Count == queue.MaxSize)
                {
                    queue.Resize(queue.MaxSize * 2 + 1);
                }
                queue.Enqueue(node, priority);
            }
        }

        /// <summary>
        /// Removes an item from the queue.  The item does not need to be the head of the queue.  
        /// If the item is not in the queue, an exception is thrown.  If unsure, check Contains() first.
        /// If multiple copies of the item are enqueued, only the first one is removed. 
        /// O(n)
        /// </summary>
        public void Remove(TItem item)
        {
            lock (queue)
            {
                try
                {
                    queue.Remove(GetExistingNode(item));
                }
                catch (InvalidOperationException ex)
                {
                    throw new InvalidOperationException("Cannot call Remove() on a node which is not enqueued: " + item, ex);
                }
            }
        }

        /// <summary>
        /// Call this method to change the priority of an item.
        /// Calling this method on a item not in the queue will throw an exception.
        /// If the item is enqueued multiple times, only the first one will be updated.
        /// (If your requirements are complex enough that you need to enqueue the same item multiple times <i>and</i> be able
        /// to update all of them, please wrap your items in a wrapper class so they can be distinguished).
        /// O(n)
        /// </summary>
        public void UpdatePriority(TItem item, TPriority priority)
        {
            lock (queue)
            {
                try
                {
                    PriorityQueueNode<TItem, TPriority> updateMe = GetExistingNode(item);
                    queue.UpdatePriority(updateMe, priority);
                }
                catch (InvalidOperationException ex)
                {
                    throw new InvalidOperationException("Cannot call UpdatePriority() on a node which is not enqueued: " + item, ex);
                }
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TItem> GetEnumerator()
        {
            List<TItem> queueData = new List<TItem>();
            lock (queue)
            {
                /* Copy to a separate list because we don't want to 'yield return' inside a lock */
                foreach (var node in queue)
                {
                    queueData.Add(node.Item);
                }
            }

            return queueData.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Validates the query.
        /// </summary>
        /// <returns></returns>
        public bool IsValidQueue()
        {
            lock (queue)
            {
                return queue.IsValidQueue();
            }
        }

        #endregion
    }
}