///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Api
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Messaia.Net.Model;
    using Messaia.Net.Pagination;
    using Messaia.Net.Service;

    /// <summary>
    /// The CRUDController class
    /// </summary>
    public abstract class CRUDController<TService, TEntity, TEntityViewModel> : CRUDControllerBase<TService, TEntity, TEntityViewModel>
        where TService : IEntityService<TEntity>
        where TEntity : class, IEntity<int>, new()
        where TEntityViewModel : class, new()
    {
        #region Fields

        /// <summary>
        /// Track loaded entities
        /// </summary>
        protected bool trackableList = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="service">The entity service</param>
        /// <param name="logger">Logger instance</param>
        public CRUDController(TService service, ILogger logger) : base(service, logger) { }

        /// <summary>
        /// Initializes an instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="service">The entity service</param>
        public CRUDController(TService service) : this(service, null) { }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all entities
        /// GET: /<controller>/
        /// </summary>
        /// <param name="filter">Applys filter to the query</param>
        /// <returns>Type: IActionResult</returns>
        [HttpGet]
        public virtual async Task<IActionResult> GetListAsync(int page, int pageSize)
        {
            if (page > 0 || pageSize > 0)
            {
                return MappedResult<Pagination<TEntityViewModel>>(this.Service.GetList(page, pageSize, null, true, this.trackableList));
            }

            return MappedResult<List<TEntityViewModel>>(await this.Service.GetListAsync(x => true, true, this.trackableList));
        }

        /// <summary>
        /// Gets all entities
        /// GET: /<controller>/
        /// </summary>
        /// <param name="filter">Applys filter to the query</param>
        /// <returns>Type: IActionResult</returns>
        [HttpGet("count")]
        public virtual async Task<IActionResult> CountAsync()
        {
            return Ok(new { Value = await this.Service.CountAsync() });
        }

        #endregion
    }
}