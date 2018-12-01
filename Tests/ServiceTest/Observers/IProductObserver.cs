///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           24.02.2018 13:53:07
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace ServiceTest.Observers
{
    using ServiceTest.Models;
    using Messaia.Net.Observable;

    /// <summary>
    /// IProductObserver class.
    /// </summary>
    public interface IProductObserver : IBaseObserver, IEntityObserver<Product>, IQueryObserver<Product> { }
}