///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 18:51:59
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace System.Net.Http
{
    using Newtonsoft.Json;
    using System.Text;

    /// <summary>
    /// JsonContent class.
    /// </summary>
    public class JsonContent : StringContent
    {
        /// <summary>
        /// Initializes an instance of the <see cref="JsonContent"/> class.
        /// </summary>        
        public JsonContent(object obj) : base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json") { }
    }
}