﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarYearAccountingPeriodSystem.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <summary>
    /// A calendar year is 12 consecutive months beginning on January 1st and ending on December 31st.
    /// </summary>
    [Serializable]
    public class CalendarYearAccountingPeriodSystem : AccountingPeriodSystem
    {
        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"><paramref name="fiscalYear"/> is null.</exception>
        public override ReportingPeriod<CalendarDay> GetReportingPeriodForFiscalYear(
            FiscalYear fiscalYear)
        {
            if (fiscalYear == null)
            {
                throw new ArgumentNullException(nameof(fiscalYear));
            }

            var januaryFirst = new CalendarDay(fiscalYear.Year, MonthOfYear.January, DayOfMonth.One);
            var decemberThirtyFirst = new CalendarDay(fiscalYear.Year, MonthOfYear.December, DayOfMonth.ThirtyOne);
            var result = new ReportingPeriod<CalendarDay>(januaryFirst, decemberThirtyFirst);
            return result;
        }
    }
}
