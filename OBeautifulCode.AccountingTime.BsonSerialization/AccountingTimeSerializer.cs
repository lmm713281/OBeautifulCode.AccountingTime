﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimeSerializer.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.AccountingTime.BsonSerialization source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.BsonSerialization
{
    using System;
    using System.Linq;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;

    /// <summary>
    /// A serializer for Accounting Time types.
    /// </summary>
#if !OBeautifulCodeAccountingTimeBsonSerializationRecipesProject
    [System.Diagnostics.DebuggerStepThrough]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.AccountingTime.BsonSerialization", "See package version number")]
#endif
    internal static class AccountingTimeSerializer
    {
        /// <summary>
        /// Registers this serializer.
        /// </summary>
        public static void Register()
        {
            // register various flavors of UnitOfTime
            var unitOfTimeType = typeof(UnitOfTime);
            var unitOfTimeTypesToRegister =
                unitOfTimeType.Assembly.GetTypes()
                    .Where(type => (type == unitOfTimeType) || type.IsSubclassOf(unitOfTimeType))
                    .ToList();
            unitOfTimeTypesToRegister.ForEach(
                t =>
                {
                    try
                    {
                        BsonSerializer.RegisterSerializer(
                            t, 
                            Activator.CreateInstance(typeof(UnitOfTimeSerializer<>).MakeGenericType(t)) as IBsonSerializer);
                    }
                    catch (BsonSerializationException)
                    {
                        // get rid of this nonsense when this method is available:
                        // https://github.com/mongodb/mongo-csharp-driver/pull/264
                    }
                });

            // register various flavors of IReportingPeriod
            var reportingPeriodType = typeof(IReportingPeriod<>);
            var reportingPeriodTypesToRegister =
                reportingPeriodType.Assembly.GetTypes()
                    .Where(type =>
                        (type == reportingPeriodType) ||
                        type.GetInterfaces()
                            .Where(_ => _.IsGenericType)
                            .Select(_ => _.GetGenericTypeDefinition())
                            .Contains(reportingPeriodType))
                    .ToList();

            foreach (var reportingPeriodTypeToRegister in reportingPeriodTypesToRegister)
            {
                unitOfTimeTypesToRegister.ForEach(
                    t =>
                    {
                        try
                        {
                            BsonSerializer.RegisterSerializer(
                                reportingPeriodTypeToRegister.MakeGenericType(t),
                                Activator.CreateInstance(typeof(IReportingPeriodSerializer<>).MakeGenericType(reportingPeriodTypeToRegister.MakeGenericType(t))) as IBsonSerializer);
                        }
                        catch (BsonSerializationException)
                        {
                            // get rid of this nonsense when this method is available:
                            // https://github.com/mongodb/mongo-csharp-driver/pull/264
                        }
                    });
            }
        }
    }
}
