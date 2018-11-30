///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 08:37:41
///   Copyright (©)   2017, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.QuartzExtensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Quartz;
    using Quartz.Simpl;
    using Quartz.Spi;
    using System;

    /// <summary>
    /// JobFactoryActivator class.
    /// </summary>
    public class JobFactoryActivator : SimpleJobFactory
    {
        #region Fields

        /// <summary>
        /// An instance of the IServiceProvider
        /// </summary>
        readonly IServiceProvider serviceProvider;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="JobFactoryActivator"/> class.
        /// <param name="serviceProvider"></param>
        /// </summary>        
        public JobFactoryActivator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resolve the job instance.
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="scheduler"></param>
        /// <returns></returns>
        public override IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                /* This will inject dependencies that the job requires */
                return (IJob)this.serviceProvider.GetRequiredService(bundle.JobDetail.JobType);
            }
            catch (Exception e)
            {
                throw new SchedulerException(string.Format("Problem while instantiating job '{0}' from the IServiceProviderFactory.", bundle.JobDetail.Key), e);
            }
        }

        #endregion
    }
}