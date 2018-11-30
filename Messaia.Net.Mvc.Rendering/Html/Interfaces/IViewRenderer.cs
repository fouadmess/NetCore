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
    using System.Threading.Tasks;

    /// <summary>
    /// The IViewRenderer class
    /// </summary>
    public interface IViewRenderer
    {
        /// <summary>
        /// Renders a view to string.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="name"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<string> RenderViewAsync<TModel>(string name, TModel model);
    }
}