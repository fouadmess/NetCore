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
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// IbanAttribute class
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class IbanAttribute : SelfContainedValidationAttribute
    {
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
            this.MergeAttribute(context.Attributes, "data-val-iban", GetErrorMessage(context));
        }

        /// <summary>
        ///  Determines whether the specified value of the object is valid.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            /* Cast object to string */
            var iban = value as string;
            if (string.IsNullOrWhiteSpace(iban))
            {
                return ValidationResult.Success;
            }

            try
            {
                /* Sanitize... */
                iban = Regex.Replace(iban.ToUpper().Trim() ?? string.Empty, @"\s+", "");
                if (Regex.IsMatch(iban, "^[A-Z0-9]"))
                {
                    var bank = iban.Substring(4, iban.Length - 4) + iban.Substring(0, 4);
                    var stringBuilder = new StringBuilder();

                    foreach (char c in bank)
                    {
                        stringBuilder.Append(Char.IsLetter(c) ? c - 55 : int.Parse(c.ToString()));
                    }

                    /* Calculate the checksum */
                    string checkSumString = stringBuilder.ToString();
                    int checksum = int.Parse(checkSumString.Substring(0, 1));
                    for (int i = 1; i < checkSumString.Length; i++)
                    {
                        checksum *= 10;
                        checksum += int.Parse(checkSumString.Substring(i, 1));
                        checksum %= 97;
                    }

                    /* Check the checksum */
                    if (checksum != 1)
                    {
                        return new ValidationResult(this.GetErrorMessage(validationContext));
                    }
                }
            }
            catch (Exception) { }

            return ValidationResult.Success;
        }

        #endregion
    }
}