// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using MvvmCross.Binding.Bindings.Source.Chained;
using MvvmCross.Binding.Bindings.Source.Leaf;
using MvvmCross.Binding.Parse.PropertyPath.PropertyTokens;
using MvvmCross.Exceptions;

namespace MvvmCross.Binding.Bindings.Source.Construction
{
    /// <summary>
    /// Uses a global cache of calls in Reflection namespace
    /// </summary>
    public class MvxPropertySourceBindingFactoryExtension
        : IMvxSourceBindingFactoryExtension
    {
        private static readonly ConcurrentDictionary<int, PropertyInfo> PropertyInfoCache = new ConcurrentDictionary<int, PropertyInfo>();

        public bool TryCreateBinding(object source, MvxPropertyToken currentToken, List<MvxPropertyToken> remainingTokens, out IMvxSourceBinding result)
        {
            if (source == null)
            {
                result = null;
                return false;
            }

            result = remainingTokens.Count == 0 ? CreateLeafBinding(source, currentToken) : CreateChainedBinding(source, currentToken, remainingTokens);
            return result != null;
        }

        protected virtual MvxChainedSourceBinding CreateChainedBinding(object source, MvxPropertyToken propertyToken,
                                                                       List<MvxPropertyToken> remainingTokens)
        {
            var indexPropertyToken = propertyToken as MvxIndexerPropertyToken;
            if (indexPropertyToken != null)
            {
                var itemPropertyInfo = FindPropertyInfo(source);
                if (itemPropertyInfo == null)
                    return null;

                return new MvxIndexerChainedSourceBinding(source, itemPropertyInfo, indexPropertyToken,
                                                          remainingTokens);
            }

            var propertyNameToken = propertyToken as MvxPropertyNamePropertyToken;
            if (propertyNameToken != null)
            {
                var propertyInfo = FindPropertyInfo(source, propertyNameToken.PropertyName);

                if (propertyInfo == null)
                    return null;

                return new MvxSimpleChainedSourceBinding(source, propertyInfo,
                                                         remainingTokens);
            }

            throw new MvxException("Unexpected property chaining - seen token type {0}",
                                   propertyToken.GetType().FullName);
        }

        protected virtual IMvxSourceBinding CreateLeafBinding(object source, MvxPropertyToken propertyToken)
        {
            var indexPropertyToken = propertyToken as MvxIndexerPropertyToken;
            if (indexPropertyToken != null)
            {
                var itemPropertyInfo = FindPropertyInfo(source);
                if (itemPropertyInfo == null)
                    return null;
                return new MvxIndexerLeafPropertyInfoSourceBinding(source, itemPropertyInfo, indexPropertyToken);
            }

            var propertyNameToken = propertyToken as MvxPropertyNamePropertyToken;
            if (propertyNameToken != null)
            {
                var propertyInfo = FindPropertyInfo(source, propertyNameToken.PropertyName);
                if (propertyInfo == null)
                    return null;
                return new MvxSimpleLeafPropertyInfoSourceBinding(source, propertyInfo);
            }

            if (propertyToken is MvxEmptyPropertyToken)
            {
                return new MvxDirectToSourceBinding(source);
            }

            throw new MvxException("Unexpected property source - seen token type {0}", propertyToken.GetType().FullName);
        }

        protected PropertyInfo FindPropertyInfo(object source, string propertyName = "Item")
        {
            var sourceType = source.GetType();
            var key = (sourceType.FullName + "." + propertyName).GetHashCode();

            PropertyInfo pi;
            if (PropertyInfoCache.TryGetValue(key, out pi))
                return pi;

            //Get lowest property
            while (sourceType != null)
            {
                //Use BindingFlags.DeclaredOnly to avoid AmbiguousMatchException
                pi = sourceType.GetProperty(propertyName, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
                if (pi != null)
                {
                    break;
                }
                sourceType = sourceType.BaseType;
            }

            PropertyInfoCache.TryAdd(key, pi);
            return pi;
        }
    }
}
