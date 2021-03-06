﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriod{T}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
    using System.ComponentModel;

    using OBeautifulCode.Math.Recipes;

    using static System.FormattableString;

    /// <inheritdoc cref="IReportingPeriod{T}" />
    [Serializable]
    [Bindable(true, BindingDirection.TwoWay)]
    public class ReportingPeriod<T> : IReportingPeriod<T>, IEquatable<ReportingPeriod<T>>, IEquatable<IReportingPeriod<UnitOfTime>>
        where T : UnitOfTime
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportingPeriod{T}"/> class.
        /// </summary>
        /// <param name="start">The start of the reporting period.</param>
        /// <param name="end">The end of the reporting period.</param>
        /// <exception cref="ArgumentNullException"><paramref name="start"/> or <paramref name="end"/> are null.</exception>
        /// <exception cref="ArgumentException"><paramref name="start"/> and <paramref name="end"/> are bounded and are diffrent concrete types of units-of-time.</exception>
        /// <exception cref="ArgumentException"><paramref name="start"/> and/or <paramref name="end"/> is unbounded and are different kinds of units-of-time.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/> is greater than <paramref name="end"/>.</exception>
        public ReportingPeriod(
            T start,
            T end)
        {
            if (start == null)
            {
                throw new ArgumentNullException(nameof(start));
            }

            if (end == null)
            {
                throw new ArgumentNullException(nameof(end));
            }

            if ((start is IAmUnboundedTime) || (end is IAmUnboundedTime))
            {
                if (start.UnitOfTimeKind != end.UnitOfTimeKind)
                {
                    throw new ArgumentException("start and/or end is unbounded and are different kinds of units-of-time");
                }
            }
            else
            {
                if (start.GetType() != end.GetType())
                {
                    throw new ArgumentException("start and end are bounded and are different concrete types of units-of-time");
                }

                if (start.CompareTo(end) == 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(start), "start is greater than end");
                }
            }

            this.Start = start;
            this.End = end;
        }

        /// <inheritdoc />
        public T Start { get; }

        /// <inheritdoc />
        public T End { get; }

        /// <summary>
        /// Determines whether two objects of type <see cref="ReportingPeriod{T}" /> are equal.
        /// </summary>
        /// <remarks>
        /// Reporting periods are equal if they have the same start and end unit-of-time.
        /// </remarks>
        /// <param name="left">The first reporting period to compare.</param>
        /// <param name="right">The second reporting period to compare.</param>
        /// <returns>true if the two reporting periods are equal; false otherwise.</returns>
        public static bool operator ==(
            ReportingPeriod<T> left,
            ReportingPeriod<T> right)
            => IsEqual(left, right);

        /// <summary>
        /// Determines whether an object of type <see cref="ReportingPeriod{T}" /> and <see cref="IReportingPeriod{T}" /> are equal.
        /// </summary>
        /// <remarks>
        /// Reporting periods are equal if they have the same start and end unit-of-time.
        /// </remarks>
        /// <param name="left">The first reporting period to compare.</param>
        /// <param name="right">The second reporting period to compare.</param>
        /// <returns>true if the two reporting periods are equal; false otherwise.</returns>
        public static bool operator ==(
            ReportingPeriod<T> left,
            IReportingPeriod<UnitOfTime> right)
            => IsEqual(left, right);

        /// <summary>
        /// Determines whether an object of type <see cref="ReportingPeriod{T}" /> and <see cref="IReportingPeriod{T}" /> are equal.
        /// </summary>
        /// <remarks>
        /// Reporting periods are equal if they have the same start and end unit-of-time.
        /// </remarks>
        /// <param name="left">The first reporting period to compare.</param>
        /// <param name="right">The second reporting period to compare.</param>
        /// <returns>true if the two reporting periods are equal; false otherwise.</returns>
        public static bool operator ==(
            IReportingPeriod<UnitOfTime> left,
            ReportingPeriod<T> right)
            => IsEqual(right, left);

        /// <summary>
        /// Determines whether two objects of type <see cref="ReportingPeriod{T}" /> are not equal.
        /// </summary>
        /// <remarks>
        /// Reporting periods are not equal if they have different start and end unit-of-time.
        /// </remarks>
        /// <param name="left">The first reporting period to compare.</param>
        /// <param name="right">The second reporting period to compare.</param>
        /// <returns>true if the two reporting periods are not equal; false otherwise.</returns>
        public static bool operator !=(
            ReportingPeriod<T> left,
            ReportingPeriod<T> right)
            => !(left == right);

        /// <summary>
        /// Determines whether an object of type <see cref="ReportingPeriod{T}" /> and <see cref="IReportingPeriod{T}" /> are not equal.
        /// </summary>
        /// <remarks>
        /// Reporting periods are not equal if they have different start and end unit-of-time.
        /// </remarks>
        /// <param name="left">The first reporting period to compare.</param>
        /// <param name="right">The second reporting period to compare.</param>
        /// <returns>true if the two reporting periods are not equal; false otherwise.</returns>
        public static bool operator !=(
            IReportingPeriod<UnitOfTime> left,
            ReportingPeriod<T> right)
            => !(left == right);

        /// <summary>
        /// Determines whether an object of type <see cref="ReportingPeriod{T}" /> and <see cref="IReportingPeriod{T}" /> are not equal.
        /// </summary>
        /// <remarks>
        /// Reporting periods are not equal if they have different start and end unit-of-time.
        /// </remarks>
        /// <param name="left">The first reporting period to compare.</param>
        /// <param name="right">The second reporting period to compare.</param>
        /// <returns>true if the two reporting periods are not equal; false otherwise.</returns>
        public static bool operator !=(
            ReportingPeriod<T> left,
            IReportingPeriod<UnitOfTime> right)
            => !(left == right);

        /// <inheritdoc />
        public bool Equals(
            ReportingPeriod<T> other)
            => this == other;

        /// <inheritdoc />
        public bool Equals(
            IReportingPeriod<UnitOfTime> other)
            => this == other;

        /// <summary>
        /// Determines whether the specified object is equal to this one, as per <see cref="Equals(ReportingPeriod{T})"/>.
        /// </summary>
        /// <param name="obj">The value to compare this one with.</param>
        /// <returns>true if the other object is a reporting period equal to this one; false otherwise, consistent with <see cref="Equals(ReportingPeriod{T})"/>.</returns>
        public override bool Equals(
            object obj)
            => this == (obj as IReportingPeriod<UnitOfTime>);

        /// <summary>
        /// Returns the hash code for this reporting period.
        /// </summary>
        /// <returns>The hash code for this reporting period.</returns>
        public override int GetHashCode() =>
            HashCodeHelper.Initialize()
                .Hash(this.Start)
                .Hash(this.End)
                .Value;

        /// <summary>
        /// Returns a friendly string representation of this reporting period.
        /// </summary>
        /// <returns>
        /// A friendly string representation of this reporting period.
        /// </returns>
        public override string ToString()
        {
            var result = Invariant($"{this.Start.ToString()} to {this.End.ToString()}");

            return result;
        }

        /// <inheritdoc />
        public TReportingPeriod Clone<TReportingPeriod>()
            where TReportingPeriod : class, IReportingPeriod<UnitOfTime>
        {
            var requestedType = typeof(TReportingPeriod);
            Type requestedUnitOfTimeType = requestedType.GetGenericArguments()[0];

            var thisUnboundGenericType = this.GetType().GetGenericTypeDefinition();
            var typeArgs = new[] { requestedUnitOfTimeType };

            // ReSharper disable once PossibleNullReferenceException
            var genericTypeToCreate = thisUnboundGenericType.MakeGenericType(typeArgs);

            string errorMessage = "A clone of this reporting-period is not assignable to the type of reporting period requested.";
            if (!requestedType.IsAssignableFrom(genericTypeToCreate))
            {
                throw new InvalidOperationException(errorMessage);
            }

            // ReSharper disable once UseMethodIsInstanceOfType
            if (!requestedUnitOfTimeType.IsAssignableFrom(this.Start.GetType()))
            {
                throw new InvalidOperationException(errorMessage);
            }

            // ReSharper disable once UseMethodIsInstanceOfType
            if (!requestedUnitOfTimeType.IsAssignableFrom(this.End.GetType()))
            {
                throw new InvalidOperationException(errorMessage);
            }

            var result = Activator.CreateInstance(genericTypeToCreate, this.Start.Clone(), this.End.Clone());
            return result as TReportingPeriod;
        }

        /// <inheritdoc />
        public IReportingPeriod<T> Clone()
        {
            var startClone = this.Start.Clone<T>();
            var endClone = this.End.Clone<T>();
            var result = new ReportingPeriod<T>(startClone, endClone);
            return result;
        }

        private static bool IsEqual(
            ReportingPeriod<T> left,
            IReportingPeriod<UnitOfTime> right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            if (right.GetType().GetGenericTypeDefinition() != typeof(ReportingPeriod<>))
            {
                return false;
            }

            try
            {
                var result = (left.Start.CompareTo(right.Start) == 0) && (left.End.CompareTo(right.End) == 0);
                return result;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }
}
