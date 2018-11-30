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
    /// RequiredForRoleAttribute class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class RequiredForRoleAttribute : RoleBasedValidationAttribute
    {
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether other property's value should match or differ from provided other property's value (default is <c>false</c>).
        /// </summary>
        /// <value>
        ///   <c>true</c> if other property's value validation should be inverted; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// How this works
        /// - true: validated property is required when other property doesn't equal provided value
        /// - false: validated property is required when other property matches provided value
        /// </remarks>
        public bool IsInverted { get; set; } = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredForRoleAttribute"/> class.
        /// </summary>
        /// <param name="otherProperty">The roles to check.</param>
        public RequiredForRoleAttribute(string roles) : base(roles) { }

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
            this.MergeAttribute(context.Attributes, "data-val-required-for-role", GetErrorMessage(context));
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
            if (value == null && this.GetUserRoles(validationContext).Any(x => this.RoleList.Contains(x)))
            {
                if (this.IsInverted)
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult(this.GetErrorMessage(validationContext));
            }

            return ValidationResult.Success;
        }

        #endregion
    }
}