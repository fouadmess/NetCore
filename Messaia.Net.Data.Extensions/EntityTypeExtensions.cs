///-----------------------------------------------------------------
///   Author:         fouad
///   AuthorUrl:      http://veritas-data.de
///   Date:           06.09.2019 07:13:48
///   Copyright (©)   2019, VERITAS DATA GmbH, all Rights Reserved. 
///                   No part of this document may be reproduced 
///                   without VERITAS DATA GmbH's express consent. 
///-----------------------------------------------------------------
namespace Microsoft.EntityFrameworkCore.Metadata
{
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    /// <summary>
    /// EntityTypeExtensions class.
    /// </summary>
    public static class EntityTypeExtensions
    {
        /// <summary>
        /// Gets the names of the defining navigations recursivly.
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static string GetDefiningNavigationName(this IEntityType entityType)
        {
            if (entityType == null)
            {
                return string.Empty;
            }

            if (entityType.DefiningEntityType != null)
            {
                return $"{entityType.DefiningEntityType.GetDefiningNavigationName()}.{entityType.DefiningNavigationName}";
            }

            return entityType?.DisplayName();
        }
    }
}