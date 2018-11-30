///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           02.03.2018 01:38:13
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace QuartzTest
{
    using Quartz;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// HelloJob class.
    /// </summary>
    [DisallowConcurrentExecution()]
    public class HelloJob : IJob
    {
        /// <summary>
        /// Ececutes the job
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            /* Get job arguments */
            var args = context.JobDetail.JobDataMap.Get("dynamic") as dynamic;
            if (args == null)
            {
                throw new ArgumentNullException("dynamic");
            }

            Console.WriteLine("My name is {0}", args.Name);
            await Task.FromResult(0);
        }
    }
}