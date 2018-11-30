///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.Security
{
    /// <summary>
    /// GlobalPermissionClaims struct
    /// </summary>
    public struct GlobalPermissionClaims
    {
        public const string Admin = nameof(Admin);
        public const string Create = nameof(Create);
        public const string Read = nameof(Read);
        public const string Update = nameof(Update);
        public const string Delete = nameof(Delete);
        public const string Owner = nameof(Owner);
        public const string Manage = nameof(Manage);
        public const string Count = nameof(Count);
    }
}