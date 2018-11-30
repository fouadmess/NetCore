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
    /// <summary>
    /// The IHtmlRenderer class
    /// </summary>
    public interface IHtmlRenderer
    {
        /// <summary>
        /// Renders a html string to string.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="name"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        string Render<TModel>(string html, TModel model);
    }
}