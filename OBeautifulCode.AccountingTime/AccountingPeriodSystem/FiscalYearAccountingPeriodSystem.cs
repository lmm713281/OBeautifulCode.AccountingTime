﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalYearAccountingPeriodSystem.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <summary>
    /// A fiscal year is 12 consecutive months ending on the last day of any month except December 31st.
    /// </summary>
    [Serializable]
    public class FiscalYearAccountingPeriodSystem : AccountingPeriodSystem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FiscalYearAccountingPeriodSystem"/> class.
        /// </summary>
        /// <param name="lastMonthInFiscalYear">The last month of the fiscal year.</param>
        /// <exception cref="ArgumentException"><paramref name="lastMonthInFiscalYear"/> is invalid.</exception>
        /// <exception cref="ArgumentException"><paramref name="lastMonthInFiscalYear"/> is <see cref="MonthOfYear.December"/>.</exception>
        public FiscalYearAccountingPeriodSystem(
            MonthOfYear lastMonthInFiscalYear)
        {
            if (lastMonthInFiscalYear == MonthOfYear.Invalid)
            {
                throw new ArgumentException("last month in fiscal year is invalid", nameof(lastMonthInFiscalYear));
            }

            if (lastMonthInFiscalYear == MonthOfYear.December)
            {
                throw new ArgumentException("last month in fiscal year is December", nameof(lastMonthInFiscalYear));
            }

            this.LastMonthInFiscalYear = lastMonthInFiscalYear;
        }

        /// <summary>
        /// Gets the last month of the fiscal year.
        /// </summary>
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public MonthOfYear LastMonthInFiscalYear { get; private set; }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"><paramref name="fiscalYear"/> is null.</exception>
        public override ReportingPeriod<CalendarDay> GetReportingPeriodForFiscalYear(
            FiscalYear fiscalYear)
        {
            if (fiscalYear == null)
            {
                throw new ArgumentNullException(nameof(fiscalYear));
            }

            var lastDayInEndingMonth = DateTime.DaysInMonth(fiscalYear.Year, (int)this.LastMonthInFiscalYear);
            var lastDayInFiscalYear = new DateTime(fiscalYear.Year, (int)this.LastMonthInFiscalYear, lastDayInEndingMonth);
            var firstDayInFiscalYear = lastDayInFiscalYear.AddDays(1).AddYears(-1);
            var result = new ReportingPeriod<CalendarDay>(firstDayInFiscalYear.ToCalendarDay(), lastDayInFiscalYear.ToCalendarDay());
            return result;
        }
    }
}
