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
    using Messaia.Net.Repository;
    using Messaia.Net.Service;

    /// <summary>
    /// The CRUDController class
    /// </summary>
    public abstract class CRUDController<TService, TEntity, TEntityViewModel, TFilter> : CRUDControllerBase<TService, TEntity, TEntityViewModel>
        where TService : IEntityService<TEntity>
        where TEntity : class, IEntity<int>, new()
        where TEntityViewModel : class, new()
        where TFilter : IFilter<TEntity>, new()
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
        public virtual async Task<IActionResult> GetListAsync(TFilter filter, int page, int pageSize)
        {
            if (page > 0 || pageSize > 0)
            {
                var pagination = this.Service.GetList(page, pageSize, filter, true, this.trackableList);

                this.OnAfterReadList(pagination);

                return MappedResult<Pagination<TEntityViewModel>>(pagination);
            }

            var list = await this.Service.GetListAsync(filter, true, this.trackableList);

            this.OnAfterReadList(list);

            return MappedResult<List<TEntityViewModel>>(list);
        }

        /// <summary>
        /// Gets all entities
        /// GET: /<controller>/
        /// </summary>
        /// <param name="filter">Applys filter to the query</param>
        /// <returns>Type: IActionResult</returns>
        [HttpGet("count")]
        public virtual async Task<IActionResult> CountAsync(TFilter filter)
        {
            return Ok(new { Value = await this.Service.CountAsync(filter) });
        }

        /// <summary>
        /// Returns a filtred dynamic list.
        /// </summary>
        /// <param name="filter">The lookup criteria</param>
        /// <returns></returns>
        [HttpGet("lookup")]
        public async Task<IActionResult> LookupAsync(TFilter filter)
        {
            if (this.Service is ILookupService<TEntity> lookupService)
            {
                return Ok(await lookupService.LookupAsync(filter));
            }

            return BadRequest("The Service doesn't implements ILookupService");
        }

        #region Helpers

        /// <summary>
        /// Occurs before the entity list is read
        /// Override this method if you are interested in OnBeforeReadList events.
        /// </summary>
        /// <param name="id"></param>
        protected virtual void OnBeforeReadList(TFilter filter, int page, int pageSize) { }

        /// <summary>
        /// Occurs after the entity list is read
        /// Override this method if you are interested in OnAfterReadList events.
        /// </summary>
        /// <param name="entityList"></param>
        protected virtual void OnAfterReadList(IPagination<TEntity> entityList) { }

        /// <summary>
        /// Occurs after the entity list is read
        /// Override this method if you are interested in OnAfterReadList events.
        /// </summary>
        /// <param name="entityList"></param>
        protected virtual void OnAfterReadList(List<TEntity> entityList) { }

        #endregion

        #endregion
    }
}