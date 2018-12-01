///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           27.01.2018 18:44:59
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Test
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Messaia.Net.Data;
    using Messaia.Net.Identity;

    /// <summary>
    /// ShopDbContext class.
    /// </summary>
    public class ShopDbContext : IdentityDbContext, IDbContext
    {
        #region Fields


        #endregion

        #region Properties

        ///// <summary>
        ///// Gets or sets the Products
        ///// </summary>
        //public DbSet<Product> Products { get; set; }

        ///// <summary>
        ///// Gets or sets the Carts
        ///// </summary>
        //public DbSet<Cart> Carts { get; set; }

        ///// <summary>
        ///// Gets or sets the CartItems
        ///// </summary>
        //public DbSet<CartItem> CartItems { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="GlassFiberDbContext"/> class.
        /// </summary>
        /// <param name="sessionContext"></param>
        /// <param name="options"></param>
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

        #endregion

        #region Methods

        /// <summary>
        /// Confuígures fluet API
        /// </summary>
        /// <param name="modelBuilder">The model builder object</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(e => typeof(IProduct).IsAssignableFrom(e.ClrType)))
            //{
            //    modelBuilder.Entity(entityType.ClrType);

            //    //modelBuilder.Entity(entityType.ClrType).Property<DateTime>("CreatedAt");

            //    //modelBuilder.Entity(entityType.ClrType).Property<DateTime>("UpdatedAt");

            //    //modelBuilder.Entity(entityType.ClrType).Property<string>("CreatedBy");

            //    //modelBuilder.Entity(entityType.ClrType).Property<string>("UpdatedBy");
            //}

            //modelBuilder.Type<IProduct>(typeof(Product)).ForEach(b =>
            //modelBuilder.Entity<Product>(b =>
            //{
            //    b.HasKey(x => x.Id);
            //    b.HasOne(x => (User)x.CreatedByUser).WithMany().HasForeignKey(x => x.CreatedByUserId).IsRequired(false);
            //    b.HasOne(x => (User)x.UpdatedByUser).WithMany().HasForeignKey(x => x.UpdatedByUserId).IsRequired(false);
            //    b.Property(p => p.ConcurrencyStamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
            //});

            //modelBuilder.Entity<Cart>(b =>
            //{
            //    b.HasKey(x => x.Id);
            //    b.HasOne(x => (User)x.CreatedByUser).WithMany().HasForeignKey(x => x.CreatedByUserId).IsRequired(false);
            //    b.HasOne(x => (User)x.UpdatedByUser).WithMany().HasForeignKey(x => x.UpdatedByUserId).IsRequired(false);
            //    b.Property(p => p.ConcurrencyStamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
            //});

            //modelBuilder.Entity<CartItem>(b =>
            //{
            //    b.HasKey(x => x.Id);
            //    b.HasOne(x => (User)x.CreatedByUser).WithMany().HasForeignKey(x => x.CreatedByUserId).IsRequired(false);
            //    b.HasOne(x => (User)x.UpdatedByUser).WithMany().HasForeignKey(x => x.UpdatedByUserId).IsRequired(false);
            //    b.HasOne(x => (Cart)x.Cart).WithMany(x => (ICollection<CartItem>)x.Items).HasForeignKey(x => x.CartId).IsRequired(false);
            //    b.HasOne(x => (Product)x.Product).WithMany().HasForeignKey(x => x.ProductId).IsRequired(false);
            //    b.Property(p => p.ConcurrencyStamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
            //});
        }

        #endregion
    }

    public static class ModelBuilderExtension
    {
        public static List<EntityTypeBuilder<TEntity>> Type<TEntity>(this ModelBuilder modelBuilder, params Type[] types) where TEntity : class
        {
            var entityTypeBuilders = new List<EntityTypeBuilder<TEntity>>();

            if (types.Count() == 0)
                return entityTypeBuilders;

            var method = typeof(ModelBuilder).GetMethod("Entity", new Type[] { });

            foreach (var type in types)
            {
                var generic = method.MakeGenericMethod(type);

                // TODO: Can't cast
                var entityTypeBuilder = (EntityTypeBuilder<TEntity>)generic.Invoke(modelBuilder, null);

                entityTypeBuilders.Add(entityTypeBuilder);
            }

            return entityTypeBuilders;
        }
    }
}