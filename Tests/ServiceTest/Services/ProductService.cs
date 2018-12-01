///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           24.02.2018 13:48:45
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace ServiceTest.Services
{
    using Models;
    using Repositories;
    using Messaia.Net.Repository;
    using Messaia.Net.Service.Impl;

    /// <summary>
    /// Service implemetation for the <see cref="Product"/> model class.
    /// </summary>
    public class ProductService : EntityService<Product, IProductRepository>, IProductService
	{
        #region Fields
        

		#endregion

        #region Properties
      

        #endregion

        #region Constructors
      
        /// <summary>
        /// Initializes an instance of the <see cref="ProductService"/> class.
        /// </summary>
		public ProductService(IProductRepository repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork) { }
		
        #endregion

        #region Methods
      

        #endregion
	}
}