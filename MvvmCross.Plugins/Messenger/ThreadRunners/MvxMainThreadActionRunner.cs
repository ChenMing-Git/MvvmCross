// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using System;
using Microsoft.Extensions.Logging;
using MvvmCross.Base;

namespace MvvmCross.Plugin.Messenger.ThreadRunners
{
    public class MvxMainThreadActionRunner
        : IMvxActionRunner
    {
        public void Run(Action action)
        {
            var dispatcher = MvxMainThreadDispatcher.Instance;
            if (dispatcher == null)
            {
                MvxPluginLog.Instance?.Log(LogLevel.Warning, "Not able to deliver message - no ui thread dispatcher available");
                return;
            }
            dispatcher.RequestMainThreadAction(action);
        }
    }
}
