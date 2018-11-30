///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           22.11.2016 02:50:28
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace WebApp
{
    using Microsoft.AspNetCore.Hosting;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Messaia.Net.Identity;
    using Messaia.Net.Model;

    /// <summary>
    /// The DatabaseInitializer class
    /// </summary>
    public class DatabaseInitializer
    {
        #region Properties

        /// <summary>
        /// Gets or sets the UserManager
        /// </summary>
        public UserManager UserManager { get; private set; }

        /// <summary>
        /// Gets or sets the RoleManager
        /// </summary>
        public RoleManager RoleManager { get; private set; }

        /// <summary>
        /// Gets or sets the DbContext
        /// </summary>
        public IdentityDbContext DbContext { get; private set; }

        /// <summary>
        /// Gets or sets the hostingEnvironment
        /// </summary>
        public IHostingEnvironment HostingEnvironment { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="UserController"/> class.
        /// </summary>
        public DatabaseInitializer(
            UserManager userManager,
            RoleManager roleManager,
            IdentityDbContext dbContext,
            IHostingEnvironment HostingEnvironment
        )
        {
            this.UserManager = userManager;
            this.RoleManager = roleManager;
            this.DbContext = dbContext;
            this.HostingEnvironment = HostingEnvironment;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initilizes the database.
        /// </summary>
        /// <returns></returns>
        public bool Init(bool deleteDatabase = false)
        {
            var database = this.DbContext.Database;
            if (database != null)
            {
                if (deleteDatabase)
                {
                    database.EnsureDeleted();
                }

                /* Create database in */
                if (database.EnsureCreated())
                {
                    this.InitIdentityTablesAsync().GetAwaiter().GetResult();
                    this.AddProcedures();
                    this.AddFunctions();
                    this.AddConstraints();

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Initialize the identity tables
        /// </summary>
        /// <returns></returns>
        private async Task InitIdentityTablesAsync()
        {
            #region Add Roles

            await this.RoleManager.CreateAsync(new Role { Name = "Administrator", Title = "Administrator", Locked = true });
            await this.RoleManager.CreateAsync(new Role { Name = "CEO", Title = "CEO" });
            await this.RoleManager.CreateAsync(new Role { Name = "DataTypist", Title = "Data Typist" });

            #endregion

            #region Add Claims to Roles

            this.AddClaimsToRole("Administrator", "permissions", new string[]
            {
                "Admin"
            });

            this.AddClaimsToRole("CEO", "permissions", new string[]
            {
                /* User */
                "CreateUser",
                "ReadUser",
                "UpdateUser",
                "DeleteUser",
                /* Role */
                "CreateRole",
                "ReadRole",
                "UpdateRole",
                "DeleteRole",
            });

            this.AddClaimsToRole("DataTypist", "permissions", new string[]
            {
                /* User */
                "CreateUser",
                "Owner"
            });

            #endregion

            #region Add Admins

            /* Admin */
            var adminEmail = "f.messaia@veritas-data.de";
            this.UserManager?.CreateAsync(new User
            {
                Email = adminEmail,
                UserName = adminEmail,
                EmailConfirmed = true,
                Enabled = true,
                Salutation = Salutation.Male,
                FirstName = "Fouad",
                LastName = "Messaia",
                PhoneNumber = "017667521307",
                Address = new Address
                {
                    Address1 = "Landwehrstr.",
                    Address2 = "45",
                    PostalCode = "64293",
                    City = "Darmstadt"
                }
            }, "P@ssw0rd").GetAwaiter().GetResult();

            var admin2Email = "info@veritas-data.de";
            this.UserManager?.CreateAsync(new User
            {
                Email = admin2Email,
                UserName = admin2Email,
                EmailConfirmed = true,
                Enabled = true,
                Salutation = Salutation.Male,
                FirstName = "Veritas",
                LastName = "Admin",
                PhoneNumber = "0123456789012",
                Address = new Address
                {
                    Address1 = "Bunsenstr.",
                    Address2 = "2a",
                    PostalCode = "64293",
                    City = "Darmstadt"
                }
            }, "P@ssw0rd").GetAwaiter().GetResult();

            var dataTypistEmail = "datatypist@example.com";
            this.UserManager?.CreateAsync(new User
            {
                Email = dataTypistEmail,
                UserName = dataTypistEmail,
                EmailConfirmed = true,
                Enabled = true,
                Salutation = Salutation.Male,
                FirstName = "Data",
                LastName = "Typist",
                PhoneNumber = "0123456789012",
                Address = new Address
                {
                    Address1 = "Bunsenstr.",
                    Address2 = "2a",
                    PostalCode = "64293",
                    City = "Darmstadt"
                }
            }, "P@ssw0rd").GetAwaiter().GetResult();

            #endregion

            #region Add Users to Roles

            this.AddUserToRoles(adminEmail, "Administrator");
            this.AddUserToRoles(admin2Email, "CEO");
            this.AddUserToRoles(dataTypistEmail, "DataTypist");

            #endregion
        }

        /// <summary>
        /// Add procedures to the database.
        /// </summary>
        private void AddProcedures()
        {

        }

        /// <summary>
        /// Adds custom functions to the database server
        /// </summary>
        private void AddFunctions()
        {

        }

        /// <summary>
        /// Add constraints to the database.
        /// </summary>
        private void AddConstraints()
        {

        }

        #region Helpers

        /// <summary>
        /// Adds claims to a role
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="claimType"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        public void AddClaimsToRole(string roleName, string claimType, params string[] claims)
        {
            var role = this.RoleManager?.FindByNameAsync(roleName).GetAwaiter().GetResult();
            if (role == null)
            {
                throw new Exception("Role not found");
            }

            foreach (var claim in claims)
            {
                this.RoleManager.AddClaimAsync(role, new Claim(claimType, claim)).GetAwaiter().GetResult();
            }
        }

        /// <summary>
        /// Adds roles to groups
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public void AddUserToRoles(string username, params string[] roles)
        {
            var user = this.UserManager?.FindByEmailAsync(username).GetAwaiter().GetResult();
            if (user == null)
            {
                throw new Exception("User not found");
            }

            this.UserManager.AddToRolesAsync(user, roles).GetAwaiter().GetResult();
        }

        #endregion

        #endregion
    }
}