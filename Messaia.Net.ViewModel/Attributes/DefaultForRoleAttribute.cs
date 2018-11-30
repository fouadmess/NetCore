///-----------------------------------------------------------------
///   Author:         Fouad Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           01.01.2016
///   Copyright (©)   2016, MESSAIA.NET, all Rights Reserved. 
///                   Licensed under the Apache License, Version 2.0. 
///                   See License.txt in the project root for license information.
///-----------------------------------------------------------------
namespace Messaia.Net.ViewModel
{
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// DefaultForRoleAttribute class
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DefaultForRoleAttribute : RoleBasedValidationAttribute
    {
        #region Properties

        /// <summary>
        /// Gets or sets the DefaultValue
        /// </summary>
        public object DefaultValue { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultForRoleAttribute"/> class.
        /// </summary>
        /// <param name="otherProperty">The roles to check.</param>
        public DefaultForRoleAttribute(string roles, object defaultValue) : base(roles)
        {
            this.DefaultValue = defaultValue;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds html attributes to the element to validate
        /// </summary>
        /// <param name="context"></param>
        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this.MergeAttribute(context.Attributes, "data-val", "true");
            this.MergeAttribute(context.Attributes, "data-val-default-for-role", GetErrorMessage(context));
        }

        /// <summary>
        ///  Determines whether the specified value of the object is valid.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }

            /* Check roles */
            if (this.GetUserRoles(validationContext).Any(x => this.RoleList.Contains(x)))
            {
                validationContext.ObjectType
                    .GetProperty(validationContext.MemberName)
                    .SetValue(validationContext.ObjectInstance, this.DefaultValue, null);
            }

            return ValidationResult.Success;
        }


        #endregion
    }
}