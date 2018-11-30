///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 11:06:58
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Microsoft.EntityFrameworkCore
{
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Messaia.Net.Common;

    /// <summary>
    /// DbContextExtensions class.
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// Adds or update entity
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="entity"></param>
        public static void AddOrUpdate(this DbContext dbContext, object entity)
        {
            var entry = dbContext.Entry(entity);
            switch (entry.State)
            {
                case EntityState.Detached:
                    dbContext.Add(entity);
                    break;

                case EntityState.Modified:
                    dbContext.Update(entity);
                    break;

                case EntityState.Added:
                    dbContext.Add(entity);
                    break;

                case EntityState.Unchanged:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Gets changed values after an update operation
        /// </summary>
        /// <param name="dbContext">The db context instance</param>
        /// <param name="trackedTypes">The types of properties to track</param>
        /// <returns></returns>
        public static List<Variance> GetChangedValues(this DbContext dbContext, params Type[] trackedTypes)
        {
            /* The list of changed properties */
            var changedProperties = new List<Variance>();

            /* Track changed properties */
            dbContext.ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified)
                .ToList()
                .ForEach(entry =>
                {
                    try
                    {
                        changedProperties.AddRange(entry.Entity.GetChangedProperties(entry.GetDatabaseValues().ToObject(), trackedTypes));
                    }
                    catch (Exception) { }
                });

            return changedProperties;
        }

        /// <summary>
        /// Gets changed values after an update operation
        /// </summary>
        /// <param name="dbContext">The db context instance</param>
        /// <param name="trackedTypes">The types of properties to track</param>
        /// <returns></returns>
        public static List<Variance> GetChangedValues<TEntity>(this DbContext dbContext, params Type[] trackedTypes) where TEntity : class
        {
            /* The list of changed properties */
            var changedProperties = new List<Variance>();

            /* Track changed properties */
            dbContext.GetModfiedEntries<TEntity>().ForEach(entry =>
            {
                try
                {
                    changedProperties.AddRange(entry.Item1.GetChangedProperties(entry.Item2.GetDatabaseValues().ToObject(), trackedTypes));
                }
                catch (Exception) { }
            });

            return changedProperties;
        }

        /// <summary>
        /// Updates many to many navigations
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="db"></param>
        /// <param name="currentItems"></param>
        /// <param name="newItems"></param>
        /// <param name="getKey"></param>
        public static void TryUpdateManyToMany<T, TKey>(this DbContext db, IEnumerable<T> currentItems, IEnumerable<T> newItems, Func<T, TKey> getKey) where T : class
        {
            db.Set<T>().RemoveRange(currentItems.Except(newItems, getKey));
            db.Set<T>().AddRange(newItems.Except(currentItems, getKey));
        }

        /// <summary>
        /// Gets the differece between two collections
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="items"></param>
        /// <param name="other"></param>
        /// <param name="getKeyFunc"></param>
        /// <returns></returns>
        public static IEnumerable<T> Except<T, TKey>(this IEnumerable<T> items, IEnumerable<T> other, Func<T, TKey> getKeyFunc)
        {
            return items
                .GroupJoin(other, getKeyFunc, getKeyFunc, (item, tempItems) => new { item, tempItems })
                .SelectMany(t => t.tempItems.DefaultIfEmpty(), (t, temp) => new { t, temp })
                .Where(t => ReferenceEquals(null, t.temp) || t.temp.Equals(default(T)))
                .Select(t => t.t.item);
        }

        /// <summary>
        /// Queries the database for copies of the values of the tracked entity as they currently exist in the database. 
        /// If the entity is not found in the database, then null is returned.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="entity"></param>
        /// <param name="loadCollections"></param>
        /// <returns></returns>
        public static TEntity GetOriginalEntity<TEntity>(this DbContext dbContext, TEntity entity, bool loadCollections = true) where TEntity : class
        {
            /* Get entity entry */
            var entry = dbContext.Entry(entity);
            if (entry == null)
            {
                throw new ArgumentException($"Entity of type {typeof(TEntity)} not found!");
            }

            /* Load original values from database */
            var originalEntity = entry.GetDatabaseValues().Clone().ToObject() as TEntity;
            if (loadCollections && originalEntity != null)
            {
                /* Get navigations of type ICollection */
                foreach (var collection in entry.Collections)
                {
                    /* Get the type of the collection member */
                    var collectionMemberType = collection.Metadata.ClrType.GetGenericArguments()?.FirstOrDefault();
                    if (collectionMemberType == null)
                    {
                        continue;
                    }

                    /* Continue, if the collection is not a map entity */
                    if (dbContext.Model.FindEntityType(collectionMemberType)?.FindPrimaryKey()?.Properties?.Count < 2)
                    {
                        continue;
                    }

                    /* Remember the original collection values */
                    var listType = typeof(List<>).MakeGenericType(collectionMemberType);

                    /* Get old collection values */
                    var oldValues = entry.Collection(collection.Metadata.Name).CurrentValue;

                    /* Cast IEnumerable to List<T> */
                    var oldValuesList = (oldValues != null ? Activator.CreateInstance(listType, oldValues) : Activator.CreateInstance(listType)) as IList;

                    /* Load collection data from database */
                    collection.Load();

                    /* Get collection property */
                    var collectionProperty = originalEntity.GetType().GetProperty(collection.Metadata.Name);

                    /* And set the value */
                    collectionProperty.SetValue(originalEntity, collection.CurrentValue);

                    /* Restore the original values into the entity */
                    entry.Collection(collection.Metadata.Name).CurrentValue = oldValuesList;
                }
            }

            return originalEntity;
        }

        /// <summary>
        /// Gets added entities
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static List<TEntity> GetAddedEntities<TEntity>(this DbContext dbContext)
            where TEntity : class
            => dbContext.GetTrackedEntities<TEntity>(EntityState.Added);

        /// <summary>
        /// Gets modified entities
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static List<TEntity> GetModfiedEntities<TEntity>(this DbContext dbContext)
            where TEntity : class
           => dbContext.GetTrackedEntities<TEntity>(EntityState.Modified);

        /// <summary>
        /// Gets deleted entities
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static List<TEntity> GetDeletedEntities<TEntity>(this DbContext dbContext)
            where TEntity : class
            => dbContext.GetTrackedEntities<TEntity>(EntityState.Deleted);

        /// <summary>
        /// Gets tracked entities
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static List<TEntity> GetTrackedEntities<TEntity>(this DbContext dbContext, EntityState states)
            where TEntity : class
            => dbContext.ChangeTracker.Entries<TEntity>()
                .Where(p => p.State == states)
                .Select(p => p.Entity)
                .ToList();

        /// <summary>
        /// Gets added entries
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static List<Tuple<TEntity, EntityEntry>> GetAddedEntries<TEntity>(this DbContext dbContext)
            where TEntity : class
            => dbContext.GetTrackedEntries<TEntity>(EntityState.Added);

        /// <summary>
        /// Gets modified entries
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static List<Tuple<TEntity, EntityEntry>> GetModfiedEntries<TEntity>(this DbContext dbContext)
            where TEntity : class
           => dbContext.GetTrackedEntries<TEntity>(EntityState.Modified);

        /// <summary>
        /// Gets deleted entries
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static List<Tuple<TEntity, EntityEntry>> GetDeletedEntries<TEntity>(this DbContext dbContext)
            where TEntity : class
            => dbContext.GetTrackedEntries<TEntity>(EntityState.Deleted);

        /// <summary>
        /// Gets tracked entries
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static List<Tuple<TEntity, EntityEntry>> GetTrackedEntries<TEntity>(this DbContext dbContext, EntityState states)
            where TEntity : class
            => dbContext.ChangeTracker.Entries<TEntity>()
                .Where(p => p.State == states)
                .Select(p => new Tuple<TEntity, EntityEntry>(p.Entity, p))
                .ToList();
    }
}