// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace MvvmCross.Core
{
#nullable enable
    public interface IMvxFillableStringToTypeParser
    {
        IDictionary<Type, MvxStringToTypeParser.IParser> TypeParsers { get; }
        IList<MvxStringToTypeParser.IExtraParser> ExtraParsers { get; }
    }
#nullable restore
}
