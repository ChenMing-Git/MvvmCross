// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using MvvmCross.Base;
using MvvmCross.ViewModels;

namespace MvvmCross.Views
{
#nullable enable
    public interface IMvxViewDispatcher : IMvxMainThreadAsyncDispatcher, IMvxMainThreadDispatcher
    {
        Task<bool> ShowViewModel(MvxViewModelRequest request);

        Task<bool> ChangePresentation(MvxPresentationHint hint);
    }
#nullable restore
}
