﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiscalUnitOfTime.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;

    /// <summary>
    /// Represents a unit of time in the context of some company's fiscal year.
    /// </summary>
    [Serializable]
    public abstract class FiscalUnitOfTime : UnitOfTime
    {
    }
}

// ReSharper restore CheckNamespace