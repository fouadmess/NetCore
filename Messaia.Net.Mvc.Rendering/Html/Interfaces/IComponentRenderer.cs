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
    /// The IComponentRenderer class
    /// </summary>
    public interface IComponentRenderer
    {
        /// <summary>
        /// Renders a component to string.
        /// </summary>
        /// <param name="name">The name of the view component</param>
        /// <param name="arguments">Arguments to be passed to the invoked view component method</param>
        /// <returns></returns>
        Task<string> RenderComponentAsync(string name, object arguments = null);

        /// <summary>
        /// Renders a component to string.
        /// </summary>
        /// <param name="arguments">Arguments to be passed to the invoked view component method</param>
        /// <returns></returns>
        Task<string> RenderComponentAsync<TModel>(object arguments = null);
    }
}