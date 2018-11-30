///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 01:23:46
///   Copyright (©)   2017, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Observable
{
    using System;

    /// <summary>
    /// Base observer interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseObserver : IObserver<ICommand> { }
}