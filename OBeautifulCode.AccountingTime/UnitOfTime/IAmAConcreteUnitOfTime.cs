﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAmAConcreteUnitOfTime.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Identifies a <see cref="UnitOfTime"/> that's concrete.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "This interface exists as a marker/way to identify a group of types and should be used at compile-time, not runtime.")]
    public interface IAmAConcreteUnitOfTime
    {
    }
}
