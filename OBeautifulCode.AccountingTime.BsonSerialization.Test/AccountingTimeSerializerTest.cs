﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimeSerializerTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.BsonSerialization.Test
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FakeItEasy;

    using FluentAssertions;

    using MongoDB.Bson;
    using MongoDB.Driver;

    using Xunit;

    public static class AccountingTimeSerializerTest
    {
        private const string DatabaseName = "test";

        private static readonly IMongoDatabase Database = new MongoClient().GetDatabase(DatabaseName);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "This is calling a method.  No fields are initized in the constructor.")]
        static AccountingTimeSerializerTest()
        {
            AccountingTimeSerializer.Register();
        }

        [Fact]
        public static async Task UnitOfTimeModel_without_nulls___Should_roundtrip_to_Mongo_and_back___When_using_custom_UnitOfTime_serializers()
        {
            // Arrange
            var collection = Database.GetCollection<UnitOfTimeModel>(nameof(UnitOfTimeModel));
            var expectedModel = A.Dummy<UnitOfTimeModel>();

            // Act
            collection.InsertOne(expectedModel);
            var actualModel = (await collection.Find(_ => _.Id == expectedModel.Id).ToListAsync()).Single();

            // Assert
            actualModel.UnitOfTime.Should().Be(expectedModel.UnitOfTime);
            actualModel.CalendarUnitOfTime.Should().Be(expectedModel.CalendarUnitOfTime);
            actualModel.CalendarDay.Should().Be(expectedModel.CalendarDay);
            actualModel.CalendarMonth.Should().Be(expectedModel.CalendarMonth);
            actualModel.CalendarQuarter.Should().Be(expectedModel.CalendarQuarter);
            actualModel.CalendarYear.Should().Be(expectedModel.CalendarYear);
            actualModel.FiscalUnitOfTime.Should().Be(expectedModel.FiscalUnitOfTime);
            actualModel.FiscalMonth.Should().Be(expectedModel.FiscalMonth);
            actualModel.FiscalQuarter.Should().Be(expectedModel.FiscalQuarter);
            actualModel.FiscalYear.Should().Be(expectedModel.FiscalYear);
            actualModel.GenericUnitOfTime.Should().Be(expectedModel.GenericUnitOfTime);
            actualModel.GenericMonth.Should().Be(expectedModel.GenericMonth);
            actualModel.GenericQuarter.Should().Be(expectedModel.GenericQuarter);
            actualModel.GenericYear.Should().Be(expectedModel.GenericYear);
        }

        [Fact]
        public static async Task UnitOfTimeModel_with_nulls___Should_roundtrip_to_Mongo_and_back___When_using_custom_UnitOfTime_serializers()
        {
            // Arrange
            var collection = Database.GetCollection<UnitOfTimeModel>(nameof(UnitOfTimeModel));
            var expectedModel = new UnitOfTimeModel();

            // Act
            collection.InsertOne(expectedModel);
            var actualModel = (await collection.Find(_ => _.Id == expectedModel.Id).ToListAsync()).Single();

            // Assert
            actualModel.UnitOfTime.Should().BeNull();
            actualModel.CalendarUnitOfTime.Should().BeNull();
            actualModel.CalendarDay.Should().BeNull();
            actualModel.CalendarMonth.Should().BeNull();
            actualModel.CalendarQuarter.Should().BeNull();
            actualModel.CalendarYear.Should().BeNull();
            actualModel.FiscalUnitOfTime.Should().BeNull();
            actualModel.FiscalMonth.Should().BeNull();
            actualModel.FiscalQuarter.Should().BeNull();
            actualModel.FiscalYear.Should().BeNull();
            actualModel.GenericUnitOfTime.Should().BeNull();
            actualModel.GenericMonth.Should().BeNull();
            actualModel.GenericQuarter.Should().BeNull();
            actualModel.GenericYear.Should().BeNull();
        }

        [Fact]
        public static void UnitOfTimeModel_without_nulls___Should_serialize_to_sortable_string_representation_of_UnitOfTime___When_using_custom_UnitOfTime_serializers()
        {
            // Arrange
            var model = A.Dummy<UnitOfTimeModel>();
            var expectedJson =
                "\"" + nameof(model.UnitOfTime) + "\" : \"" + model.UnitOfTime.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.CalendarUnitOfTime) + "\" : \"" + model.CalendarUnitOfTime.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.CalendarDay) + "\" : \"" + model.CalendarDay.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.CalendarMonth) + "\" : \"" + model.CalendarMonth.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.CalendarQuarter) + "\" : \"" + model.CalendarQuarter.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.CalendarYear) + "\" : \"" + model.CalendarYear.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.FiscalUnitOfTime) + "\" : \"" + model.FiscalUnitOfTime.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.FiscalMonth) + "\" : \"" + model.FiscalMonth.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.FiscalQuarter) + "\" : \"" + model.FiscalQuarter.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.FiscalYear) + "\" : \"" + model.FiscalYear.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.GenericUnitOfTime) + "\" : \"" + model.GenericUnitOfTime.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.GenericMonth) + "\" : \"" + model.GenericMonth.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.GenericQuarter) + "\" : \"" + model.GenericQuarter.SerializeToSortableString() + "\", " +
                "\"" + nameof(model.GenericYear) + "\" : \"" + model.GenericYear.SerializeToSortableString() + "\"";

            // Act
            var actualJson = model.ToJson();

            // Assert
            actualJson.Should().Contain(expectedJson);
        }

        [Fact]
        public static async Task ReportingPeriodModel_without_nulls___Should_roundtrip_to_Mongo_and_back___When_using_custom_IReportingPeriod_serializers()
        {
            // Arrange
            var collection1 = Database.GetCollection<ReportingPeriodModel>(nameof(ReportingPeriodModel));
            var collection2 = Database.GetCollection<IReportingPeriodModel>(nameof(IReportingPeriodModel));

            var expectedModel1 = A.Dummy<ReportingPeriodModel>();
            var expectedModel2 = A.Dummy<IReportingPeriodModel>();

            // Act
            collection1.InsertOne(expectedModel1);
            collection2.InsertOne(expectedModel2);

            var actualModel1 = (await collection1.Find(_ => _.Id == expectedModel1.Id).ToListAsync()).Single();
            var actualModel2 = (await collection2.Find(_ => _.Id == expectedModel2.Id).ToListAsync()).Single();

            // Assert
            actualModel1.UnitOfTime.Should().Be(expectedModel1.UnitOfTime);
            actualModel1.CalendarUnitOfTime.Should().Be(expectedModel1.CalendarUnitOfTime);
            actualModel1.CalendarDay.Should().Be(expectedModel1.CalendarDay);
            actualModel1.CalendarMonth.Should().Be(expectedModel1.CalendarMonth);
            actualModel1.CalendarQuarter.Should().Be(expectedModel1.CalendarQuarter);
            actualModel1.CalendarYear.Should().Be(expectedModel1.CalendarYear);
            actualModel1.FiscalUnitOfTime.Should().Be(expectedModel1.FiscalUnitOfTime);
            actualModel1.FiscalMonth.Should().Be(expectedModel1.FiscalMonth);
            actualModel1.FiscalQuarter.Should().Be(expectedModel1.FiscalQuarter);
            actualModel1.FiscalYear.Should().Be(expectedModel1.FiscalYear);
            actualModel1.GenericUnitOfTime.Should().Be(expectedModel1.GenericUnitOfTime);
            actualModel1.GenericMonth.Should().Be(expectedModel1.GenericMonth);
            actualModel1.GenericQuarter.Should().Be(expectedModel1.GenericQuarter);
            actualModel1.GenericYear.Should().Be(expectedModel1.GenericYear);

            actualModel2.UnitOfTime.Should().Be(expectedModel2.UnitOfTime);
            actualModel2.CalendarUnitOfTime.Should().Be(expectedModel2.CalendarUnitOfTime);
            actualModel2.CalendarDay.Should().Be(expectedModel2.CalendarDay);
            actualModel2.CalendarMonth.Should().Be(expectedModel2.CalendarMonth);
            actualModel2.CalendarQuarter.Should().Be(expectedModel2.CalendarQuarter);
            actualModel2.CalendarYear.Should().Be(expectedModel2.CalendarYear);
            actualModel2.FiscalUnitOfTime.Should().Be(expectedModel2.FiscalUnitOfTime);
            actualModel2.FiscalMonth.Should().Be(expectedModel2.FiscalMonth);
            actualModel2.FiscalQuarter.Should().Be(expectedModel2.FiscalQuarter);
            actualModel2.FiscalYear.Should().Be(expectedModel2.FiscalYear);
            actualModel2.GenericUnitOfTime.Should().Be(expectedModel2.GenericUnitOfTime);
            actualModel2.GenericMonth.Should().Be(expectedModel2.GenericMonth);
            actualModel2.GenericQuarter.Should().Be(expectedModel2.GenericQuarter);
            actualModel2.GenericYear.Should().Be(expectedModel2.GenericYear);
        }

        [Fact]
        public static async Task ReportingPeriodModel_with_nulls___Should_roundtrip_to_Mongo_and_back___When_using_custom_IReportingPeriod_serializers()
        {
            // Arrange
            var collection1 = Database.GetCollection<ReportingPeriodModel>(nameof(ReportingPeriodModel));
            var collection2 = Database.GetCollection<IReportingPeriodModel>(nameof(IReportingPeriodModel));

            var expectedModel1 = new ReportingPeriodModel();
            var expectedModel2 = new IReportingPeriodModel();

            // Act
            collection1.InsertOne(expectedModel1);
            collection2.InsertOne(expectedModel2);

            var actualModel1 = (await collection1.Find(_ => _.Id == expectedModel1.Id).ToListAsync()).Single();
            var actualModel2 = (await collection2.Find(_ => _.Id == expectedModel2.Id).ToListAsync()).Single();

            // Assert
            actualModel1.UnitOfTime.Should().BeNull();
            actualModel1.CalendarUnitOfTime.Should().BeNull();
            actualModel1.CalendarDay.Should().BeNull();
            actualModel1.CalendarMonth.Should().BeNull();
            actualModel1.CalendarQuarter.Should().BeNull();
            actualModel1.CalendarYear.Should().BeNull();
            actualModel1.FiscalUnitOfTime.Should().BeNull();
            actualModel1.FiscalMonth.Should().BeNull();
            actualModel1.FiscalQuarter.Should().BeNull();
            actualModel1.FiscalYear.Should().BeNull();
            actualModel1.GenericUnitOfTime.Should().BeNull();
            actualModel1.GenericMonth.Should().BeNull();
            actualModel1.GenericQuarter.Should().BeNull();
            actualModel1.GenericYear.Should().BeNull();

            actualModel2.UnitOfTime.Should().BeNull();
            actualModel2.CalendarUnitOfTime.Should().BeNull();
            actualModel2.CalendarDay.Should().BeNull();
            actualModel2.CalendarMonth.Should().BeNull();
            actualModel2.CalendarQuarter.Should().BeNull();
            actualModel2.CalendarYear.Should().BeNull();
            actualModel2.FiscalUnitOfTime.Should().BeNull();
            actualModel2.FiscalMonth.Should().BeNull();
            actualModel2.FiscalQuarter.Should().BeNull();
            actualModel2.FiscalYear.Should().BeNull();
            actualModel2.GenericUnitOfTime.Should().BeNull();
            actualModel2.GenericMonth.Should().BeNull();
            actualModel2.GenericQuarter.Should().BeNull();
            actualModel2.GenericYear.Should().BeNull();
        }

        [Fact]
        public static void UnitOfTimeModel_without_nulls___Should_serialize_to_string_representation_of_ReportingPeriod___When_using_custom_UnitOfTime_serializers()
        {
            // Arrange
            var model1 = A.Dummy<ReportingPeriodModel>();
            var model2 = A.Dummy<IReportingPeriodModel>();

            var expectedJson1 =
                "\"" + nameof(model1.UnitOfTime) + "\" : \"" + model1.UnitOfTime.SerializeToString() + "\", " +
                "\"" + nameof(model1.CalendarUnitOfTime) + "\" : \"" + model1.CalendarUnitOfTime.SerializeToString() + "\", " +
                "\"" + nameof(model1.CalendarDay) + "\" : \"" + model1.CalendarDay.SerializeToString() + "\", " +
                "\"" + nameof(model1.CalendarMonth) + "\" : \"" + model1.CalendarMonth.SerializeToString() + "\", " +
                "\"" + nameof(model1.CalendarQuarter) + "\" : \"" + model1.CalendarQuarter.SerializeToString() + "\", " +
                "\"" + nameof(model1.CalendarYear) + "\" : \"" + model1.CalendarYear.SerializeToString() + "\", " +
                "\"" + nameof(model1.FiscalUnitOfTime) + "\" : \"" + model1.FiscalUnitOfTime.SerializeToString() + "\", " +
                "\"" + nameof(model1.FiscalMonth) + "\" : \"" + model1.FiscalMonth.SerializeToString() + "\", " +
                "\"" + nameof(model1.FiscalQuarter) + "\" : \"" + model1.FiscalQuarter.SerializeToString() + "\", " +
                "\"" + nameof(model1.FiscalYear) + "\" : \"" + model1.FiscalYear.SerializeToString() + "\", " +
                "\"" + nameof(model1.GenericUnitOfTime) + "\" : \"" + model1.GenericUnitOfTime.SerializeToString() + "\", " +
                "\"" + nameof(model1.GenericMonth) + "\" : \"" + model1.GenericMonth.SerializeToString() + "\", " +
                "\"" + nameof(model1.GenericQuarter) + "\" : \"" + model1.GenericQuarter.SerializeToString() + "\", " +
                "\"" + nameof(model1.GenericYear) + "\" : \"" + model1.GenericYear.SerializeToString() + "\"";

            var expectedJson2 =
                "\"" + nameof(model2.UnitOfTime) + "\" : \"" + model2.UnitOfTime.SerializeToString() + "\", " +
                "\"" + nameof(model2.CalendarUnitOfTime) + "\" : \"" + model2.CalendarUnitOfTime.SerializeToString() + "\", " +
                "\"" + nameof(model2.CalendarDay) + "\" : \"" + model2.CalendarDay.SerializeToString() + "\", " +
                "\"" + nameof(model2.CalendarMonth) + "\" : \"" + model2.CalendarMonth.SerializeToString() + "\", " +
                "\"" + nameof(model2.CalendarQuarter) + "\" : \"" + model2.CalendarQuarter.SerializeToString() + "\", " +
                "\"" + nameof(model2.CalendarYear) + "\" : \"" + model2.CalendarYear.SerializeToString() + "\", " +
                "\"" + nameof(model2.FiscalUnitOfTime) + "\" : \"" + model2.FiscalUnitOfTime.SerializeToString() + "\", " +
                "\"" + nameof(model2.FiscalMonth) + "\" : \"" + model2.FiscalMonth.SerializeToString() + "\", " +
                "\"" + nameof(model2.FiscalQuarter) + "\" : \"" + model2.FiscalQuarter.SerializeToString() + "\", " +
                "\"" + nameof(model2.FiscalYear) + "\" : \"" + model2.FiscalYear.SerializeToString() + "\", " +
                "\"" + nameof(model2.GenericUnitOfTime) + "\" : \"" + model2.GenericUnitOfTime.SerializeToString() + "\", " +
                "\"" + nameof(model2.GenericMonth) + "\" : \"" + model2.GenericMonth.SerializeToString() + "\", " +
                "\"" + nameof(model2.GenericQuarter) + "\" : \"" + model2.GenericQuarter.SerializeToString() + "\", " +
                "\"" + nameof(model2.GenericYear) + "\" : \"" + model2.GenericYear.SerializeToString() + "\"";

            // Act
            var actualJson1 = model1.ToJson();
            var actualJson2 = model2.ToJson();

            // Assert
            actualJson1.Should().Contain(expectedJson1);
            actualJson2.Should().Contain(expectedJson2);
        }

        private class UnitOfTimeModel
        {
            // ReSharper disable UnusedAutoPropertyAccessor.Local
            // ReSharper disable MemberCanBePrivate.Local
            public Guid Id { get; set; }

            public UnitOfTime UnitOfTime { get; set; }

            public CalendarUnitOfTime CalendarUnitOfTime { get; set; }

            public CalendarDay CalendarDay { get; set; }

            public CalendarMonth CalendarMonth { get; set; }

            public CalendarQuarter CalendarQuarter { get; set; }

            public CalendarYear CalendarYear { get; set; }

            public FiscalUnitOfTime FiscalUnitOfTime { get; set; }

            public FiscalMonth FiscalMonth { get; set; }

            public FiscalQuarter FiscalQuarter { get; set; }

            public FiscalYear FiscalYear { get; set; }

            public GenericUnitOfTime GenericUnitOfTime { get; set; }

            public GenericMonth GenericMonth { get; set; }

            public GenericQuarter GenericQuarter { get; set; }

            public GenericYear GenericYear { get; set; }

            // ReSharper restore MemberCanBePrivate.Local
            // ReSharper restore UnusedAutoPropertyAccessor.Local
        }

        private class ReportingPeriodModel
        {
            // ReSharper disable UnusedAutoPropertyAccessor.Local
            // ReSharper disable MemberCanBePrivate.Local
            public Guid Id { get; set; }

            public ReportingPeriod<UnitOfTime> UnitOfTime { get; set; }

            public ReportingPeriod<CalendarUnitOfTime> CalendarUnitOfTime { get; set; }

            public ReportingPeriod<CalendarDay> CalendarDay { get; set; }

            public ReportingPeriod<CalendarMonth> CalendarMonth { get; set; }

            public ReportingPeriod<CalendarQuarter> CalendarQuarter { get; set; }

            public ReportingPeriod<CalendarYear> CalendarYear { get; set; }

            public ReportingPeriod<FiscalUnitOfTime> FiscalUnitOfTime { get; set; }

            public ReportingPeriod<FiscalMonth> FiscalMonth { get; set; }

            public ReportingPeriod<FiscalQuarter> FiscalQuarter { get; set; }

            public ReportingPeriod<FiscalYear> FiscalYear { get; set; }

            public ReportingPeriod<GenericUnitOfTime> GenericUnitOfTime { get; set; }

            public ReportingPeriod<GenericMonth> GenericMonth { get; set; }

            public ReportingPeriod<GenericQuarter> GenericQuarter { get; set; }

            public ReportingPeriod<GenericYear> GenericYear { get; set; }

            // ReSharper restore MemberCanBePrivate.Local
            // ReSharper restore UnusedAutoPropertyAccessor.Local
        }

        // ReSharper disable InconsistentNaming
        private class IReportingPeriodModel
        // ReSharper restore InconsistentNaming
        {
            // ReSharper disable UnusedAutoPropertyAccessor.Local
            // ReSharper disable MemberCanBePrivate.Local
            public Guid Id { get; set; }

            public IReportingPeriod<UnitOfTime> UnitOfTime { get; set; }

            public IReportingPeriod<CalendarUnitOfTime> CalendarUnitOfTime { get; set; }

            public IReportingPeriod<CalendarDay> CalendarDay { get; set; }

            public IReportingPeriod<CalendarMonth> CalendarMonth { get; set; }

            public IReportingPeriod<CalendarQuarter> CalendarQuarter { get; set; }

            public IReportingPeriod<CalendarYear> CalendarYear { get; set; }

            public IReportingPeriod<FiscalUnitOfTime> FiscalUnitOfTime { get; set; }

            public IReportingPeriod<FiscalMonth> FiscalMonth { get; set; }

            public IReportingPeriod<FiscalQuarter> FiscalQuarter { get; set; }

            public IReportingPeriod<FiscalYear> FiscalYear { get; set; }

            public IReportingPeriod<GenericUnitOfTime> GenericUnitOfTime { get; set; }

            public IReportingPeriod<GenericMonth> GenericMonth { get; set; }

            public IReportingPeriod<GenericQuarter> GenericQuarter { get; set; }

            public IReportingPeriod<GenericYear> GenericYear { get; set; }

            // ReSharper restore MemberCanBePrivate.Local
            // ReSharper restore UnusedAutoPropertyAccessor.Local
        }
    }
}
