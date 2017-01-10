﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodExtensions.Manipulation.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;
    using System.Collections.Generic;

    using static System.FormattableString;

    /// <summary>
    /// Extension methods to shape and manipulate a <see cref="ReportingPeriod{T}"/>.
    /// </summary>
    public static partial class ReportingPeriodExtensions
    {
        /// <summary>
        /// Clones a reporting period while adjusting the start or end of the reporting period, or both.
        /// </summary>
        /// <typeparam name="TReportingPeriod">The type of reporting period to return.</typeparam>
        /// <param name="reportingPeriod">The reporting period to clone.</param>
        /// <param name="component">The component(s) of the reporting period to adjust.</param>
        /// <param name="unitsToAdd">The number of units to add when adjusting the reporting period component.  Use negative numbers to subtract units.</param>
        /// <param name="granularityOfUnitsToAdd">The granularity of the units to add to the specified reporting period component(s).  Must be as or less granular than the reporting period component (e.g. can add CalendarYear to a CalendarQuarter, but not vice-versa).</param>
        /// <returns>A clone of the specified reporting period with the specified adjustment made to the start or end of the reporting period, or both.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="component"/> is <see cref="ReportingPeriodComponent.Invalid"/>.</exception>
        /// <exception cref="ArgumentException">Cannot add or subtract from a unit-of-time whose granularity is <see cref="UnitOfTimeGranularity.Unbounded"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="granularityOfUnitsToAdd"/> is <see cref="UnitOfTimeGranularity.Invalid"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="granularityOfUnitsToAdd"/> is <see cref="UnitOfTimeGranularity.Unbounded"/>.  Cannot add units of that granularity.</exception>
        /// <exception cref="ArgumentException"><paramref name="granularityOfUnitsToAdd"/> is more granular than the reporting period component.  Only units that are as granular or less granular than a unit-of-time can be added to that unit-of-time.</exception>
        /// <exception cref="InvalidOperationException">The adjustment has caused the <see cref="ReportingPeriod{T}.Start"/> to be greater than <see cref="ReportingPeriod{T}.End"/></exception>
        /// <exception cref="InvalidOperationException">The adjusted reporting period cannot be converted into a <typeparamref name="TReportingPeriod"/></exception>
        public static TReportingPeriod CloneWithAdjustment<TReportingPeriod>(this IReportingPeriod<UnitOfTime> reportingPeriod, ReportingPeriodComponent component, int unitsToAdd, UnitOfTimeGranularity granularityOfUnitsToAdd)
            where TReportingPeriod : class, IReportingPeriod<UnitOfTime>
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (component == ReportingPeriodComponent.Invalid)
            {
                throw new ArgumentException(Invariant($"{nameof(component)} is {nameof(ReportingPeriodComponent.Invalid)}"));
            }

            var start = reportingPeriod.Start;
            var end = reportingPeriod.End;
            if ((component == ReportingPeriodComponent.Start) || (component == ReportingPeriodComponent.Both))
            {
                start = start.Plus(unitsToAdd, granularityOfUnitsToAdd);
            }

            if ((component == ReportingPeriodComponent.End) || (component == ReportingPeriodComponent.Both))
            {
                end = end.Plus(unitsToAdd, granularityOfUnitsToAdd);
            }

            try
            {
                var result = new ReportingPeriod<UnitOfTime>(start, end).Clone<TReportingPeriod>();
                return result;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new InvalidOperationException("The adjustment has caused the Start of the reporting period to be greater than the End of the reporting period.", ex);
            }
        }

        /// <summary>
        /// Creates all permutations of reporting periods between the
        /// start and end of a specified reporting period, from 1 unit
        /// to the specified number of maximum number of units that a
        /// reporting period can contain.
        /// </summary>
        /// <typeparam name="T">The unit-of-time of the reporting period.</typeparam>
        /// <param name="reportingPeriod">The reporting period to permute.</param>
        /// <param name="maxUnitsInAnyReportingPeriod">Maximum number of units-of-time in each reporting period.</param>
        /// <returns>All possible reporting periods containing between 1 and <paramref name="maxUnitsInAnyReportingPeriod"/> units-of-time, contained within <paramref name="reportingPeriod"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod"/> <see cref="IReportingPeriod{T}.Start"/> and/or <see cref="IReportingPeriod{T}.End"/> is unbounded.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxUnitsInAnyReportingPeriod"/> is less than or equal to 0.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is a perfectly fine usage of nesting generic types.")]
        public static ICollection<IReportingPeriod<T>> CreatePermutations<T>(this IReportingPeriod<T> reportingPeriod, int maxUnitsInAnyReportingPeriod)
            where T : UnitOfTime
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (reportingPeriod.HasComponentWithUnboundedGranularity())
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod)} {nameof(reportingPeriod.Start)} and/or {nameof(reportingPeriod.End)} is unbounded"));
            }

            if (maxUnitsInAnyReportingPeriod < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(maxUnitsInAnyReportingPeriod), "max units in any reporting period is <= 0");
            }

            var allUnits = reportingPeriod.GetUnitsWithin();
            var allReportingPeriods = new List<IReportingPeriod<T>>();
            for (int unitOfTimeIndex = 0; unitOfTimeIndex < allUnits.Count; unitOfTimeIndex++)
            {
                for (int numberOfUnits = 1; numberOfUnits <= maxUnitsInAnyReportingPeriod; numberOfUnits++)
                {
                    if (unitOfTimeIndex + numberOfUnits - 1 < allUnits.Count)
                    {
                        var subReportingPeriod = new ReportingPeriod<T>(allUnits[unitOfTimeIndex], allUnits[unitOfTimeIndex + numberOfUnits - 1]);
                        allReportingPeriods.Add(subReportingPeriod);
                    }
                }
            }

            return allReportingPeriods;
        }

        /// <summary>
        /// Splits a reporting period into units-of-time by a specified granularity.
        /// </summary>
        /// <param name="reportingPeriod">The reporting period to split.</param>
        /// <param name="granularity">The granularity to use when splitting.</param>
        /// <param name="overflowStrategy">
        /// The strategy to use when <paramref name="granularity"/> is less granular than
        /// the <paramref name="reportingPeriod"/> and, when splitting, the resulting units-of-time
        /// cannot be aligned with the start and end of the reporting period.  For example,
        /// splitting Mar2015-Feb2017 by year results in 2015,2016,2017, however only 2016 is
        /// fully contained within the reporting period.  The reporting period is missing Jan2015-Feb2015
        /// and March2017-Dec2017.
        /// </param>
        /// <returns>
        /// Returns the units-of-time that split the specified reporting period by the specified granularity.
        /// The units-of-time will always be in the specified granularity, regardless of the granularity
        /// of the reporting period (e.g. splitting a fiscal month reporting period using yearly granularity
        /// will return <see cref="FiscalYear"/> objects).
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="reportingPeriod"/> has a component that is <see cref="UnitOfTimeGranularity.Unbounded"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="granularity"/> is <see cref="UnitOfTimeGranularity.Invalid"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="granularity"/> is <see cref="UnitOfTimeGranularity.Unbounded"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="overflowStrategy"/> is not <see cref="OverflowStrategy.ThrowOnOverflow"/>.</exception>
        /// <exception cref="InvalidOperationException">There was some overflow when splitting.</exception>
        public static IList<UnitOfTime> Split(this IReportingPeriod<UnitOfTime> reportingPeriod, UnitOfTimeGranularity granularity, OverflowStrategy overflowStrategy = OverflowStrategy.ThrowOnOverflow)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            if (reportingPeriod.HasComponentWithUnboundedGranularity())
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod)} {nameof(reportingPeriod.Start)} and/or {nameof(reportingPeriod.End)} is {nameof(UnitOfTimeGranularity.Unbounded)}"));
            }

            if (granularity == UnitOfTimeGranularity.Invalid)
            {
                throw new ArgumentException(Invariant($"{nameof(granularity)} is {nameof(UnitOfTimeGranularity.Invalid)}"));
            }

            if (granularity == UnitOfTimeGranularity.Unbounded)
            {
                throw new ArgumentException(Invariant($"{nameof(granularity)} is {nameof(UnitOfTimeGranularity.Unbounded)}"));
            }

            if (overflowStrategy != OverflowStrategy.ThrowOnOverflow)
            {
                throw new ArgumentException(Invariant($"{nameof(overflowStrategy)} is not {nameof(OverflowStrategy.ThrowOnOverflow)}"));
            }

            if (reportingPeriod.GetUnitOfTimeGranularity().IsAsGranularOrLessGranularThan(granularity))
            {
                var result = reportingPeriod.MakeMoreGranular(granularity).GetUnitsWithin();
                return result;
            }
            else
            {
                var result = reportingPeriod.MakeLessGranular(granularity).GetUnitsWithin();
                return result;
            }
        }

        /// <summary>
        /// Converts the the specified reporting period into the most
        /// granular possible, but equivalent, reporting period.
        /// </summary>
        /// <param name="reportingPeriod">The reporting period to operate on.</param>
        /// <returns>
        /// A reporting period that addresses the same set of time as <paramref name="reportingPeriod"/>,
        /// but is the most granular version possible of that reporting period.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reportingPeriod"/> is null.</exception>
        public static IReportingPeriod<UnitOfTime> ToMostGranular(this IReportingPeriod<UnitOfTime> reportingPeriod)
        {
            if (reportingPeriod == null)
            {
                throw new ArgumentNullException(nameof(reportingPeriod));
            }

            var mostGranularStart = reportingPeriod.Start.ToMostGranular();
            var mostGranularEnd = reportingPeriod.End.ToMostGranular();
            var result = new ReportingPeriod<UnitOfTime>(mostGranularStart.Start, mostGranularEnd.End);
            return result;
        }

        private static IReportingPeriod<UnitOfTime> MakeMoreGranular(this IReportingPeriod<UnitOfTime> reportingPeriod, UnitOfTimeGranularity granularity)
        {
            if (reportingPeriod.GetUnitOfTimeGranularity().IsMoreGranularThan(granularity))
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod)} is more granular than {nameof(granularity)}"));
            }

            var moreGranularStart = reportingPeriod.Start.MakeMoreGranular(granularity);
            var moreGranularEnd = reportingPeriod.End.MakeMoreGranular(granularity);
            var result = new ReportingPeriod<UnitOfTime>(moreGranularStart.Start, moreGranularEnd.End);
            return result;
        }

        private static IReportingPeriod<UnitOfTime> MakeMoreGranular(this UnitOfTime unitOfTime, UnitOfTimeGranularity granularity)
        {
            if (unitOfTime.UnitOfTimeGranularity.IsMoreGranularThan(granularity))
            {
                throw new ArgumentException(Invariant($"{nameof(unitOfTime)} is more granular than {nameof(granularity)}"));
            }

            if (unitOfTime.UnitOfTimeGranularity == granularity)
            {
                var result = new ReportingPeriod<UnitOfTime>(unitOfTime, unitOfTime);
                return result;
            }

            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Year)
            {
                // ReSharper disable PossibleNullReferenceException
                var unitOfTimeAsYear = unitOfTime as IHaveAYear;
                var startQuarter = QuarterNumber.Q1;
                var endQuarter = QuarterNumber.Q4;

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var moreGranularReportingPeriod = new ReportingPeriod<UnitOfTime>(new CalendarQuarter(unitOfTimeAsYear.Year, startQuarter), new CalendarQuarter(unitOfTimeAsYear.Year, endQuarter));
                    var result = moreGranularReportingPeriod.MakeMoreGranular(granularity);
                    return result;
                }

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Fiscal)
                {
                    var moreGranularReportingPeriod = new ReportingPeriod<UnitOfTime>(new FiscalQuarter(unitOfTimeAsYear.Year, startQuarter), new FiscalQuarter(unitOfTimeAsYear.Year, endQuarter));
                    var result = moreGranularReportingPeriod.MakeMoreGranular(granularity);
                    return result;
                }

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Generic)
                {
                    var moreGranularReportingPeriod = new ReportingPeriod<UnitOfTime>(new GenericQuarter(unitOfTimeAsYear.Year, startQuarter), new GenericQuarter(unitOfTimeAsYear.Year, endQuarter));
                    var result = moreGranularReportingPeriod.MakeMoreGranular(granularity);
                    return result;
                }

                // ReSharper restore PossibleNullReferenceException
                throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTime.UnitOfTimeKind);
            }

            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Quarter)
            {
                // ReSharper disable PossibleNullReferenceException
                var unitOfTimeAsQuarter = unitOfTime as IHaveAQuarter;
                var startMonth = ((((int)unitOfTimeAsQuarter.QuarterNumber) - 1) * 3) + 1;
                var endMonth = ((int)unitOfTimeAsQuarter.QuarterNumber) * 3;

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var moreGranularReportingPeriod = new ReportingPeriod<UnitOfTime>(new CalendarMonth(unitOfTimeAsQuarter.Year, (MonthOfYear)startMonth), new CalendarMonth(unitOfTimeAsQuarter.Year, (MonthOfYear)endMonth));
                    var result = moreGranularReportingPeriod.MakeMoreGranular(granularity);
                    return result;
                }

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Fiscal)
                {
                    var moreGranularReportingPeriod = new ReportingPeriod<UnitOfTime>(new FiscalMonth(unitOfTimeAsQuarter.Year, (MonthNumber)startMonth), new FiscalMonth(unitOfTimeAsQuarter.Year, (MonthNumber)endMonth));
                    var result = moreGranularReportingPeriod.MakeMoreGranular(granularity);
                    return result;
                }

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Generic)
                {
                    var moreGranularReportingPeriod = new ReportingPeriod<UnitOfTime>(new GenericMonth(unitOfTimeAsQuarter.Year, (MonthNumber)startMonth), new GenericMonth(unitOfTimeAsQuarter.Year, (MonthNumber)endMonth));
                    var result = moreGranularReportingPeriod.MakeMoreGranular(granularity);
                    return result;
                }

                // ReSharper restore PossibleNullReferenceException
                throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTime.UnitOfTimeKind);
            }

            if (unitOfTime.UnitOfTimeGranularity == UnitOfTimeGranularity.Month)
            {
                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var calendarUnitOfTime = unitOfTime as CalendarUnitOfTime;
                    var moreGranularReportingPeriod = new ReportingPeriod<UnitOfTime>(calendarUnitOfTime.GetFirstCalendarDay(), calendarUnitOfTime.GetLastCalendarDay());
                    var result = moreGranularReportingPeriod.MakeMoreGranular(granularity);
                    return result;
                }

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Fiscal)
                {
                    throw new NotSupportedException("The Fiscal kind cannot be made more granular than Month.");
                }

                if (unitOfTime.UnitOfTimeKind == UnitOfTimeKind.Generic)
                {
                    throw new NotSupportedException("The Generic kind cannot be made more granular than Month.");
                }

                throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTime.UnitOfTimeKind);
            }

            throw new NotSupportedException("This granularity is not supported: " + unitOfTime.UnitOfTimeGranularity);
        }

        private static IReportingPeriod<UnitOfTime> MakeLessGranular(this IReportingPeriod<UnitOfTime> reportingPeriod, UnitOfTimeGranularity granularity)
        {
            var reportingPeriodGranularity = reportingPeriod.GetUnitOfTimeGranularity();
            if (reportingPeriodGranularity.IsLessGranularThan(granularity))
            {
                throw new ArgumentException(Invariant($"{nameof(reportingPeriod)} is less granular than {nameof(granularity)}"));
            }

            if (reportingPeriodGranularity == granularity)
            {
                return reportingPeriod;
            }

            var unitOfTimeKind = reportingPeriod.GetUnitOfTimeKind();
            if (reportingPeriodGranularity == UnitOfTimeGranularity.Day)
            {
                // ReSharper disable PossibleNullReferenceException
                if (unitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var startAsCalendarDay = reportingPeriod.Start as CalendarDay;
                    var startMonth = new CalendarMonth(startAsCalendarDay.Year, startAsCalendarDay.MonthOfYear);
                    if (startMonth.GetFirstCalendarDay() != startAsCalendarDay)
                    {
                        throw new InvalidOperationException("Cannot convert a calendar day reporting period to a calendar month reporting period when the reporting period start time is not the first day of a month.");
                    }

                    var endAsCalendarDay = reportingPeriod.End as CalendarDay;
                    var endMonth = new CalendarMonth(endAsCalendarDay.Year, endAsCalendarDay.MonthOfYear);
                    if (endMonth.GetLastCalendarDay() != endAsCalendarDay)
                    {
                        throw new InvalidOperationException("Cannot convert a calendar day reporting period to a calendar month reporting period when the reporting period end time is not the last day of a month.");
                    }

                    var lessGranularReportingPeriod = new ReportingPeriod<CalendarMonth>(startMonth, endMonth);
                    var result = MakeLessGranular(lessGranularReportingPeriod, granularity);
                    return result;
                }

                // ReSharper restore PossibleNullReferenceException
                throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTimeKind);
            }

            if (reportingPeriodGranularity == UnitOfTimeGranularity.Month)
            {
                // ReSharper disable PossibleNullReferenceException
                var startAsMonth = reportingPeriod.Start as IHaveAMonth;
                var endAsMonth = reportingPeriod.End as IHaveAMonth;

                var quarterByStartMonth = new Dictionary<int, QuarterNumber>
                {
                    { 1, QuarterNumber.Q1 },
                    { 4, QuarterNumber.Q2 },
                    { 7, QuarterNumber.Q3 },
                    { 10, QuarterNumber.Q4 }
                };

                var quarterByEndMonth = new Dictionary<int, QuarterNumber>
                {
                    { 3, QuarterNumber.Q1 },
                    { 6, QuarterNumber.Q2 },
                    { 9, QuarterNumber.Q3 },
                    { 12, QuarterNumber.Q4 }
                };

                if (!quarterByStartMonth.ContainsKey((int)startAsMonth.MonthNumber))
                {
                    throw new InvalidOperationException("Cannot convert a monthly reporting period to a quarterly reporting period when the reporting period start time is not the first month of a recognized quarter.");
                }

                if (!quarterByEndMonth.ContainsKey((int)endAsMonth.MonthNumber))
                {
                    throw new InvalidOperationException("Cannot convert a monthly reporting period to a quarterly reporting period when the reporting period end time is not the last month of a recognized quarter.");
                }

                if (unitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var startQuarter = new CalendarQuarter(startAsMonth.Year, quarterByStartMonth[(int)startAsMonth.MonthNumber]);
                    var endQuarter = new CalendarQuarter(endAsMonth.Year, quarterByEndMonth[(int)endAsMonth.MonthNumber]);
                    var lessGranularReportingPeriod = new ReportingPeriod<CalendarQuarter>(startQuarter, endQuarter);
                    var result = MakeLessGranular(lessGranularReportingPeriod, granularity);
                    return result;
                }

                if (unitOfTimeKind == UnitOfTimeKind.Fiscal)
                {
                    var startQuarter = new FiscalQuarter(startAsMonth.Year, quarterByStartMonth[(int)startAsMonth.MonthNumber]);
                    var endQuarter = new FiscalQuarter(endAsMonth.Year, quarterByEndMonth[(int)endAsMonth.MonthNumber]);
                    var lessGranularReportingPeriod = new ReportingPeriod<FiscalQuarter>(startQuarter, endQuarter);
                    var result = MakeLessGranular(lessGranularReportingPeriod, granularity);
                    return result;
                }

                if (unitOfTimeKind == UnitOfTimeKind.Generic)
                {
                    var startQuarter = new GenericQuarter(startAsMonth.Year, quarterByStartMonth[(int)startAsMonth.MonthNumber]);
                    var endQuarter = new GenericQuarter(endAsMonth.Year, quarterByEndMonth[(int)endAsMonth.MonthNumber]);
                    var lessGranularReportingPeriod = new ReportingPeriod<GenericQuarter>(startQuarter, endQuarter);
                    var result = MakeLessGranular(lessGranularReportingPeriod, granularity);
                    return result;
                }

                // ReSharper restore PossibleNullReferenceException
                throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTimeKind);
            }

            if (reportingPeriodGranularity == UnitOfTimeGranularity.Quarter)
            {
                // ReSharper disable PossibleNullReferenceException
                var startAsQuarter = reportingPeriod.Start as IHaveAQuarter;
                var endAsQuarter = reportingPeriod.End as IHaveAQuarter;

                if (startAsQuarter.QuarterNumber != QuarterNumber.Q1)
                {
                    throw new InvalidOperationException("Cannot convert a quarterly reporting period to a yearly reporting period when the reporting period start time is not Q1.");
                }

                if (endAsQuarter.QuarterNumber != QuarterNumber.Q4)
                {
                    throw new InvalidOperationException("Cannot convert a quarterly reporting period to a yearly reporting period when the reporting period end is not Q4.");
                }

                if (unitOfTimeKind == UnitOfTimeKind.Calendar)
                {
                    var startYear = new CalendarYear(startAsQuarter.Year);
                    var endYear = new CalendarYear(endAsQuarter.Year);
                    var lessGranularReportingPeriod = new ReportingPeriod<CalendarYear>(startYear, endYear);
                    var result = MakeLessGranular(lessGranularReportingPeriod, granularity);
                    return result;
                }

                if (unitOfTimeKind == UnitOfTimeKind.Fiscal)
                {
                    var startYear = new FiscalYear(startAsQuarter.Year);
                    var endYear = new FiscalYear(endAsQuarter.Year);
                    var lessGranularReportingPeriod = new ReportingPeriod<FiscalYear>(startYear, endYear);
                    var result = MakeLessGranular(lessGranularReportingPeriod, granularity);
                    return result;
                }

                if (unitOfTimeKind == UnitOfTimeKind.Generic)
                {
                    var startYear = new GenericYear(startAsQuarter.Year);
                    var endYear = new GenericYear(endAsQuarter.Year);
                    var lessGranularReportingPeriod = new ReportingPeriod<GenericYear>(startYear, endYear);
                    var result = MakeLessGranular(lessGranularReportingPeriod, granularity);
                    return result;
                }

                // ReSharper restore PossibleNullReferenceException
                throw new NotSupportedException("This kind of unit-of-time is not supported: " + unitOfTimeKind);
            }

            throw new NotSupportedException("This granularity is not supported: " + reportingPeriodGranularity);
        }
    }
}

// ReSharper restore CheckNamespace