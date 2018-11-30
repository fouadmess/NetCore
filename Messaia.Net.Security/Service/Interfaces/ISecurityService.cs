///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Security
{
    using System;
    using Messaia.Net.Observable;

    /// <summary>
    /// The ISecurityService interface
    /// </summary>
    public interface ISecurityService<TEntity> : IObserver<ICommand> where TEntity : class { }
}