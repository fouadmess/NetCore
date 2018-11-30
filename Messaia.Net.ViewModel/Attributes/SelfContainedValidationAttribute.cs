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
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// SelfContainedValidationAttribute class
    /// </summary>
    public abstract class SelfContainedValidationAttribute : ValidationAttribute, IClientModelValidator
    {
        /// <summary>
        /// Adds html attributes to the element to validate
        /// </summary>
        /// <param name="context"></param>
        public abstract void AddValidation(ClientModelValidationContext context);

        /// <summary>
        /// Gets the error message to display
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual string GetErrorMessage(ClientModelValidationContext context)
        {
            var services = context.ActionContext.HttpContext.RequestServices;
            var options = services.GetRequiredService<IOptions<MvcDataAnnotationsLocalizationOptions>>();
            var factory = services.GetService<IStringLocalizerFactory>();
            var modelType = context.ModelMetadata.ContainerType ?? context.ModelMetadata.ModelType;

            var stringLocalizer = GetStringLocalizer(options.Value, factory, modelType);
            var displayName = context.ModelMetadata.GetDisplayName();

            return GetErrorMessage(displayName, stringLocalizer);
        }

        /// <summary>
        /// Gets the error message to display
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected virtual string GetErrorMessage(ValidationContext validationContext)
        {
            var options = validationContext.GetRequiredService<IOptions<MvcDataAnnotationsLocalizationOptions>>();
            var factory = validationContext.GetService<IStringLocalizerFactory>();
            var modelType = validationContext.ObjectType;
            var stringLocalizer = GetStringLocalizer(options.Value, factory, modelType);

            return GetErrorMessage(validationContext.DisplayName, stringLocalizer);
        }

        /// <summary>
        /// Gets string localizer
        /// </summary>
        /// <param name="options"></param>
        /// <param name="factory"></param>
        /// <param name="modelType"></param>
        /// <returns></returns>
        private IStringLocalizer GetStringLocalizer(MvcDataAnnotationsLocalizationOptions options, IStringLocalizerFactory factory, Type modelType)
        {
            var provider = options.DataAnnotationLocalizerProvider;
            if (factory != null && provider != null)
            {
                return provider(modelType, factory);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the error message to display
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="stringLocalizer"></param>
        /// <returns></returns>
        protected virtual string GetErrorMessage(string displayName, IStringLocalizer stringLocalizer)
        {
            if (stringLocalizer != null && !string.IsNullOrEmpty(ErrorMessage) && string.IsNullOrEmpty(ErrorMessageResourceName) && ErrorMessageResourceType == null)
            {
                return stringLocalizer[ErrorMessage, displayName];
            }

            return FormatErrorMessage(displayName);
        }

        /// <summary>
        /// Merges html attributes
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        protected void MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (!attributes.ContainsKey(key))
            {
                attributes.Add(key, value);
            }
        }
    }
}