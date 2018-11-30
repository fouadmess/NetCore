///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Observable.Impl
{
    using System;

    /// <summary>
    /// The Unsubscriber class
    /// Unsubscribes registered observers in the service class
    /// </summary>
    /// <typeparam name="ICommand"></typeparam>
    public class Unsubscriber<ICommand> : IDisposable
    {
        /// <summary>
        /// The observers list
        /// </summary>
        private IPriorityQueue<IObserver<ICommand>> observers;

        /// <summary>
        /// The oberserver instance
        /// </summary>
        private IObserver<ICommand> observer;

        /// <summary>
        /// Unsucsribes the observers
        /// </summary>
        /// <param name="observers"></param>
        /// <param name="observer"></param>
        public Unsubscriber(IPriorityQueue<IObserver<ICommand>> observers, IObserver<ICommand> observer)
        {
            this.observers = observers;
            this.observer = observer;
        }

        /// <summary>
        /// Frees resources
        /// </summary>
        public void Dispose()
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }
    }
}