﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimeDummyFactory.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.AccountingTime.Recipes source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Recipes
{
    using System;

    using AutoFakeItEasy;

    using FakeItEasy;

    using OBeautifulCode.Math;

    /// <summary>
    /// A dummy factory for Accounting Time types.
    /// </summary>
#if !OBeautifulCodeAccountingTimeRecipesProject
    [System.Diagnostics.DebuggerStepThrough]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.AccountingTime.Recipes", "See package version number")]
#endif
    // ReSharper disable once InheritdocConsiderUsage
    public class AccountingTimeDummyFactory : IDummyFactory
    {
        private const int MinYear = 1950;

        private const int MaxYear = 2050;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountingTimeDummyFactory"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "This is not excessively complex.  Dummy factories typically wire-up many types.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "This is not excessively complex.  Dummy factories typically wire-up many types.")]
        public AccountingTimeDummyFactory()
        {
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(DayOfMonth.Invalid);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(MonthNumber.Invalid);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(MonthOfYear.Invalid);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(QuarterNumber.Invalid);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(UnitOfTimeKind.Invalid);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(UnitOfTimeGranularity.Invalid);
            AutoFixtureBackedDummyFactory.ConstrainDummyToExclude(ReportingPeriodComponent.Invalid);

            AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<UnitOfTime>();
            AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<CalendarUnitOfTime>();
            AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<FiscalUnitOfTime>();
            AutoFixtureBackedDummyFactory.UseRandomConcreteSubclassForDummy<GenericUnitOfTime>();

            AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IHaveAMonth>();
            AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IHaveAQuarter>();
            AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IHaveAYear>();
            AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IAmBoundedTime>();
            AutoFixtureBackedDummyFactory.UseRandomInterfaceImplementationForDummy<IAmUnboundedTime>();

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var year = ThreadSafeRandom.Next(MinYear, MaxYear + 1);
                    var result = new GenericMonth(year, A.Dummy<MonthNumber>());
                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var year = ThreadSafeRandom.Next(MinYear, MaxYear + 1);
                    var result = new GenericQuarter(year, A.Dummy<QuarterNumber>());
                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var year = ThreadSafeRandom.Next(MinYear, MaxYear + 1);
                    var result = new GenericYear(year);
                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var year = ThreadSafeRandom.Next(MinYear, MaxYear + 1);
                    var result = new FiscalMonth(year, A.Dummy<MonthNumber>());
                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var year = ThreadSafeRandom.Next(MinYear, MaxYear + 1);
                    var result = new FiscalQuarter(year, A.Dummy<QuarterNumber>());
                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var year = ThreadSafeRandom.Next(MinYear, MaxYear + 1);
                    var result = new FiscalYear(year);
                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    while (true)
                    {
                        try
                        {
                            var date = A.Dummy<DateTime>();
                            var year = ThreadSafeRandom.Next(MinYear, MaxYear + 1);
                            var result = new CalendarDay(year, (MonthOfYear)date.Month, (DayOfMonth)date.Day);
                            return result;
                        }
                        catch (ArgumentException)
                        {
                        }
                    }
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var year = ThreadSafeRandom.Next(MinYear, MaxYear + 1);
                    var result = new CalendarMonth(year, A.Dummy<MonthOfYear>());
                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var year = ThreadSafeRandom.Next(MinYear, MaxYear + 1);
                    var result = new CalendarQuarter(year, A.Dummy<QuarterNumber>());
                    return result;
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var year = ThreadSafeRandom.Next(MinYear, MaxYear + 1);
                    var result = new CalendarYear(year);
                    return result;
                });

            AddDummyCreatorForAbstractReportingPeriod<UnitOfTime>();
            AddDummyCreatorForAbstractReportingPeriod<CalendarUnitOfTime>();
            AddDummyCreatorForAbstractReportingPeriod<FiscalUnitOfTime>();
            AddDummyCreatorForAbstractReportingPeriod<GenericUnitOfTime>();
            AddDummyCreatorForConcreteReportingPeriod<CalendarDay>();
            AddDummyCreatorForConcreteReportingPeriod<CalendarQuarter>();
            AddDummyCreatorForConcreteReportingPeriod<CalendarMonth>();
            AddDummyCreatorForConcreteReportingPeriod<CalendarYear>();
            AddDummyCreatorForConcreteReportingPeriod<CalendarUnbounded>();
            AddDummyCreatorForConcreteReportingPeriod<FiscalMonth>();
            AddDummyCreatorForConcreteReportingPeriod<FiscalQuarter>();
            AddDummyCreatorForConcreteReportingPeriod<FiscalYear>();
            AddDummyCreatorForConcreteReportingPeriod<FiscalUnbounded>();
            AddDummyCreatorForConcreteReportingPeriod<GenericMonth>();
            AddDummyCreatorForConcreteReportingPeriod<GenericQuarter>();
            AddDummyCreatorForConcreteReportingPeriod<GenericYear>();
            AddDummyCreatorForConcreteReportingPeriod<GenericUnbounded>();
        }

        /// <inheritdoc />
        public Priority Priority => new FakeItEasy.Priority(1);

        /// <inheritdoc />
        public bool CanCreate(Type type)
        {
            return false;
        }

        /// <inheritdoc />
        public object Create(Type type)
        {
            return null;
        }

        private static void AddDummyCreatorForConcreteReportingPeriod<T>()
            where T : UnitOfTime
        {
            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var start = A.Dummy<T>();
                    var end = A.Dummy<T>().ThatIs(u => u.GetType() == start.GetType());
                    if ((start is IAmBoundedTime) && ((dynamic)start <= (dynamic)end))
                    {
                        return new ReportingPeriod<T>(start, end);
                    }

                    return new ReportingPeriod<T>(end, start);
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator<IReportingPeriod<T>>(
                () =>
                {
                    var result = A.Dummy<ReportingPeriod<T>>();
                    return result;
                });
        }

        private static void AddDummyCreatorForAbstractReportingPeriod<T>()
            where T : UnitOfTime
        {
            AutoFixtureBackedDummyFactory.AddDummyCreator(
                () =>
                {
                    var start = A.Dummy<T>();
                    var end = A.Dummy<T>();

                    if ((start is IAmUnboundedTime) || (end is IAmUnboundedTime))
                    {
                        if ((start is IAmUnboundedTime) && (end is IAmUnboundedTime))
                        {
                            return new ReportingPeriod<T>(start, start);
                        }

                        if (start is IAmUnboundedTime)
                        {
                            end = A.Dummy<T>().Whose(_ => _.UnitOfTimeKind == start.UnitOfTimeKind);
                        }
                        else
                        {
                            start = A.Dummy<T>().Whose(_ => _.UnitOfTimeKind == end.UnitOfTimeKind);
                        }

                        return new ReportingPeriod<T>(end, start);
                    }
                    else
                    {
                        end = A.Dummy<T>().ThatIs(u => u.GetType() == start.GetType());
                        if ((dynamic)start <= (dynamic)end)
                        {
                            return new ReportingPeriod<T>(start, end);
                        }

                        return new ReportingPeriod<T>(end, start);
                    }
                });

            AutoFixtureBackedDummyFactory.AddDummyCreator<IReportingPeriod<T>>(
                () =>
                {
                    var result = A.Dummy<ReportingPeriod<T>>();
                    return result;
                });
        }
    }
}
