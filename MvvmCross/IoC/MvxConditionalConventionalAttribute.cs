// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using System;

namespace MvvmCross.IoC
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public abstract class MvxConditionalConventionalAttribute : Attribute
    {
        public abstract bool IsConditionSatisfied { get; }
    }
}
