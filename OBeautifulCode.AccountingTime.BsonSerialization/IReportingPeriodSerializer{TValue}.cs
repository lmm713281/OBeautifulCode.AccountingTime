﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReportingPeriodSerializer{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.AccountingTime.BsonSerialization source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.BsonSerialization
{
    using System;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.Serializers;

    using OBeautifulCode.AccountingTime;

    using static System.FormattableString;

    /// <inheritdoc />
#if !OBeautifulCodeAccountingTimeBsonSerializationRecipesProject
    [System.Diagnostics.DebuggerStepThrough]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.AccountingTime.BsonSerialization", "See package version number")]
#endif
    // ReSharper disable InconsistentNaming
    public class IReportingPeriodSerializer<TValue> : SerializerBase<TValue>
    // ReSharper restore InconsistentNaming
        where TValue : class, IReportingPeriod<UnitOfTime>
    {
        /// <inheritdoc />
        public override TValue Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var type = context.Reader.GetCurrentBsonType();
            switch (type)
            {
                case BsonType.String:
                    return context.Reader.ReadString().DeserializeFromString<TValue>();
                case BsonType.Null:
                    context.Reader.ReadNull();
                    return default(TValue);
                default:
                    throw new NotSupportedException(Invariant($"Cannot convert a {type} to a {typeof(TValue).Name}."));
            }
        }

        /// <inheritdoc />
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TValue value)
        {
            if (value == null)
            {
                context.Writer.WriteNull();
            }
            else
            {
                context.Writer.WriteString(value.SerializeToString());
            }
        }
    }
}
