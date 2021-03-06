// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using System;
using System.Windows.Input;
using Foundation;
using Microsoft.Extensions.Logging;
using MvvmCross.Base;
using MvvmCross.Logging;
using UIKit;

namespace MvvmCross.Platforms.Tvos.Binding.Views
{
    public abstract class MvxBaseCollectionViewSource : UICollectionViewSource
    {
        public static readonly NSString UnknownCellIdentifier = null;

        private readonly NSString _cellIdentifier;
        private readonly UICollectionView _collectionView;

        protected virtual NSString DefaultCellIdentifier => _cellIdentifier;

        protected MvxBaseCollectionViewSource(UICollectionView collectionView)
            : this(collectionView, UnknownCellIdentifier)
        {
        }

        protected MvxBaseCollectionViewSource(UICollectionView collectionView,
                                              NSString cellIdentifier)
        {
            _collectionView = collectionView;
            _cellIdentifier = cellIdentifier;
        }

        protected UICollectionView CollectionView => _collectionView;

        public ICommand SelectionChangedCommand { get; set; }

        public virtual void ReloadData()
        {
            try
            {
                _collectionView.ReloadData();
            }
            catch (Exception exception)
            {
                MvxLogHost.GetLog<MvxBaseCollectionViewSource>()?.Log(
                    LogLevel.Warning, exception, "Exception masked during CollectionView ReloadData");
            }
        }

        protected virtual UICollectionViewCell GetOrCreateCellFor(UICollectionView collectionView, NSIndexPath indexPath,
                                                                  object item)
        {
            return (UICollectionViewCell)collectionView.DequeueReusableCell(DefaultCellIdentifier, indexPath);
        }

        protected abstract object GetItemAt(NSIndexPath indexPath);

        public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var item = GetItemAt(indexPath);

            var command = SelectionChangedCommand;
            if (command != null && command.CanExecute(item))
                command.Execute(item);

            SelectedItem = item;
        }

        private object _selectedItem;

        public object SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                // note that we only expect this to be called from the control/Table
                // we don't have any multi-select or any scroll into view functionality here
                _selectedItem = value;
                var handler = SelectedItemChanged;
                handler?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler SelectedItemChanged;

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var item = GetItemAt(indexPath);
            var cell = GetOrCreateCellFor(collectionView, indexPath, item);

            var bindable = cell as IMvxDataConsumer;
            if (bindable != null)
                bindable.DataContext = item;

            return cell;
        }

        public override void CellDisplayingEnded(UICollectionView collectionView, UICollectionViewCell cell, NSIndexPath indexPath)
        {
            //Don't bind to NULL to speed up cells in lists when fast scrolling
            //There should be almost no scenario in which this is required
            //If it is required, do this in your own subclass using this code:

            //var bindable = cell as IMvxDataConsumer;
            //if (bindable != null)
            //    bindable.DataContext = null;
        }

        public override nint NumberOfSections(UICollectionView collectionView)
        {
            return 1;
        }
    }
}
