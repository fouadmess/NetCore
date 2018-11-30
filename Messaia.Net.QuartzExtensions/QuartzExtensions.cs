///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 01:17:01
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Quartz
{
    using Quartz.Impl.Matchers;
    using Quartz.Listener;
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    /// <summary>
    /// QuartzExtensions class.
    /// </summary>
    public static class QuartzExtensions
    {
        #region Fields

        /// <summary>
        /// Default job and trigger group
        /// </summary>
        public const string DEFAULT_GROUP = "DEFAULT";

        #endregion

        #region Properties


        #endregion

        #region Constructors


        #endregion

        #region Methods

        /// <summary>
        /// Schedules the jobs
        /// </summary>
        /// <param name="job"></param>
        /// <param name="builderFunc"></param>
        /// <param name="chainedJobs"></param>
        /// <returns></returns>
        public static async Task<bool> ScheduleAsync(this IScheduler scheduler, IJobDetail job, Func<TriggerBuilder, TriggerBuilder> builderFunc = null, params IJobDetail[] chainedJobs)
        {
            /* Get job key */
            var jobKey = job.Key;

            /* Schedule the jon only once! */
            if (await scheduler.CheckExists(jobKey))
            {
                return false;
            }

            /* Trigger the job to run now (one time) */
            var triggerBuilder = TriggerBuilder.Create()
                .WithIdentity(Guid.NewGuid().ToString(), jobKey.Group ?? DEFAULT_GROUP)
                .ForJob(job);

            /* Extend the builder */
            if (builderFunc != null)
            {
                builderFunc(triggerBuilder);
            }
            else
            {
                triggerBuilder
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithMisfireHandlingInstructionFireNow());
            }

            /* Build the trigger */
            var trigger = triggerBuilder.Build();



            /* Tell quartz to schedule the job using our trigger */
            await scheduler.ScheduleJob(job, trigger);

            if (chainedJobs != null)
            {
                /* Create the job chainer */
                var jobChainingJobListener = new JobChainingJobListener("Chainer");

                /* Add the chainer to the job listener */
                scheduler.ListenerManager.AddJobListener(jobChainingJobListener, GroupMatcher<JobKey>.AnyGroup());

                /* Add the chained jobs */
                int i = 0;
                foreach (var chainedJob in chainedJobs)
                {
                    var key = i > 0 ? chainedJobs[i - 1].Key : job.Key;
                    jobChainingJobListener.AddJobChainLink(key, chainedJob.Key);
                    await scheduler.AddJob(chainedJob, true);
                    i++;
                }
            }

            /* Start scheduling */
            await scheduler.Start();

            return true;
        }

        /// <summary>
        /// Schedules the jobs
        /// </summary>
        /// <param name="job"></param>
        /// <param name="builderFunc"></param>
        /// <param name="chainedJobs"></param>
        /// <returns></returns>
        public static async Task<bool> ScheduleAsync<JobType>(this IScheduler scheduler, string identity, string group = null, Func<TriggerBuilder, TriggerBuilder> builderFunc = null) where JobType : IJob
        {
            return await scheduler.ScheduleAsync<JobType>(identity, group, null, builderFunc);
        }

        /// <summary>
        /// Schedules the jobs
        /// </summary>
        /// <param name="job"></param>
        /// <param name="builderFunc"></param>
        /// <param name="chainedJobs"></param>
        /// <returns></returns>
        public static async Task<bool> ScheduleAsync<JobType>(this IScheduler scheduler, string identity, string group = null, object arguments = null, Func<TriggerBuilder, TriggerBuilder> builderFunc = null) where JobType : IJob
        {
            /* Create the main job */
            var mainJob = scheduler.CreateJob(typeof(JobType), identity, group, arguments);

            /* Schedule the main job */
            return await scheduler.ScheduleAsync(mainJob, builderFunc);
        }

        /// <summary>
        /// Schedules the jobs
        /// </summary>
        /// <param name="job"></param>
        /// <param name="chainedJobs"></param>
        /// <returns></returns>
        public static async Task<bool> ScheduleAsync(this IScheduler scheduler, Type jobType, string identity, string group = null, Func<TriggerBuilder, TriggerBuilder> builderFunc = null)
        {
            /* Create the main job */
            var mainJob = scheduler.CreateJob(jobType, identity, group);

            /* Schedule the main job */
            return await scheduler.ScheduleAsync(mainJob, builderFunc);
        }

        /// <summary>
        /// Stops a scheduler job
        /// </summary>
        /// <param name="jobKey"></param>
        public static async Task<bool> StopJob(this IScheduler scheduler, JobKey jobKey, bool interrupt = false)
        {
            if (jobKey == null)
            {
                throw new ArgumentNullException(nameof(jobKey));
            }

            /* Schedule the jon only once! */
            if (!await scheduler.CheckExists(jobKey))
            {
                return false;
            }

            /* Interrupt the job */
            if (interrupt)
            {
                await scheduler.Interrupt(jobKey);
            }

            /* Stop the job */
            return await scheduler.DeleteJob(jobKey);
        }

        /// <summary>
        /// Stops a scheduler job
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="group"></param>
        public static async Task<bool> StopJob(this IScheduler scheduler, string identity, string group = null, bool interrupt = false)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            return await scheduler.StopJob(new JobKey(identity, group ?? DEFAULT_GROUP), interrupt);
        }

        /// <summary>
        /// Creates a new job.
        /// </summary>
        /// <param name="identity">Optional job identity</param>
        /// <param name="group">The group of the job</param>
        /// <param name="arguments">The arguments passed to the job</param>
        /// <param name="durable">Makes the job durable</param>
        /// <returns></returns>
        public static IJobDetail CreateJob<JobType>(this IScheduler scheduler, string identity, string group = null, object arguments = null, bool durable = false) where JobType : IJob
        {
            return scheduler.CreateJob(typeof(JobType), identity, group, arguments, durable);
        }

        /// <summary>
        /// Creates a new job.
        /// </summary>
        /// <param name="type">The type of the job</param>
        /// <param name="identity">Optional job identity</param>
        /// <param name="group">The group of the job</param>
        /// <param name="arguments">The arguments passed to the job</param>
        /// <param name="durable">Makes the job durable</param>
        /// <returns></returns>
        public static IJobDetail CreateJob(this IScheduler scheduler, Type type, string identity, string group = null, object arguments = null, bool durable = false)
        {
            /* Build the job instance */
            var job = JobBuilder.Create(type)
                .WithIdentity(identity ?? Guid.NewGuid().ToString(), group ?? DEFAULT_GROUP)
                .StoreDurably(durable)
                .Build();

            /* Add arguments to the job, if any */
            if (arguments != null)
            {
                job.JobDataMap[IsAnonymousType(arguments.GetType()) ? "dynamic" : arguments.GetType().Name] = arguments;
            }

            return job;
        }

        /// <summary>
        /// Gets all job keys
        /// </summary>
        /// <param name="scheduler"></param>
        /// <returns></returns>
        public static List<JobKey> GetAllJobsKeys(this IScheduler scheduler)
        {
            var keys = new List<JobKey>();
            var jobGroups = scheduler.GetJobGroupNames().GetAwaiter().GetResult();
            foreach (string group in jobGroups)
            {
                keys.AddRange(scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupContains(group)).GetAwaiter().GetResult().ToList());
            }

            return keys;
        }

        #region Helpers

        /// <summary>
        /// Checks whether a type is an AnonymousType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsAnonymousType(this Type type)
        {
            var hasCompilerGeneratedAttribute = type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Count() > 0;
            var nameContainsAnonymousType = type.FullName.Contains("AnonymousType");
            var isAnonymousType = hasCompilerGeneratedAttribute && nameContainsAnonymousType;
            return isAnonymousType;
        }

        #endregion

        #endregion
    }
}