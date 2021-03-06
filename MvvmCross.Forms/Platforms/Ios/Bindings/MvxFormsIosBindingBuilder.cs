// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Forms.Bindings;
using MvvmCross.IoC;
using MvvmCross.Platforms.Ios.Binding;

namespace MvvmCross.Forms.Platforms.Ios.Bindings
{
    public class MvxFormsIosBindingBuilder : MvxIosBindingBuilder
    {
        public MvxFormsIosBindingBuilder()
        {
        }

        public override void DoRegistration(IMvxIoCProvider iocProvider)
        {
            base.DoRegistration(iocProvider);
            InitializeBindingCreator();
        }

        protected override IMvxTargetBindingFactoryRegistry CreateTargetBindingRegistry()
        {
            return new MvxFormsTargetBindingFactoryRegistry();
        }

        private void InitializeBindingCreator()
        {
            var creator = CreateBindingCreator();
            Mvx.IoCProvider.RegisterSingleton(creator);
        }

        protected virtual IMvxBindingCreator CreateBindingCreator()
        {
            return new MvxFormsBindingCreator();
        }
    }
}
