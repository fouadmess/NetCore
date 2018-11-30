///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Mvc.Rendering
{
    using System;
    using System.IO;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Routing;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
    using System.Text.Encodings.Web;

    /// <summary>
    /// The ViewRenderer class
    /// </summary>
    public class RazorRenderer : IViewRenderer, IComponentRenderer
    {
        #region Fields

        /// <summary>
        /// An instance of IRazorViewEngine
        /// </summary>
        private IRazorViewEngine viewEngine;

        /// <summary>
        /// An instance of ITempDataProvider
        /// </summary>
        private ITempDataProvider tempDataProvider;

        /// <summary>
        /// An instance of IServiceProvider
        /// </summary>
        private IServiceProvider serviceProvider;

        /// <summary>
        /// An instance of IViewComponentHelper
        /// </summary>
        private IViewComponentHelper viewComponentHelper;

        /// <summary>
        /// An instance of HtmlEncoder
        /// </summary>
        private HtmlEncoder htmlEncoder;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="RazorRenderer"/> class.
        /// </summary>
        /// <param name="viewEngine"></param>
        /// <param name="tempDataProvider"></param>
        /// <param name="serviceProvider"></param>
        public RazorRenderer(
            IRazorViewEngine viewEngine, 
            ITempDataProvider tempDataProvider, 
            IServiceProvider serviceProvider, 
            IViewComponentHelper viewComponentHelper,
            HtmlEncoder htmlEncoder
        )
        {
            this.viewEngine = viewEngine;
            this.tempDataProvider = tempDataProvider;
            this.serviceProvider = serviceProvider;
            this.viewComponentHelper = viewComponentHelper;
            this.htmlEncoder = htmlEncoder;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Renders a view to string.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="name"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> RenderViewAsync<TModel>(string name, TModel model)
        {
            var actionContext = GetActionContext();

            /* Finds the view with the given viewName using view locations and information from the context. */
            var viewEngineResult = viewEngine.FindView(actionContext, name, false);
            if (!viewEngineResult.Success)
            {
                throw new InvalidOperationException(string.Format("Couldn't find view '{0}'", name));
            }

            /* Get the view object */
            var view = viewEngineResult.View;

            using (var output = new StringWriter())
            {
                /* Create a new instance of ViewContext. */
                var viewContext = new ViewContext(
                    actionContext,
                    view,
                    new ViewDataDictionary<TModel>(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { Model = model },
                    new TempDataDictionary(actionContext.HttpContext, tempDataProvider),
                    output,
                    new HtmlHelperOptions()
                );

                /* Asynchronously renders the view using the specified context. */
                await view.RenderAsync(viewContext);

                /* Return the result as a string */
                return output.ToString();
            }
        }

        /// <summary>
        /// Renders a component to string.
        /// </summary>
        /// <param name="name">The name of the view component</param>
        /// <param name="arguments">Arguments to be passed to the invoked view component method</param>
        /// <returns></returns>
        public async Task<string> RenderComponentAsync(string name, object arguments = null)
        {
            /* Create an instance of StringWriter to write to */
            using (var writer = new StringWriter())
            {
                /* Contextualizes the instance with the specified viewContext */
                (this.viewComponentHelper as IViewContextAware)?.Contextualize(this.GetViewContext(writer));

                /* Invokes a view component with the specified name */
                var htmlContent = await this.viewComponentHelper.InvokeAsync(name, arguments);

                /* Writes the content by encoding it with the specified encoder to the specified writer */
                htmlContent.WriteTo(writer, this.htmlEncoder);

                return writer.ToString();
            }
        }

        /// <summary>
        /// Renders a component to string.
        /// </summary>
        /// <param name="arguments">Arguments to be passed to the invoked view component method</param>
        /// <returns></returns>
        public async Task<string> RenderComponentAsync<TModel>(object arguments = null)
        {
            /* Create an instance of StringWriter to write to */
            using (var writer = new StringWriter())
            {
                /* Contextualizes the instance with the specified viewContext */
                (this.viewComponentHelper as IViewContextAware)?.Contextualize(this.GetViewContext(writer));

                /* Invokes a view component with the specified name */
                var htmlContent = await this.viewComponentHelper.InvokeAsync(typeof(TModel), arguments);

                /* Writes the content by encoding it with the specified encoder to the specified writer */
                htmlContent.WriteTo(writer, this.htmlEncoder);

                return writer.ToString();
            }
        }

        #region Helpers

        /// <summary>
        /// Creates a new instance of ActionContext.
        /// </summary>
        /// <returns></returns>
        private ActionContext GetActionContext()
        {
            /* Create a new instance of DefaultHttpContext */
            var httpContext = new DefaultHttpContext();

            /* Set the service provider */
            httpContext.RequestServices = serviceProvider;

            /* Create a new instance of ActionContext */
            return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        }

        /// <summary>
        /// Creates a new instance of ViewContext.
        /// </summary>
        /// <param name="writer"></param>
        /// <returns></returns>
        private ViewContext GetViewContext(TextWriter writer)
        {
            /* Create an ActionContext instance */
            var actionContext = GetActionContext();

            /* Create an ViewContext instance */
            return new ViewContext(
                actionContext,
                NullView.Instance,
                new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { },
                new TempDataDictionary(actionContext.HttpContext, tempDataProvider),
                writer,
                new HtmlHelperOptions()
            );
        }

        #endregion

        #endregion
    }
}