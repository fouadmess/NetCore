///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 09:39:32
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Identity
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using System.Security.Claims;

    /// <summary>
    /// UserStore class.
    /// </summary>
    public class UserStore : UserStore<User, Role, IdentityDbContext, int, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>
    {
        #region Fields


        #endregion

        #region Properties


        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="UserStore"/> class.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="describer"></param>
        public UserStore(IdentityDbContext context, IdentityErrorDescriber describer = null) : base(context, describer) { }

        #endregion

        #region Methods

        /// <summary>
        /// Called to create a new instance of a <see cref="IdentityUserRole{int}"/>.
        /// </summary>
        /// <param name="user">The associated user.</param>
        /// <param name="role">The associated role.</param>
        /// <returns></returns>
        protected override UserRole CreateUserRole(User user, Role role)
        {
            return new UserRole()
            {
                UserId = user.Id,
                RoleId = role.Id
            };
        }

        /// <summary>
        /// Called to create a new instance of a <see cref="IdentityUserClaim{int}"/>.
        /// </summary>
        /// <param name="user">The associated user.</param>
        /// <param name="claim">The associated claim.</param>
        /// <returns></returns>
        protected override UserClaim CreateUserClaim(User user, Claim claim)
        {
            var userClaim = new UserClaim { UserId = user.Id };
            userClaim.InitializeFromClaim(claim);
            return userClaim;
        }

        /// <summary>
        /// Called to create a new instance of a <see cref="IdentityUserLogin{int}"/>.
        /// </summary>
        /// <param name="user">The associated user.</param>
        /// <param name="login">The sasociated login.</param>
        /// <returns></returns>
        protected override UserLogin CreateUserLogin(User user, UserLoginInfo login)
        {
            return new UserLogin
            {
                UserId = user.Id,
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider,
                ProviderDisplayName = login.ProviderDisplayName
            };
        }

        /// <summary>
        /// Called to create a new instance of a <see cref="IdentityUserToken{int}"/>.
        /// </summary>
        /// <param name="user">The associated user.</param>
        /// <param name="loginProvider">The associated login provider.</param>
        /// <param name="name">The name of the user token.</param>
        /// <param name="value">The value of the user token.</param>
        /// <returns></returns>
        protected override UserToken CreateUserToken(User user, string loginProvider, string name, string value)
        {
            return new UserToken
            {
                UserId = user.Id,
                LoginProvider = loginProvider,
                Name = name,
                Value = value
            };
        }

        #endregion
    }
}