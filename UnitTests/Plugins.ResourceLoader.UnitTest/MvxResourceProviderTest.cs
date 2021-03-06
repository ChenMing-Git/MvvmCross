// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using MvvmCross.Plugin.ResourceLoader;
using Xunit;

namespace MvvmCross.Plugin.ResourceLoader.UnitTest
{
    public class MvxResourceProviderTest
    {
        private class MvxResourceProviderStub : MvxResourceProvider
        {
            public string GetResourceNameForPath(string path)
            {
                return GenerateResourceNameFromPath(path);
            }
        }

        [Fact]
        public void ICanGenerateResourceNameWithNumbers()
        {
            MvxResourceProviderStub stub = new MvxResourceProviderStub();
            string numberPath = stub.GetResourceNameForPath("123");

            Assert.Equal("_123", numberPath);
        }

        [Fact]
        public void ICanGenerateResourceNameWithDots()
        {
            MvxResourceProviderStub stub = new MvxResourceProviderStub();
            string numberPath = stub.GetResourceNameForPath("abc.def");

            Assert.Equal("abc.def", numberPath);
        }

        [Fact]
        public void ICanGenerateResourceNameWithSlashes()
        {
            MvxResourceProviderStub stub = new MvxResourceProviderStub();
            string numberPath = stub.GetResourceNameForPath("abc/def");

            Assert.Equal("abc.def", numberPath);
        }

        [Fact]
        public void ICanGenerateResourceNameWithVersionNumbers()
        {
            MvxResourceProviderStub stub = new MvxResourceProviderStub();
            string numberPath = stub.GetResourceNameForPath("abc1.2.3");

            Assert.Equal("abc1._2._3", numberPath);
        }

        [Fact]
        public void ICanGenerateResourceNameWithNumberFolders()
        {
            MvxResourceProviderStub stub = new MvxResourceProviderStub();
            string numberPath = stub.GetResourceNameForPath("abc1/2/3");

            Assert.Equal("abc1._2._3", numberPath);
        }

        [Fact]
        public void ICanGenerateResourceNameWithInvalidCharacters()
        {
            MvxResourceProviderStub stub = new MvxResourceProviderStub();
            string numberPath = stub.GetResourceNameForPath("abc1-2~3");

            Assert.Equal("abc1_2_3", numberPath);
        }

        [Fact]
        public void ICanGenerateResourceNameWithDoubleSlashes()
        {
            MvxResourceProviderStub stub = new MvxResourceProviderStub();
            string numberPath = stub.GetResourceNameForPath("abc//def");

            Assert.Equal("abc.def", numberPath);
        }
    }
}
