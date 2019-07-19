///-----------------------------------------------------------------
///   Author:         Messaia
///   AuthorUrl:      http://messaia.com
///   Date:           04.01.2019 17:19:43
///   Copyright (©)   2019, VERITAS DATA GmbH, all Rights Reserved. 
///                   No part of this document may be reproduced 
///                   without VERITAS DATA GmbH's express consent. 
///-----------------------------------------------------------------
namespace System
{
    /// <summary>
    /// DecimalExtensions class.
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        /// Formats a decimal to a price string
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static string FormatPrice(this decimal price)
        {
            return string.Format("{0:0},<sup>{1:00}</sup>", price.IntPart(), price.FractionalPart());
        }

        /// <summary>
        /// Gets the integer part of a decimal
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int IntPart(this decimal number)
        {
            return (int)number;
        }

        /// <summary>
        /// Gets the fractional part of a decimal
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int FractionalPart(this decimal number)
        {
            return (int)((number - (int)number) * 100);
        }
    }
}