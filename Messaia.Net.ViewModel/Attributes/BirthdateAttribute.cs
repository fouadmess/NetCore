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

    /// <summary>
    /// BirthdayAttribute class
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class BirthdateAttribute : SelfContainedValidationAttribute
    {
        #region Properties

        /// <summary>
        /// Gets or sets the MinAge
        /// </summary>
        public int MinAge { get; set; } = 18;

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
            this.MergeAttribute(context.Attributes, "data-val-birthdate", GetErrorMessage(context));
            this.MergeAttribute(context.Attributes, "data-val-birthdate-minage", this.MinAge.ToString());
        }

        /// <summary>
        ///  Determines whether the specified value of the object is valid.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var birthday = (DateTime)value;
                if (birthday > DateTime.Today.AddYears(-this.MinAge))
                {
                    return new ValidationResult(this.GetErrorMessage(validationContext));
                }
            }
            catch (Exception)
            {
                return new ValidationResult(this.GetErrorMessage(validationContext));
            }

            return ValidationResult.Success;
        }

        #endregion
    }
}