///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 07:25:41
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Repository.Impl
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the default implementation of the <see cref="IUnitOfWork"/> interface.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the db context.</typeparam>
    public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {
        #region Properties

        /// <summary>
        /// Gets the db context.
        /// </summary>
        /// <returns>The instance of type <typeparamref name="TDbContext"/>.</returns>
        public TDbContext DbContext { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The db context</param>
        public UnitOfWork(TDbContext context)
        {
            this.DbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <param name="cancellationToken">
        ///     Type: System.Threading.CancellationToken
        ///     A CancellationToken to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     Type: System.Threading.Tasks.Task<Int32>
        ///     A task that represents the asynchronous save operation. The task result contains the number of objects written to the underlying database.
        /// </returns>
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
            => this.DbContext.SaveChangesAsync(cancellationToken);

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>Type: System.Int32</returns>
        public int SaveChanges()
            => this.DbContext.SaveChanges();

        /// <summary>
        /// Executes the specified raw SQL command.
        /// </summary>
        /// <param name="sql">The raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The number of state entities written to database.</returns>
        public int ExecuteSqlCommand(string sql, params object[] parameters)
            => this.DbContext.Database.ExecuteSqlCommand(sql, parameters);

        /// <summary>
        /// Executes the specified raw SQL command asyncly.
        /// </summary>
        /// <param name="sql">The raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The number of state entities written to database.</returns>
        public async Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
            => await this.DbContext.Database.ExecuteSqlCommandAsync(sql, parameters);

        /// <summary>
        /// Uses raw SQL queries to fetch the specified <typeparamref name="TEntity" /> data.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="sql">The raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>An <see cref="IQueryable{T}" /> that contains elements that satisfy the condition specified by raw SQL.</returns>
        public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class
            => this.DbContext.Set<TEntity>().FromSql(sql, parameters);

        #endregion
    }
}