﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeSerializer{TValue}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Bson
{
    using System;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.Serializers;

    using OBeautifulCode.Validation.Recipes;

    using static System.FormattableString;

    /// <inheritdoc />
    internal class UnitOfTimeSerializer<TValue> : SerializerBase<TValue>
        where TValue : UnitOfTime
    {
        /// <inheritdoc />
        public override TValue Deserialize(
            BsonDeserializationContext context,
            BsonDeserializationArgs args)
        {
            new { context }.Must().NotBeNull();

            var type = context.Reader.GetCurrentBsonType();
            switch (type)
            {
                case BsonType.String:
                    return context.Reader.ReadString().DeserializeFromSortableString<TValue>();
                case BsonType.Null:
                    context.Reader.ReadNull();
                    return default(TValue);
                default:
                    throw new NotSupportedException(Invariant($"Cannot convert a {type} to a {typeof(TValue).Name}."));
            }
        }

        /// <inheritdoc />
        public override void Serialize(
            BsonSerializationContext context,
            BsonSerializationArgs args,
            TValue value)
        {
            new { context }.Must().NotBeNull();

            if (value == null)
            {
                context.Writer.WriteNull();
            }
            else
            {
                context.Writer.WriteString(value.SerializeToSortableString());
            }
        }
    }
}
