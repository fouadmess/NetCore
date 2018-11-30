///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016 20:22:35
///   Copyright (©)   2018, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Http.Util
{
    using IdentityModel.Client;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// HttpHelper class.
    /// </summary>
    public static class HttpHelper
    {
        #region Fields


        #endregion

        #region Properties


        #endregion

        #region Methods

        /// <summary>
        /// Performs authentication
        /// </summary>
        /// <param name="authentication"></param>
        /// <returns>The access token</returns>
        public static async Task<string> AuthenticateAsync(Authentication authentication)
        {
            if (authentication == null)
            {
                throw new ArgumentNullException(nameof(authentication));
            }

            try
            {
                /* Discover the identity server */
                var discovery = await DiscoveryClient.GetAsync(authentication.AuthUrl);
                if (discovery.IsError)
                {
                    throw new AuthenticationException(discovery.Error);
                }

                /* Requests a token using the resource owner password credentials */
                var tokenClient = new TokenClient(discovery.TokenEndpoint, authentication.AuthClient, authentication.AuthSecret);
                var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(
                    authentication.UserName,
                    authentication.Password,
                    authentication.ApiScope
                );

                if (tokenResponse.IsError)
                {
                    throw new AuthenticationException(tokenResponse.Error);
                }

                return tokenResponse.AccessToken;
            }
            catch (Exception ex)
            {
                throw new AuthenticationException(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Validates an access token
        /// </summary>
        /// <param name="authentication"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static async Task<bool> ValidateAccessTokenAsync(Authentication authentication, string accessToken)
        {
            if (authentication == null)
            {
                throw new ArgumentNullException(nameof(authentication));
            }

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return false;
            }

            try
            {
                /* Initializes a new instance of IntrospectionClient */
                var introspectionClient = new IntrospectionClient(
                    new Uri(new Uri(authentication.AuthUrl), "connect/introspect").ToString(),
                    authentication.ApiScope,
                    authentication.AuthSecret
                );

                /* Check the access token */
                var result = await introspectionClient.SendAsync(new IntrospectionRequest { Token = accessToken });

                return !result.IsError && result.IsActive;
            }
            catch (Exception ex)
            {
                throw new AuthenticationException(ex.Message, ex.InnerException);
            }
        }

        #endregion
    }
}