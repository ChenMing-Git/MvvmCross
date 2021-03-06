// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using System;
using Android.App;
using Android.Content;
using Android.OS;
using MvvmCross.Base;

namespace MvvmCross.Platforms.Android.Views.Base
{
    public abstract class MvxBaseActivityAdapter
    {
        private readonly IMvxEventSourceActivity _eventSource;

        protected Activity Activity => _eventSource as Activity;

        protected MvxBaseActivityAdapter(IMvxEventSourceActivity eventSource)
        {
            _eventSource = eventSource;

            _eventSource.CreateCalled += EventSourceOnCreateCalled;
            _eventSource.CreateWillBeCalled += EventSourceOnCreateWillBeCalled;
            _eventSource.StartCalled += EventSourceOnStartCalled;
            _eventSource.RestartCalled += EventSourceOnRestartCalled;
            _eventSource.ResumeCalled += EventSourceOnResumeCalled;
            _eventSource.PauseCalled += EventSourceOnPauseCalled;
            _eventSource.StopCalled += EventSourceOnStopCalled;
            _eventSource.DestroyCalled += EventSourceOnDestroyCalled;
            _eventSource.DisposeCalled += EventSourceOnDisposeCalled;
            _eventSource.SaveInstanceStateCalled += EventSourceOnSaveInstanceStateCalled;
            _eventSource.NewIntentCalled += EventSourceOnNewIntentCalled;

            _eventSource.ActivityResultCalled += EventSourceOnActivityResultCalled;
            _eventSource.StartActivityForResultCalled += EventSourceOnStartActivityForResultCalled;
        }

        protected virtual void EventSourceOnSaveInstanceStateCalled(
            object sender, MvxValueEventArgs<Bundle> eventArgs)
        {
        }

        protected virtual void EventSourceOnCreateWillBeCalled(
            object sender, MvxValueEventArgs<Bundle> eventArgs)
        {
        }

        protected virtual void EventSourceOnStopCalled(object sender, EventArgs eventArgs)
        {
        }

        protected virtual void EventSourceOnStartCalled(object sender, EventArgs eventArgs)
        {
        }

        protected virtual void EventSourceOnStartActivityForResultCalled(
            object sender, MvxValueEventArgs<MvxStartActivityForResultParameters> eventArgs)
        {
        }

        protected virtual void EventSourceOnResumeCalled(object sender, EventArgs eventArgs)
        {
        }

        protected virtual void EventSourceOnRestartCalled(object sender, EventArgs eventArgs)
        {
        }

        protected virtual void EventSourceOnPauseCalled(object sender, EventArgs eventArgs)
        {
        }

        protected virtual void EventSourceOnNewIntentCalled(object sender, MvxValueEventArgs<Intent> eventArgs)
        {
        }

        protected virtual void EventSourceOnDisposeCalled(object sender, EventArgs eventArgs)
        {
        }

        protected virtual void EventSourceOnDestroyCalled(object sender, EventArgs eventArgs)
        {
        }

        protected virtual void EventSourceOnCreateCalled(object sender, MvxValueEventArgs<Bundle> eventArgs)
        {
        }

        protected virtual void EventSourceOnActivityResultCalled(
            object sender, MvxValueEventArgs<MvxActivityResultParameters> eventArgs)
        {
        }
    }
}
