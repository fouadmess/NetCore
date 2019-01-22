///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 08:50:16
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Identity
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Messaia.Net.Data;

    /// <summary>
    /// IdentityDbContext class.
    /// </summary>
    public class IdentityDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IDbContext
    {
        #region Fields


        #endregion

        #region Properties


        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="IdentityDbContext"/>.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public IdentityDbContext(DbContextOptions options) : base(options)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityDbContext" /> class.
        /// </summary>
        protected IdentityDbContext()
        { }

        #endregion

        #region Methods

        /// <summary>
        /// Confuígures fluet API
        /// </summary>
        /// <param name="modelBuilder">The model builder object</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /* Rename identity tables */
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<UserToken>().ToTable("UserTokens");
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims");

            /* User */
            modelBuilder.Entity<User>(b =>
            {
                b.HasMany(r => r.Roles).WithOne(x => x.User).HasForeignKey(ur => ur.UserId).IsRequired();
                b.OwnsOne(x => x.Address).Ignore(x => x.Id).ToTable($"{nameof(User)}{nameof(User.Address)}es");
                b.HasIndex(x => x.ExternalId).IsUnique();
            });

            /* Role */
            modelBuilder.Entity<Role>(b =>
            {
                b.HasMany(r => r.Users).WithOne(x => x.Role).HasForeignKey(ur => ur.RoleId).IsRequired();
                b.HasMany(r => r.Claims).WithOne().HasForeignKey(ur => ur.RoleId);
            });
        }

        #endregion
    }
}