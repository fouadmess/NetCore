///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Service
{
    using System;

    /// <summary>
    /// IObservable class.
    /// </summary>
    public interface IObservableService<out T, TPriority> : IObservable<T>
    {
        /// <summary>
        /// Notifies the provider that an observer is to receive notifications.
        /// </summary>
        /// <param name="observer">The object that is to receive notifications.</param>
        /// <param name="priority">The priority</param>
        /// <returns></returns>
        IDisposable Subscribe(IObserver<T> observer, TPriority priority);
    }
}