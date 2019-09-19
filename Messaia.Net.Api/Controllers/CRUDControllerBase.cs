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
    using AutoMapper;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;
    using Messaia.Net.Model;
    using Messaia.Net.Observable;
    using Messaia.Net.Service;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// The CRUDController class
    /// </summary>
    [Route("api/[controller]")]
    [ValidateModelState]
    public abstract class CRUDControllerBase<TService, TEntity, TEntityViewModel> : ControllerBase
        where TService : IEntityService<TEntity>
        where TEntity : class, IEntity<int>, new()
        where TEntityViewModel : class
    {
        #region Fields

        /// <summary>
        /// An instance of ILogger
        /// </summary>
        protected readonly ILogger logger;

        /// <summary>
        /// Track single loaded entity
        /// </summary>
        protected bool trackableSingle = false;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Service
        /// </summary>
        public TService Service { get; private set; }

        /// <summary>
        /// Gets or sets the PatchForbiddenPaths
        /// </summary>
        protected List<string> PatchForbiddenPaths { get; set; } = new List<string>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="CRUDController"/> class.
        /// </summary>
        /// <param name="service">The entity service</param>
        /// <param name="securityService">The security service</param>
        /// <param name="logger">Logger instance</param>
        public CRUDControllerBase(TService service, ILogger logger)
        {
            this.Service = service;
            this.logger = logger;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="service">The entity service</param>
        public CRUDControllerBase(TService service) : this(service, null) { }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a single entity by ID
        /// GET: /<controller>/{id}
        /// </summary>
        /// <param name="id">The entoty ID</param>
        /// <returns>Type: IActionResult</returns>
        [HttpGet("{id:int}")]
        public virtual async Task<IActionResult> GetAsync(int id)
        {
            /* Trigger BeforeRead event */
            this.OnBeforeRead(id);

            /* Load the entity and check if it exists */
            var entity = await this.Service.GetAsync(x => x.Id == id, true, this.trackableSingle);
            if (entity == null)
            {
                this.logger.LogWarning(LoggingEvents.GetItemNotFound, "GetAsync({ID}) NOT FOUND", id);
                return NotFound(id);
            }

            /* Trigger AfterRead event */
            this.OnAfterRead(entity);

            return MappedResult<TEntityViewModel>(entity);
        }

        /// <summary>
        /// Saves an entity.
        /// </summary>
        /// <param name="viewModel">The view model</param>
        /// <returns>Type: IActionResult</returns>
        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody]TEntityViewModel viewModel, bool updateOnConflict = false)
        {
            /* Map the view model */
            var entity = this.Map<TEntity>(viewModel);

            /* Validates the entity */
            if (!await this.IsValidAsync(entity))
            {
                return BadRequest(this.ModelState);
            }

            /* Trigger BeforeCreate event */
            this.OnBeforeCreate(entity, viewModel);

            /* Revalidate the model */
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            /* Save the entity */
            var result = await this.Service.CreateAsync(entity, true, updateOnConflict);
            if (!result.Succeeded)
            {
                /* Write some logs */
                this.logger.LogWarning(LoggingEvents.InsertItem, "Item {TYPE} could no be created", entity.GetType().Name);

                /* Add errors to model state and return a bad request */
                this.AddErrors(result);
                return BadRequest(this.ModelState);
            }

            /* Trigger AfterCreate event */
            this.OnAfterCreate(entity, viewModel);

            /* Write some logs */
            this.logger.LogInformation(LoggingEvents.InsertItem, "Item {ID} Created", entity.Id);

            return CreatedAtRoute(new { id = entity.Id }, entity);
        }

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="viewModel">The view model</param>
        /// <returns>Type: IActionResult</returns>
        [HttpPut("{id:int}")]
        public virtual async Task<IActionResult> Put(int id, [FromBody]TEntityViewModel viewModel)
        {
            /* Load the entity and check if it exists */
            var entity = await this.Service.GetAsync(x => x.Id == id);
            if (entity == null)
            {
                this.logger.LogWarning(LoggingEvents.GetItemNotFound, "Update({ID}) NOT FOUND", id);
                return NotFound(id);
            }

            /* Use automapper to map the ViewModel ontop of the database object */
            this.Map(viewModel, entity);

            /* Validates the entity */
            if (!await this.IsValidAsync(entity))
            {
                return BadRequest(this.ModelState);
            }

            /* Trigger BeforeUpdate event */
            this.OnBeforeUpdate(entity, viewModel);

            /* Revalidate the model */
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            /* Update the entity */
            var result = await this.Service.UpdateAsync(entity);
            if (!result.Succeeded)
            {
                /* Write some logs */
                this.logger.LogWarning(LoggingEvents.InsertItem, "Item {TYPE} could no be updated", entity.GetType().Name);

                /* Add errors to model state and return a bad request */
                this.AddErrors(result);
                return BadRequest(this.ModelState);
            }

            /* Trigger AfterUpdate event */
            this.OnAfterUpdate(entity, viewModel);

            /* Write some logs */
            this.logger.LogInformation(LoggingEvents.UpdateItem, "Item {ID} Updated", entity.Id);

            return new NoContentResult();
        }

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="patchedViewModel">The view model</param>
        /// <returns>Type: IActionResult</returns>
        [HttpPatch("{id:int}")]
        public virtual async Task<IActionResult> Patch(int id, [FromBody]JsonPatchDocument<TEntityViewModel> patchedViewModel)
        {
            /* Check if the user is allowed to perform this action */
            if (patchedViewModel.Operations.Any(x => this.PatchForbiddenPaths?.Any(y => y.ToLower().Equals(x.path.ToLower())) == true))
            {
                return BadRequest(new { Message = "You are not authorized to perform this action!" });
            }

            /* Load the entity and check if it exists */
            var entity = await this.Service.GetAsync(x => x.Id == id);
            if (entity == null)
            {
                this.logger.LogWarning(LoggingEvents.GetItemNotFound, "Update({ID}) NOT FOUND", id);
                return NotFound(id);
            }

            /* Use Automapper to map the database object to the ViewModel object */
            var viewModel = Mapper.Map<TEntityViewModel>(entity);

            /* Apply the patch to that ViewModel */
            patchedViewModel.ApplyTo(viewModel);

            /* Use automapper to map the ViewModel ontop of the database object */
            this.Map(viewModel, entity);

            /* Validates the entity */
            if (!await this.IsValidAsync(entity))
            {
                return BadRequest(this.ModelState);
            }

            /* Trigger BeforeUpdate event */
            this.OnBeforeUpdate(entity, viewModel);

            /* Revalidate the model */
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            /* Update the entity */
            var result = await this.Service.UpdateAsync(entity);
            if (!result.Succeeded)
            {
                /* Write some logs */
                this.logger.LogWarning(LoggingEvents.InsertItem, "Item {TYPE} could no be updated", entity.GetType().Name);

                /* Add errors to model state and return a bad request */
                this.AddErrors(result);
                return BadRequest(this.ModelState);
            }

            /* Trigger AfterUpdate event */
            this.OnAfterUpdate(entity, viewModel);

            /* Write some logs */
            this.logger.LogInformation(LoggingEvents.UpdateItem, "Item {ID} Updated", entity.Id);

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            /* Trigger BeforeDelete event */
            this.OnBeforeDelete(id);

            /* Delete the entity */
            var result = await this.Service.DeleteAsync(x => x.Id == id);
            if (!result.Succeeded)
            {
                /* Write some logs */
                this.logger.LogWarning(LoggingEvents.InsertItem, "Item {ID} could no be deleted", id);

                /* Add errors to model state and return a bad request */
                this.AddErrors(result);
                return BadRequest(this.ModelState);
            }

            /* Trigger AfterDelete event */
            this.OnAfterDelete(id);

            /* Write some logs */
            this.logger.LogInformation(LoggingEvents.DeleteItem, "Item {ID} Deleted", id);

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public virtual async Task<IActionResult> Delete(int[] ids)
        {
            foreach (var id in ids)
            {
                /* Trigger BeforeDelete event */
                this.OnBeforeDelete(id);

                var result = await this.Service.DeleteAsync(x => x.Id == id);
                if (!result.Succeeded)
                {
                    /* Write some logs */
                    this.logger.LogWarning(LoggingEvents.InsertItem, "Item {ID} could no be deleted", id);

                    /* Add errors to model state and return a bad request */
                    this.AddErrors(result);
                    return BadRequest(this.ModelState);
                }

                /* Trigger AfterDelete event */
                this.OnAfterDelete(id);
            }

            /* Write some logs */
            this.logger.LogInformation(LoggingEvents.DeleteItem, "Items {ID} Deleted", string.Join(",", ids));

            return new NoContentResult();
        }

        #region Events

        /// <summary>
        /// Occurs before the entity is read
        /// Override this method if you are interested in BeforeRead events.
        /// </summary>
        /// <param name="id"></param>
        protected virtual void OnBeforeRead(int id) { }

        /// <summary>
        /// Occurs after the entity is read
        /// Override this method if you are interested in AfterRead events.
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void OnAfterRead(TEntity entity) { }

        /// <summary>
        /// Occurs before the entity is created
        /// Override this method if you are interested in BeforeCreate events.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="viewModel"></param>
        protected virtual void OnBeforeCreate(TEntity entity, TEntityViewModel viewModel) { }

        /// <summary>
        /// Occurs after the entity is created
        /// Override this method if you are interested in AfterCreate events.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="viewModel"></param>
        protected virtual void OnAfterCreate(TEntity entity, TEntityViewModel viewModel) { }

        /// <summary>
        /// Occurs before the entity is updated
        /// Override this method if you are interested in BeforeUpdate events.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="viewModel"></param>
        protected virtual void OnBeforeUpdate(TEntity entity, TEntityViewModel viewModel) { }

        /// <summary>
        /// Occurs after the entity is updated
        /// Override this method if you are interested in AfterUpdate events.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="viewModel"></param>
        protected virtual void OnAfterUpdate(TEntity entity, TEntityViewModel viewModel) { }

        /// <summary>
        /// Occurs before the entity is deleted
        /// Override this method if you are interested in BeforeDelete events.
        /// </summary>
        /// <param name="id"></param>
        protected virtual void OnBeforeDelete(int id) { }

        /// <summary>
        /// Occurs after the entity is deleted
        /// Override this method if you are interested in AfterDelete events.
        /// </summary>
        /// <param name="id"></param>
        protected virtual void OnAfterDelete(int id) { }

        #endregion

        #region Helpers

        /// <summary>
        /// Returns a mm
        /// Mappes the values and creates an OkObjectResult object that produces an OK (200) response.
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public virtual IActionResult MappedResult<TDestination>(object source)
        {
            /* No mapping nedded, if source is type of TDestination */
            if (source is TDestination)
            {
                return Ok((TDestination)source);
            }

            return Ok(this.Map<TDestination>(source));
        }

        /// <summary>
        /// Validates the specified entity.
        /// </summary>
        /// <param name="entity">Type: TEntity</param>
        /// <returns>Type: boolean</returns>
        protected virtual async Task<bool> IsValidAsync(TEntity entity)
        {
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Maps the specified model to the specified view model.
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        protected virtual TDestination Map<TDestination>(object source)
        {
            /* No mapping nedded, if source is type of TDestination */
            if (source is TDestination)
            {
                return (TDestination)source;
            }

            return Mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// Maps the specified model to the specified view model.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        protected virtual TDestination Map<TDestination, TSource>(TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }

        /// <summary>
        /// Adds an observer to ther entity service
        /// </summary>
        /// <param name="observer">The observer instance</param>
        /// <returns>Type: boolean</returns>
        protected virtual void Subscribe(IObserver<ICommand> observer)
        {
            this.Service?.Subscribe(observer);
        }

        /// <summary>
        /// Adds errors to model state
        /// </summary>
        /// <param name="result"></param>
        protected virtual void AddErrors(ServiceResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        #endregion

        #endregion
    }
}