///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Data
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A DbContext instance represents a combination of the Unit 
    /// Of Work and Repository patterns such that it can be used 
    /// to query from a database and group together changes that 
    /// will then be written back to the store as a unit. 
    /// DbContext is conceptually similar to ObjectContext.
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// Saves all changes made in this context to the 
        /// underlying database.
        /// </summary>
        /// <returns>Type: System.Int32</returns>
        int SaveChanges();

        /// <summary>
        /// Asynchronously saves all changes made in this context 
        /// to the underlying database.
        /// </summary>
        /// <param name="cancellationToken">
        ///     Type: System.Threading.CancellationToken
        ///     A CancellationToken to observe while waiting 
        ///     for the task to complete.
        /// </param>
        /// <returns>
        ///     Type: System.Threading.Tasks.Task<Int32>
        ///     A task that represents the asynchronous save operation. 
        ///     The task result contains the number of objects written 
        ///     to the underlying database.
        /// </returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}