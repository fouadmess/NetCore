///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Security
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// The SecurityExceptionFilter class
    /// </summary>
    public class SecurityExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// Handels exceptions.
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception?.InnerException?.GetType();
            if (exceptionType == typeof(NotAuthorizedException))
            {
                if ((bool)context.HttpContext?.User?.Identity?.IsAuthenticated)
                {
                    context.Result = new ForbidResult();
                }
                else
                {
                    context.Result = new ChallengeResult();
                }

                context.Exception = null;
            }

            base.OnException(context);
        }
    }
}