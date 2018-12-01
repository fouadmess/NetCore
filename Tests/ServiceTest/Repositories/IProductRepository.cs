///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           24.02.2018 13:23:25
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace ServiceTest.Repositories
{
    using Models;
    using Messaia.Net.Repository;

    /// <summary>
    /// Repository interface for the <see cref="Product"/> model class.
    /// </summary>
    public interface IProductRepository : IGenericRepository<Product> { }
}