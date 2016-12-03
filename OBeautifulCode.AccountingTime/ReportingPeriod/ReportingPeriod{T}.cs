﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriod{T}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <inheritdoc />
    [Serializable]
    public abstract class ReportingPeriod<T> : IReportingPeriod<T>
        where T : UnitOfTime
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportingPeriod{T}"/> class.
        /// </summary>
        /// <param name="start">The start of the reporting period.</param>
        /// <param name="end">The end of the reporting period.</param>
        /// <exception cref="ArgumentNullException"><paramref name="start"/> or <paramref name="end"/> are null.</exception>
        /// <exception cref="ArgumentException"><paramref name="start"/> and <paramref name="end"/> are different kinds of units-of-time.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/> is greater than <paramref name="end"/>.</exception>
        protected ReportingPeriod(T start, T end)
        {
            if (start == null)
            {
                throw new ArgumentNullException(nameof(start));
            }

            if (end == null)
            {
                throw new ArgumentNullException(nameof(end));
            }

            if (start.GetType() != end.GetType())
            {
                throw new ArgumentException("start and end are different kinds of units-of-time");
            }

            if (start.CompareTo(end) == 1)
            {
                throw new ArgumentOutOfRangeException(nameof(start), "start is greater than end");
            }

            this.Start = start;
            this.End = end;
        }

        // ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

        /// <inheritdoc />
        public T Start { get; private set; }

        /// <inheritdoc />
        public T End { get; private set; }

        /// <inheritdoc />
        public TReportingPeriod Clone<TReportingPeriod>()
            where TReportingPeriod : class, IReportingPeriod<UnitOfTime>
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public abstract IReportingPeriod<T> Clone();

        // ReSharper restore AutoPropertyCanBeMadeGetOnly.Local
    }
}

// ReSharper restore CheckNamespace