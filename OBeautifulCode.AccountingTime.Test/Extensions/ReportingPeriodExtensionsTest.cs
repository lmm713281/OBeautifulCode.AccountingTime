﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoFakeItEasy;

    using FakeItEasy;

    using FluentAssertions;

    using Naos.Recipes.TupleInitializers;

    using Xunit;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Testing this class requires lots of types because of the number of unit-of-time types intersected with the options for reporting period.")]
    public static class ReportingPeriodExtensionsTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void CloneWithAdjustment___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>().Whose(_ => _.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded && _.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);
            var reportingPeriodComponent = A.Dummy<ReportingPeriodComponent>();
            var granularityToAdd = reportingPeriod.Start.UnitOfTimeGranularity;
            var unitsToAdd = A.Dummy<int>().ThatIs(i => i > -100 && i < 100);

            // Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.CloneWithAdjustment<IReportingPeriod<UnitOfTime>>(null, reportingPeriodComponent, unitsToAdd, granularityToAdd));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void CloneWithAdjustment___Should_throw_ArgumentException___When_parameter_reportingPeriodComponent_is_Invalid()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>().Whose(_ => _.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded && _.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);
            var granularityToAdd = reportingPeriod.Start.UnitOfTimeGranularity;
            var unitsToAdd = A.Dummy<int>().ThatIs(i => i > -100 && i < 100);

            // Act
            var ex = Record.Exception(() => reportingPeriod.CloneWithAdjustment<IReportingPeriod<UnitOfTime>>(ReportingPeriodComponent.Invalid, unitsToAdd, granularityToAdd));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void CloneWithAdjustment___Should_throw_ArgumentException___When_parameter_granularityOfUnitsToAdd_is_Invalid()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>().Whose(_ => _.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded && _.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);
            var reportingPeriodComponent = A.Dummy<ReportingPeriodComponent>();
            var unitsToAdd = A.Dummy<int>().ThatIs(i => i > -100 && i < 100);

            // Act
            var ex = Record.Exception(() => reportingPeriod.CloneWithAdjustment<IReportingPeriod<UnitOfTime>>(reportingPeriodComponent, unitsToAdd, UnitOfTimeGranularity.Invalid));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void CloneWithAdjustment___Should_throw_ArgumentException___When_parameter_granularityOfUnitsToAdd_is_Unbounded()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>().Whose(_ => _.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded && _.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);
            var reportingPeriodComponent = A.Dummy<ReportingPeriodComponent>();
            var unitsToAdd = A.Dummy<int>().ThatIs(i => i > -100 && i < 100);

            // Act
            var ex = Record.Exception(() => reportingPeriod.CloneWithAdjustment<IReportingPeriod<UnitOfTime>>(reportingPeriodComponent, unitsToAdd, UnitOfTimeGranularity.Unbounded));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void CloneWithAdjustment___Should_throw_ArgumentException___When_adjusting_reportingPeriod_with_an_Unbounded_Start()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>().Whose(_ => _.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded && _.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);
            var granularityToAdd = reportingPeriod.End.UnitOfTimeGranularity;
            var unitsToAdd = A.Dummy<int>().ThatIs(i => i > -100 && i < 100);

            // Act
            var ex1 = Record.Exception(() => reportingPeriod.CloneWithAdjustment<IReportingPeriod<UnitOfTime>>(ReportingPeriodComponent.Start, unitsToAdd, granularityToAdd));
            var ex2 = Record.Exception(() => reportingPeriod.CloneWithAdjustment<IReportingPeriod<UnitOfTime>>(ReportingPeriodComponent.Both, unitsToAdd, granularityToAdd));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void CloneWithAdjustment___Should_throw_ArgumentException___When_adjusting_reportingPeriod_with_an_Unbounded_End()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>().Whose(_ => _.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded && _.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded);
            var granularityToAdd = reportingPeriod.Start.UnitOfTimeGranularity;
            var unitsToAdd = A.Dummy<int>().ThatIs(i => i > -100 && i < 100);

            // Act
            var ex1 = Record.Exception(() => reportingPeriod.CloneWithAdjustment<IReportingPeriod<UnitOfTime>>(ReportingPeriodComponent.End, unitsToAdd, granularityToAdd));
            var ex2 = Record.Exception(() => reportingPeriod.CloneWithAdjustment<IReportingPeriod<UnitOfTime>>(ReportingPeriodComponent.Both, unitsToAdd, granularityToAdd));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "testing all the flavors of unit-of-time")]
        public static void CloneWithAdjustment___Should_throw_ArgumentException___When_granularityOfUnitsToAdd_is_more_granular_than_component_being_adjusted()
        {
            // Arrange
            var unitsToAdd = A.Dummy<int>().ThatIs(i => i > -100 && i < 100);
            var tests = new[]
            {
                new { ReportingPeriod = (IReportingPeriod<UnitOfTime>)A.Dummy<IReportingPeriod<CalendarDay>>(), GranularityOfUnitsToAdd = new UnitOfTimeGranularity[] { } },
                new { ReportingPeriod = (IReportingPeriod<UnitOfTime>)A.Dummy<IReportingPeriod<CalendarMonth>>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day } },
                new { ReportingPeriod = (IReportingPeriod<UnitOfTime>)A.Dummy<IReportingPeriod<CalendarQuarter>>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month } },
                new { ReportingPeriod = (IReportingPeriod<UnitOfTime>)A.Dummy<IReportingPeriod<CalendarYear>>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter } },
                new { ReportingPeriod = (IReportingPeriod<UnitOfTime>)A.Dummy<IReportingPeriod<FiscalMonth>>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day } },
                new { ReportingPeriod = (IReportingPeriod<UnitOfTime>)A.Dummy<IReportingPeriod<FiscalQuarter>>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month } },
                new { ReportingPeriod = (IReportingPeriod<UnitOfTime>)A.Dummy<IReportingPeriod<FiscalYear>>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter } },
                new { ReportingPeriod = (IReportingPeriod<UnitOfTime>)A.Dummy<IReportingPeriod<GenericMonth>>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day } },
                new { ReportingPeriod = (IReportingPeriod<UnitOfTime>)A.Dummy<IReportingPeriod<GenericQuarter>>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month } },
                new { ReportingPeriod = (IReportingPeriod<UnitOfTime>)A.Dummy<IReportingPeriod<GenericYear>>(), GranularityOfUnitsToAdd = new[] { UnitOfTimeGranularity.Day, UnitOfTimeGranularity.Month, UnitOfTimeGranularity.Quarter } }
            };

            // Act
            var exceptions = new List<Exception>();
            foreach (var test in tests)
            {
                foreach (var granularityOfUnitsToAdd in test.GranularityOfUnitsToAdd)
                {
                    exceptions.Add(Record.Exception(() => test.ReportingPeriod.CloneWithAdjustment<IReportingPeriod<UnitOfTime>>(ReportingPeriodComponent.Start, unitsToAdd, granularityOfUnitsToAdd)));
                    exceptions.Add(Record.Exception(() => test.ReportingPeriod.CloneWithAdjustment<IReportingPeriod<UnitOfTime>>(ReportingPeriodComponent.End, unitsToAdd, granularityOfUnitsToAdd)));
                    exceptions.Add(Record.Exception(() => test.ReportingPeriod.CloneWithAdjustment<IReportingPeriod<UnitOfTime>>(ReportingPeriodComponent.Both, unitsToAdd, granularityOfUnitsToAdd)));
                }
            }

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        public static void CloneWithAdjustment___Should_adjust_Start_of_reportingPeriod___When_ReportingPeriodComponent_is_Start()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 1,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2017, MonthNumber.Eleven)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = -2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Ten), new FiscalMonth(2017, MonthNumber.Eleven)),
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2014, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                }
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var actualReportingPeriod = test.ReportingPeriod.CloneWithAdjustment<ReportingPeriod<FiscalMonth>>(ReportingPeriodComponent.Start, test.UnitsToAdd, test.GranularityOfUnitsToAdd);
                actualReportingPeriod.Should().Be(test.ExpectedReportingPeriod);
            }
        }

        [Fact]
        public static void CloneWithAdjustment___Should_adjust_End_of_reportingPeriod___When_ReportingPeriodComponent_is_End()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 1,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Twelve))
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = -2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Five))
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2019, MonthNumber.Eleven))
                }
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var actualReportingPeriod = test.ReportingPeriod.CloneWithAdjustment<ReportingPeriod<FiscalMonth>>(ReportingPeriodComponent.End, test.UnitsToAdd, test.GranularityOfUnitsToAdd);
                actualReportingPeriod.Should().Be(test.ExpectedReportingPeriod);
            }
        }

        [Fact]
        public static void CloneWithAdjustment___Should_adjust_Start_and_End_of_reportingPeriod___When_ReportingPeriodComponent_is_Both()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 1,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2017, MonthNumber.Twelve))
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = -2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Ten), new FiscalMonth(2017, MonthNumber.Five))
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2018, MonthNumber.Four), new FiscalMonth(2019, MonthNumber.Eleven))
                }
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var actualReportingPeriod = test.ReportingPeriod.CloneWithAdjustment<ReportingPeriod<FiscalMonth>>(ReportingPeriodComponent.Both, test.UnitsToAdd, test.GranularityOfUnitsToAdd);
                actualReportingPeriod.Should().Be(test.ExpectedReportingPeriod);
            }
        }

        [Fact]
        public static void CloneWithAdjustment___Should_adjust_Start_of_reportingPeriod___When_ReportingPeriodComponent_is_Start_and_reportingPeriod_End_is_unbounded()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalMonth(2016, MonthNumber.Four), new FiscalUnbounded()),
                    UnitsToAdd = 1,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalMonth(2016, MonthNumber.Five), new FiscalUnbounded())
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalMonth(2016, MonthNumber.Four), new FiscalUnbounded()),
                    UnitsToAdd = -2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalMonth(2015, MonthNumber.Ten), new FiscalUnbounded())
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalMonth(2014, MonthNumber.Four), new FiscalUnbounded()),
                    UnitsToAdd = 2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalMonth(2016, MonthNumber.Four), new FiscalUnbounded())
                }
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var actualReportingPeriod = test.ReportingPeriod.CloneWithAdjustment<ReportingPeriod<FiscalUnitOfTime>>(ReportingPeriodComponent.Start, test.UnitsToAdd, test.GranularityOfUnitsToAdd);
                actualReportingPeriod.Should().Be(test.ExpectedReportingPeriod);
            }
        }

        [Fact]
        public static void CloneWithAdjustment___Should_adjust_Start_of_reportingPeriod___When_ReportingPeriodComponent_is_End_and_reportingPeriod_Start_is_unbounded()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalUnbounded(), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 1,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalUnbounded(), new FiscalMonth(2017, MonthNumber.Twelve))
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalUnbounded(), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = -2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalUnbounded(), new FiscalMonth(2017, MonthNumber.Five))
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalUnbounded(), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalUnitOfTime>(new FiscalUnbounded(), new FiscalMonth(2019, MonthNumber.Eleven))
                }
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var actualReportingPeriod = test.ReportingPeriod.CloneWithAdjustment<ReportingPeriod<FiscalUnitOfTime>>(ReportingPeriodComponent.End, test.UnitsToAdd, test.GranularityOfUnitsToAdd);
                actualReportingPeriod.Should().Be(test.ExpectedReportingPeriod);
            }
        }

        [Fact]
        public static void CloneWithAdjustment___Should_convert_return_type_to_TReportingPeriod___When_called()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 1,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2017, MonthNumber.Twelve))
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = -2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2015, MonthNumber.Ten), new FiscalMonth(2017, MonthNumber.Five))
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year,
                    ExpectedReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2018, MonthNumber.Four), new FiscalMonth(2019, MonthNumber.Eleven))
                }
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var actualReportingPeriod1 = test.ReportingPeriod.CloneWithAdjustment<ReportingPeriod<FiscalUnitOfTime>>(ReportingPeriodComponent.Both, test.UnitsToAdd, test.GranularityOfUnitsToAdd);
                var actualReportingPeriod2 = test.ReportingPeriod.CloneWithAdjustment<ReportingPeriod<UnitOfTime>>(ReportingPeriodComponent.Both, test.UnitsToAdd, test.GranularityOfUnitsToAdd);
                actualReportingPeriod1.Should().Be(test.ExpectedReportingPeriod);
                actualReportingPeriod2.Should().Be(test.ExpectedReportingPeriod);
            }
        }

        [Fact]
        public static void CloneWithAdjustment___Should_throw_InvalidOperationException___When_adjusted_reporting_period_cannot_be_converted_to_return_type_TReportingPeriod___When_called()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 1,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month,
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = -2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter,
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Eleven)),
                    UnitsToAdd = 2,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year,
                }
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var ex1 = Record.Exception(() => test.ReportingPeriod.CloneWithAdjustment<ReportingPeriod<CalendarUnitOfTime>>(ReportingPeriodComponent.Both, test.UnitsToAdd, test.GranularityOfUnitsToAdd));
                var ex2 = Record.Exception(() => test.ReportingPeriod.CloneWithAdjustment<ReportingPeriod<GenericMonth>>(ReportingPeriodComponent.Both, test.UnitsToAdd, test.GranularityOfUnitsToAdd));
                ex1.Should().BeOfType<InvalidOperationException>();
                ex2.Should().BeOfType<InvalidOperationException>();
            }
        }

        [Fact]
        public static void CloneWithAdjustment___Should_throw_InvalidOperationException___When_adjusting_Start_and_adjusting_reporting_period_causes_Start_to_be_Greater_than_End___When_called()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Eleven)),
                    UnitsToAdd = 8,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Eleven)),
                    UnitsToAdd = 3,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2014, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Three)),
                    UnitsToAdd = 3,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year
                }
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var ex = Record.Exception(() => test.ReportingPeriod.CloneWithAdjustment<ReportingPeriod<FiscalMonth>>(ReportingPeriodComponent.Start, test.UnitsToAdd, test.GranularityOfUnitsToAdd));
                ex.Should().BeOfType<InvalidOperationException>();
            }
        }

        [Fact]
        public static void CloneWithAdjustment___Should_throw_InvalidOperationException___When_adjusting_End_and_adjusting_reporting_period_causes_Start_to_be_Greater_than_End___When_called()
        {
            // Arrange
            var tests = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Eleven)),
                    UnitsToAdd = -8,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Month
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Eleven)),
                    UnitsToAdd = -3,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Quarter
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2014, MonthNumber.Four), new FiscalMonth(2017, MonthNumber.Three)),
                    UnitsToAdd = -3,
                    GranularityOfUnitsToAdd = UnitOfTimeGranularity.Year
                }
            };

            // Act, Assert
            foreach (var test in tests)
            {
                var ex = Record.Exception(() => test.ReportingPeriod.CloneWithAdjustment<ReportingPeriod<FiscalMonth>>(ReportingPeriodComponent.End, test.UnitsToAdd, test.GranularityOfUnitsToAdd));
                ex.Should().BeOfType<InvalidOperationException>();
            }
        }

        [Fact]
        public static void CreatePermutations___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.CreatePermutations<UnitOfTime>(null, A.Dummy<PositiveInteger>()));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void CreatePermutations___Should_throw_ArgumentException___When_parameter_reportingPeriod_Start_and_or_End_is_unbounded()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarUnitOfTime>(new CalendarUnbounded(), A.Dummy<CalendarUnitOfTime>());
            var reportingPeriod2 = new ReportingPeriod<GenericUnitOfTime>(A.Dummy<GenericUnitOfTime>(), new GenericUnbounded());
            var reportingPeriod3 = new ReportingPeriod<FiscalUnitOfTime>(new FiscalUnbounded(), new FiscalUnbounded());

            // Act
            var ex1 = Record.Exception(() => reportingPeriod1.CreatePermutations(A.Dummy<PositiveInteger>()));
            var ex2 = Record.Exception(() => reportingPeriod2.CreatePermutations(A.Dummy<PositiveInteger>()));
            var ex3 = Record.Exception(() => reportingPeriod3.CreatePermutations(A.Dummy<PositiveInteger>()));

            // Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
            ex3.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void CreatePermutations___Should_throw_ArgumentOutOfRangeException___When_parameter_maxUnitsInAnyReportingPeriod_is_0_or_less()
        {
            // Arrange
            var reportingPeriod = A.Dummy<ReportingPeriod<UnitOfTime>>().Whose(_ => _.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded && _.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded);

            // Act
            var ex1 = Record.Exception(() => reportingPeriod.CreatePermutations(0));
            var ex2 = Record.Exception(() => reportingPeriod.CreatePermutations(A.Dummy<NegativeInteger>()));

            // Assert
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_CalendarDay()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight));
            var reportingPeriod2 = new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three)));

            permutations2b.Should().Equal(
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyEight), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.February, DayOfMonth.TwentyNine), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.One), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Two), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three)),
                new ReportingPeriod<CalendarDay>(new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three), new CalendarDay(2016, MonthOfYear.March, DayOfMonth.Three)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_CalendarMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.February));
            var reportingPeriod2 = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.June));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.February)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.March)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.April), new CalendarMonth(2016, MonthOfYear.April)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.May), new CalendarMonth(2016, MonthOfYear.May)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.June), new CalendarMonth(2016, MonthOfYear.June)));

            permutations2b.Should().Equal(
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.February)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.March)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.February), new CalendarMonth(2016, MonthOfYear.April)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.March)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.April)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.March), new CalendarMonth(2016, MonthOfYear.May)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.April), new CalendarMonth(2016, MonthOfYear.April)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.April), new CalendarMonth(2016, MonthOfYear.May)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.April), new CalendarMonth(2016, MonthOfYear.June)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.May), new CalendarMonth(2016, MonthOfYear.May)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.May), new CalendarMonth(2016, MonthOfYear.June)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2016, MonthOfYear.June), new CalendarMonth(2016, MonthOfYear.June)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_FiscalMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Six));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Two)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Three)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Six), new FiscalMonth(2016, MonthNumber.Six)));

            permutations2b.Should().Equal(
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Two)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Three)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Two), new FiscalMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Three)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Three), new FiscalMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Four), new FiscalMonth(2016, MonthNumber.Six)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Five), new FiscalMonth(2016, MonthNumber.Six)),
                new ReportingPeriod<FiscalMonth>(new FiscalMonth(2016, MonthNumber.Six), new FiscalMonth(2016, MonthNumber.Six)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_GenericMonth()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Two));
            var reportingPeriod2 = new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Six));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Two)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Three)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Five), new GenericMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Six), new GenericMonth(2016, MonthNumber.Six)));

            permutations2b.Should().Equal(
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Two)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Three)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Two), new GenericMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Three)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Three), new GenericMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Four)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Four), new GenericMonth(2016, MonthNumber.Six)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Five), new GenericMonth(2016, MonthNumber.Five)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Five), new GenericMonth(2016, MonthNumber.Six)),
                new ReportingPeriod<GenericMonth>(new GenericMonth(2016, MonthNumber.Six), new GenericMonth(2016, MonthNumber.Six)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_CalendarQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q2));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q4), new CalendarQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q2)));

            permutations2b.Should().Equal(
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q2), new CalendarQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q4), new CalendarQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q4), new CalendarQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2016, QuarterNumber.Q4), new CalendarQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod<CalendarQuarter>(new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q2)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_FiscalQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q2));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q4), new FiscalQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2017, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q2)));

            permutations2b.Should().Equal(
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q2), new FiscalQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q3), new FiscalQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q4), new FiscalQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q4), new FiscalQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2016, QuarterNumber.Q4), new FiscalQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2017, QuarterNumber.Q1), new FiscalQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod<FiscalQuarter>(new FiscalQuarter(2017, QuarterNumber.Q2), new FiscalQuarter(2017, QuarterNumber.Q2)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_GenericQuarter()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2));
            var reportingPeriod2 = new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q2));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q4), new GenericQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q2)));

            permutations2b.Should().Equal(
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q2)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q2), new GenericQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q3)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q3), new GenericQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q4), new GenericQuarter(2016, QuarterNumber.Q4)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q4), new GenericQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2016, QuarterNumber.Q4), new GenericQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q1)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2017, QuarterNumber.Q1), new GenericQuarter(2017, QuarterNumber.Q2)),
                new ReportingPeriod<GenericQuarter>(new GenericQuarter(2017, QuarterNumber.Q2), new GenericQuarter(2017, QuarterNumber.Q2)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_CalendarYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2016));
            var reportingPeriod2 = new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2019));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2016)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2017)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2018), new CalendarYear(2018)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2019), new CalendarYear(2019)));

            permutations2b.Should().Equal(
                new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2016)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2017)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2016), new CalendarYear(2018)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2017)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2018)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2019)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2018), new CalendarYear(2018)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2018), new CalendarYear(2019)),
                new ReportingPeriod<CalendarYear>(new CalendarYear(2019), new CalendarYear(2019)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_FiscalYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2016));
            var reportingPeriod2 = new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2019));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2016)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2017), new FiscalYear(2017)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2018), new FiscalYear(2018)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2019), new FiscalYear(2019)));

            permutations2b.Should().Equal(
                new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2016)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2017)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2016), new FiscalYear(2018)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2017), new FiscalYear(2017)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2017), new FiscalYear(2018)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2017), new FiscalYear(2019)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2018), new FiscalYear(2018)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2018), new FiscalYear(2019)),
                new ReportingPeriod<FiscalYear>(new FiscalYear(2019), new FiscalYear(2019)));
        }

        [Fact]
        public static void CreatePermutations___Should_return_permutations___When_called_for_reporting_period_of_GenericYear()
        {
            // Arrange
            var reportingPeriod1 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2016));
            var reportingPeriod2 = new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2019));

            // Act
            var permutations1a = reportingPeriod1.CreatePermutations(1);
            var permutations1b = reportingPeriod1.CreatePermutations(5);

            var permutations2a = reportingPeriod2.CreatePermutations(1);
            var permutations2b = reportingPeriod2.CreatePermutations(3);

            // Assert
            permutations1a.Should().Equal(reportingPeriod1);
            permutations1b.Should().Equal(reportingPeriod1);

            permutations2a.Should().Equal(
                new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2016)),
                new ReportingPeriod<GenericYear>(new GenericYear(2017), new GenericYear(2017)),
                new ReportingPeriod<GenericYear>(new GenericYear(2018), new GenericYear(2018)),
                new ReportingPeriod<GenericYear>(new GenericYear(2019), new GenericYear(2019)));

            permutations2b.Should().Equal(
                new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2016)),
                new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2017)),
                new ReportingPeriod<GenericYear>(new GenericYear(2016), new GenericYear(2018)),
                new ReportingPeriod<GenericYear>(new GenericYear(2017), new GenericYear(2017)),
                new ReportingPeriod<GenericYear>(new GenericYear(2017), new GenericYear(2018)),
                new ReportingPeriod<GenericYear>(new GenericYear(2017), new GenericYear(2019)),
                new ReportingPeriod<GenericYear>(new GenericYear(2018), new GenericYear(2018)),
                new ReportingPeriod<GenericYear>(new GenericYear(2018), new GenericYear(2019)),
                new ReportingPeriod<GenericYear>(new GenericYear(2019), new GenericYear(2019)));
        }

        [Fact]
        public static void Split___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.Split(null, A.Dummy<UnitOfTimeGranularity>()));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void Split___Should_throw_ArgumentException___When_parameter_reportingPeriod_has_an_unbounded_component()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                A.Dummy<IReportingPeriod<UnitOfTime>>().Whose(_ => (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded)),
                A.Dummy<IReportingPeriod<UnitOfTime>>().Whose(_ => (_.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)),
                A.Dummy<IReportingPeriod<UnitOfTime>>().Whose(_ => (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)),
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(A.Dummy<UnitOfTimeGranularity>()))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        public static void Split___Should_throw_ArgumentException___When_parameter_granularity_is_Invalid()
        {
            // Arrange
            var reportingPeriod = A.Dummy<IReportingPeriod<UnitOfTime>>().Whose(_ => !_.HasComponentWithUnboundedGranularity());

            // Act
            var ex = Record.Exception(() => reportingPeriod.Split(UnitOfTimeGranularity.Invalid));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarYear___When_reportingPeriod_is_in_CalendarYear_and_granularity_is_Year()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2017)),
                    ExpectedUnitsOfTime = new[] { new CalendarYear(2017) }
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2018)),
                    ExpectedUnitsOfTime = new[] { new CalendarYear(2017), new CalendarYear(2018) }
                }
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Year), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            // ReSharper disable CoVariantArrayConversion
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
            // ReSharper restore CoVariantArrayConversion
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarQuarter___When_reportingPeriod_is_in_CalendarYear_and_granularity_is_Quarter()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2017)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q4) }
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2018)),
                    ExpectedUnitsOfTime = new[] { new CalendarQuarter(2017, QuarterNumber.Q1), new CalendarQuarter(2017, QuarterNumber.Q2), new CalendarQuarter(2017, QuarterNumber.Q3), new CalendarQuarter(2017, QuarterNumber.Q4), new CalendarQuarter(2018, QuarterNumber.Q1), new CalendarQuarter(2018, QuarterNumber.Q2), new CalendarQuarter(2018, QuarterNumber.Q3), new CalendarQuarter(2018, QuarterNumber.Q4) }
                }
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Quarter), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            // ReSharper disable CoVariantArrayConversion
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
            // ReSharper restore CoVariantArrayConversion
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarMonth___When_reportingPeriod_is_in_CalendarYear_and_granularity_is_Month()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2017)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2017, MonthOfYear.February), new CalendarMonth(2017, MonthOfYear.March), new CalendarMonth(2017, MonthOfYear.April), new CalendarMonth(2017, MonthOfYear.May), new CalendarMonth(2017, MonthOfYear.June), new CalendarMonth(2017, MonthOfYear.July), new CalendarMonth(2017, MonthOfYear.August), new CalendarMonth(2017, MonthOfYear.September), new CalendarMonth(2017, MonthOfYear.October), new CalendarMonth(2017, MonthOfYear.November), new CalendarMonth(2017, MonthOfYear.December) }
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2018)),
                    ExpectedUnitsOfTime = new[] { new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2017, MonthOfYear.February), new CalendarMonth(2017, MonthOfYear.March), new CalendarMonth(2017, MonthOfYear.April), new CalendarMonth(2017, MonthOfYear.May), new CalendarMonth(2017, MonthOfYear.June), new CalendarMonth(2017, MonthOfYear.July), new CalendarMonth(2017, MonthOfYear.August), new CalendarMonth(2017, MonthOfYear.September), new CalendarMonth(2017, MonthOfYear.October), new CalendarMonth(2017, MonthOfYear.November), new CalendarMonth(2017, MonthOfYear.December), new CalendarMonth(2018, MonthOfYear.January), new CalendarMonth(2018, MonthOfYear.February), new CalendarMonth(2018, MonthOfYear.March), new CalendarMonth(2018, MonthOfYear.April), new CalendarMonth(2018, MonthOfYear.May), new CalendarMonth(2018, MonthOfYear.June), new CalendarMonth(2018, MonthOfYear.July), new CalendarMonth(2018, MonthOfYear.August), new CalendarMonth(2018, MonthOfYear.September), new CalendarMonth(2018, MonthOfYear.October), new CalendarMonth(2018, MonthOfYear.November), new CalendarMonth(2018, MonthOfYear.December) }
                }
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Month), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            // ReSharper disable CoVariantArrayConversion
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
            // ReSharper restore CoVariantArrayConversion
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarDay___When_reportingPeriod_is_in_CalendarYear_and_granularity_is_Day()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2017)),
                    ExpectedUnitsOfTimeBeginning = new[] { new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Three) },
                    ExpectedUnitsOfTimeEnd = new[] { new CalendarDay(2017, MonthOfYear.December, DayOfMonth.TwentyNine), new CalendarDay(2017, MonthOfYear.December, DayOfMonth.Thirty), new CalendarDay(2017, MonthOfYear.December, DayOfMonth.ThirtyOne) },
                    ExpectedCount = 365
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<CalendarYear>(new CalendarYear(2017), new CalendarYear(2018)),
                    ExpectedUnitsOfTimeBeginning = new[] { new CalendarDay(2017, MonthOfYear.January, DayOfMonth.One), new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Two), new CalendarDay(2017, MonthOfYear.January, DayOfMonth.Three) },
                    ExpectedUnitsOfTimeEnd = new[] { new CalendarDay(2018, MonthOfYear.December, DayOfMonth.TwentyNine), new CalendarDay(2018, MonthOfYear.December, DayOfMonth.Thirty), new CalendarDay(2018, MonthOfYear.December, DayOfMonth.ThirtyOne) },
                    ExpectedCount = 365 * 2
                }
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Day), ExpectedBeginning = _.ExpectedUnitsOfTimeBeginning, ExpectedEnd = _.ExpectedUnitsOfTimeEnd, _.ExpectedCount }).ToList();

            // Assert
            // ReSharper disable CoVariantArrayConversion
            results.ForEach(_ =>
            {
                _.Actual.Should().StartWith(_.ExpectedBeginning);
                _.Actual.Should().EndWith(_.ExpectedEnd);
                _.Actual.Should().HaveCount(_.ExpectedCount);
            });
            // ReSharper restore CoVariantArrayConversion
        }

        [Fact]
        public static void Split___Should_throw_InvalidOperationException___When_reportingPeriod_is_in_CalendarMonth_and_granularity_is_Year_and_there_is_overflow()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2017, MonthOfYear.March), new CalendarMonth(2018, MonthOfYear.February)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2017, MonthOfYear.April), new CalendarMonth(2019, MonthOfYear.July)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2019, MonthOfYear.November)),
                new ReportingPeriod<CalendarMonth>(new CalendarMonth(2017, MonthOfYear.February), new CalendarMonth(2019, MonthOfYear.December))
            };

            // Act
            var exceptions = reportingPeriods.Select(_ => Record.Exception(() => _.Split(UnitOfTimeGranularity.Year))).ToList();

            // Assert
            exceptions.ForEach(_ => _.Should().BeOfType<InvalidOperationException>());
        }

        [Fact]
        public static void Split___Should_return_reportingPeriod_split_into_CalendarYear___When_reportingPeriod_is_in_CalendarMonth_and_granularity_is_Year()
        {
            // Arrange
            var reportingPeriods = new[]
            {
                new
                {
                    ReportingPeriod = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2017, MonthOfYear.December)),
                    ExpectedUnitsOfTime = new[] { new CalendarYear(2017) }
                },
                new
                {
                    ReportingPeriod = new ReportingPeriod<CalendarMonth>(new CalendarMonth(2017, MonthOfYear.January), new CalendarMonth(2018, MonthOfYear.December)),
                    ExpectedUnitsOfTime = new[] { new CalendarYear(2017), new CalendarYear(2018) }
                }
            };

            // Act
            var results = reportingPeriods.Select(_ => new { Actual = _.ReportingPeriod.Split(UnitOfTimeGranularity.Year), Expected = _.ExpectedUnitsOfTime }).ToList();

            // Assert
            // ReSharper disable CoVariantArrayConversion
            results.ForEach(_ => _.Actual.Should().Equal(_.Expected));
            // ReSharper restore CoVariantArrayConversion
        }

        [Fact]
        public static void GetUnitOfTimeKind___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.GetUnitOfTimeKind(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Need to test all flavors of unit-of-time")]
        public static void GetUnitOfTimeKind___Should_return_the_kind_of_unit_of_time_used_in_the_reporting_period___When_called()
        {
            // Arrange
            var reportingPeriods = new List<Tuple<IReportingPeriod<UnitOfTime>, UnitOfTimeKind>>
            {
                { A.Dummy<IReportingPeriod<CalendarUnitOfTime>>(), UnitOfTimeKind.Calendar },
                { A.Dummy<IReportingPeriod<CalendarDay>>(), UnitOfTimeKind.Calendar },
                { A.Dummy<IReportingPeriod<CalendarMonth>>(), UnitOfTimeKind.Calendar },
                { A.Dummy<IReportingPeriod<CalendarQuarter>>(), UnitOfTimeKind.Calendar },
                { A.Dummy<IReportingPeriod<CalendarYear>>(), UnitOfTimeKind.Calendar },
                { A.Dummy<IReportingPeriod<CalendarUnbounded>>(), UnitOfTimeKind.Calendar },
                { A.Dummy<IReportingPeriod<FiscalUnitOfTime>>(), UnitOfTimeKind.Fiscal },
                { A.Dummy<IReportingPeriod<FiscalMonth>>(), UnitOfTimeKind.Fiscal },
                { A.Dummy<IReportingPeriod<FiscalQuarter>>(), UnitOfTimeKind.Fiscal },
                { A.Dummy<IReportingPeriod<FiscalYear>>(), UnitOfTimeKind.Fiscal },
                { A.Dummy<IReportingPeriod<FiscalUnbounded>>(), UnitOfTimeKind.Fiscal },
                { A.Dummy<IReportingPeriod<GenericUnitOfTime>>(), UnitOfTimeKind.Generic },
                { A.Dummy<IReportingPeriod<GenericMonth>>(), UnitOfTimeKind.Generic },
                { A.Dummy<IReportingPeriod<GenericQuarter>>(), UnitOfTimeKind.Generic },
                { A.Dummy<IReportingPeriod<GenericYear>>(), UnitOfTimeKind.Generic },
                { A.Dummy<IReportingPeriod<GenericUnbounded>>(), UnitOfTimeKind.Generic },
            };

            // Act
            var unitOfTimeKinds = reportingPeriods.Select(_ => new { Actual = _.Item1.GetUnitOfTimeKind(), Expected = _.Item2 }).ToList();

            // Assert
            unitOfTimeKinds.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void GetUnitOfTimeGranularity___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.GetUnitOfTimeGranularity(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Need to test all flavors of unit-of-time")]
        public static void GetUnitOfTimeGranularity___Should_throw_ArgumentException___When_reportingPeriod_Start_and_End_has_different_granularity()
        {
            // Arrange
            var reportingPeriods = new IReportingPeriod<UnitOfTime>[]
            {
                A.Dummy<IReportingPeriod<CalendarUnitOfTime>>().Whose(_ => (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded)),
                A.Dummy<IReportingPeriod<CalendarUnitOfTime>>().Whose(_ => (_.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)),
                A.Dummy<IReportingPeriod<FiscalUnitOfTime>>().Whose(_ => (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded)),
                A.Dummy<IReportingPeriod<FiscalUnitOfTime>>().Whose(_ => (_.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)),
                A.Dummy<IReportingPeriod<GenericUnitOfTime>>().Whose(_ => (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded)),
                A.Dummy<IReportingPeriod<GenericUnitOfTime>>().Whose(_ => (_.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded)),
            };

            // Act
            var unitOfTimeGranularity = reportingPeriods.Select(_ => Record.Exception(() => _.GetUnitOfTimeGranularity())).ToList();

            // Assert
            unitOfTimeGranularity.ForEach(_ => _.Should().BeOfType<ArgumentException>());
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Need to test all flavors of unit-of-time")]
        public static void GetUnitOfTimeGranularity___Should_return_the_granularity_of_unit_of_time_used_in_the_reporting_period___When_called()
        {
            // Arrange
            var reportingPeriods = new List<Tuple<IReportingPeriod<UnitOfTime>, UnitOfTimeGranularity>>
            {
                { A.Dummy<IReportingPeriod<CalendarDay>>(), UnitOfTimeGranularity.Day },
                { A.Dummy<IReportingPeriod<CalendarMonth>>(), UnitOfTimeGranularity.Month },
                { A.Dummy<IReportingPeriod<CalendarQuarter>>(), UnitOfTimeGranularity.Quarter },
                { A.Dummy<IReportingPeriod<CalendarYear>>(), UnitOfTimeGranularity.Year },
                { A.Dummy<IReportingPeriod<CalendarUnbounded>>(), UnitOfTimeGranularity.Unbounded },
                { A.Dummy<IReportingPeriod<FiscalMonth>>(), UnitOfTimeGranularity.Month },
                { A.Dummy<IReportingPeriod<FiscalQuarter>>(), UnitOfTimeGranularity.Quarter },
                { A.Dummy<IReportingPeriod<FiscalYear>>(), UnitOfTimeGranularity.Year },
                { A.Dummy<IReportingPeriod<FiscalUnbounded>>(), UnitOfTimeGranularity.Unbounded },
                { A.Dummy<IReportingPeriod<GenericMonth>>(), UnitOfTimeGranularity.Month },
                { A.Dummy<IReportingPeriod<GenericQuarter>>(), UnitOfTimeGranularity.Quarter },
                { A.Dummy<IReportingPeriod<GenericYear>>(), UnitOfTimeGranularity.Year },
                { A.Dummy<IReportingPeriod<GenericUnbounded>>(), UnitOfTimeGranularity.Unbounded }
            };

            // Act
            var unitOfTimeGranularity = reportingPeriods.Select(_ => new { Actual = _.Item1.GetUnitOfTimeGranularity(), Expected = _.Item2 }).ToList();

            // Assert
            unitOfTimeGranularity.ForEach(_ => _.Actual.Should().Be(_.Expected));
        }

        [Fact]
        public static void HasComponentWithUnboundedGranularity___Should_throw_ArgumentNullException___When_parameter_reportingPeriod_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ReportingPeriodExtensions.HasComponentWithUnboundedGranularity(null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void HasComponentWithUnboundedGranularity___Should_return_false___When_neither_the_Start_nor_End_component_has_an_Unbounded_UnitOfTimeGranularity()
        {
            // Arrange
            var reportingPeriod = A.Dummy<IReportingPeriod<UnitOfTime>>().Whose(_ => (_.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded));

            // Act
            var result = reportingPeriod.HasComponentWithUnboundedGranularity();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public static void HasComponentWithUnboundedGranularity___Should_return_true___When_either_the_Start_or_End_or_both_components_has_an_Unbounded_UnitOfTimeGranularity()
        {
            // Arrange
            var reportingPeriod1 = A.Dummy<IReportingPeriod<UnitOfTime>>().Whose(_ => (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded));
            var reportingPeriod2 = A.Dummy<IReportingPeriod<UnitOfTime>>().Whose(_ => (_.Start.UnitOfTimeGranularity != UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded));
            var reportingPeriod3 = A.Dummy<IReportingPeriod<UnitOfTime>>().Whose(_ => (_.Start.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded) && (_.End.UnitOfTimeGranularity == UnitOfTimeGranularity.Unbounded));

            // Act
            var result1 = reportingPeriod1.HasComponentWithUnboundedGranularity();
            var result2 = reportingPeriod2.HasComponentWithUnboundedGranularity();
            var result3 = reportingPeriod3.HasComponentWithUnboundedGranularity();

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
        }

        // ReSharper restore InconsistentNaming
    }
}

// ReSharper restore CheckNamespace