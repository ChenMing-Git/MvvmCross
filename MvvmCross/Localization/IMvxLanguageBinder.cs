// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

namespace MvvmCross.Localization
{
#nullable enable
    public interface IMvxLanguageBinder
    {
        string? GetText(string entryKey);

        string? GetText(string entryKey, params object[] args);
    }
#nullable restore
}
