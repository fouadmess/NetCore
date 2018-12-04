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
    using System.Net.Http;
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
                var client = new HttpClient();

                /* Discover the identity server */
                var discovery = await client.GetDiscoveryDocumentAsync(authentication.AuthUrl);
                if (discovery.IsError)
                {
                    throw new AuthenticationException(discovery.Error);
                }

                /* Requesting a token using the password Grant Type */
                var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = discovery.TokenEndpoint,
                    GrantType = authentication.GrantType,
                    ClientId = authentication.AuthClient,
                    ClientSecret = authentication.AuthSecret,
                    Scope = authentication.ApiScope,
                    UserName = authentication.UserName,
                    Password = authentication.Password
                });

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
                var client = new HttpClient();

                /* Send an OAuth token introspection request */
                var response = await client.IntrospectTokenAsync(new TokenIntrospectionRequest
                {
                    Address = new Uri(new Uri(authentication.AuthUrl), "connect/introspect").ToString(),
                    ClientId = authentication.AuthClient,
                    ClientSecret = authentication.AuthSecret,
                    Token = accessToken
                });

                if (response.IsError)
                {
                    throw new Exception(response.Error);
                }

                /* Check the access token */
                return !response.IsError && response.IsActive;
            }
            catch (Exception ex)
            {
                throw new AuthenticationException(ex.Message, ex.InnerException);
            }
        }

        #endregion
    }
}