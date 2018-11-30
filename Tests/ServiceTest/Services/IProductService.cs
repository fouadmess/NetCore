///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           24.02.2018 13:48:32
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace ServiceTest.Services
{
    using Models;
    using Messaia.Net.Service;

    /// <summary>
    /// Service interface for the <see cref="Product"/> model class.
    /// </summary>
	public interface IProductService : IEntityService<Product> {}
}