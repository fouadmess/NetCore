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
    /// The DateTimeExtensionsController class
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Gets first date of month of this date
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(this DateTime dateTime) => new DateTime(dateTime.Year, dateTime.Month, 1);

        /// <summary>
        /// Returns the number of days in the specified month and year.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int DaysInMonth(this DateTime dateTime) => DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

        /// <summary>
        /// Gets last date of month of this date
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime LastDayOfMonth(this DateTime dateTime) => new DateTime(dateTime.Year, dateTime.Month, dateTime.DaysInMonth());
    }
}