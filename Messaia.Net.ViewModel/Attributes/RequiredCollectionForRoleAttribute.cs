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
    using Microsoft.Extensions.Localization;
    using System;
    using System.Collections;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// RequiredCollectionForRoleAttribute class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class RequiredCollectionForRoleAttribute : RoleBasedValidationAttribute
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Count.
        /// </summary>
        /// <value>
        /// Gets or sets the other name.
        /// </value>
        public int Count { get; private set; } = 1;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes an instance of the <see cref="RequiredCollectionForRoleAttribute"/> class.
        /// </summary>
        public RequiredCollectionForRoleAttribute(string roles) : this(roles, 1) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredCollectionForRoleAttribute"/> class.
        /// </summary>
        /// <param name="count">The count required.</param>
        public RequiredCollectionForRoleAttribute(string roles, int count) : base(roles)
        {
            this.Count = count;
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

            this.MergeAttribute(context.Attributes, "data-val", "false");
            this.MergeAttribute(context.Attributes, "data-val-required-count", GetErrorMessage(context));
        }

        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }

            /* Check if the annotated property is of type ICollection */
            var propertyType = validationContext.ObjectType.GetProperty(validationContext.MemberName).PropertyType;
            if (propertyType.GetInterface(nameof(IEnumerable)) == null)
            {
                throw new ArgumentException("The annotated proerty is not of type ICollection!");
            }

            /* Check if the value is a collection and has the required length */
            if (
                this.GetUserRoles(validationContext).Any(x => this.RoleList.Contains(x)) &&
                (!(value is IEnumerable collection) || ((ICollection)collection).Count < this.Count)
            )
            {
                return new ValidationResult(this.GetErrorMessage(validationContext));
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Gets the error message to display
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="stringLocalizer"></param>
        /// <returns></returns>
        protected override string GetErrorMessage(string displayName, IStringLocalizer stringLocalizer)
        {
            if (stringLocalizer != null && !string.IsNullOrEmpty(ErrorMessage) && string.IsNullOrEmpty(ErrorMessageResourceName) && ErrorMessageResourceType == null)
            {
                return stringLocalizer[ErrorMessage, displayName, this.Count];
            }

            return FormatErrorMessage(displayName);
        }

        #endregion
    }
}