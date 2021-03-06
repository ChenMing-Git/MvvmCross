// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Windows.Input;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Attributes;

namespace MvvmCross.Platforms.Android.Binding.Views
{
    [Register("mvvmcross.platforms.android.binding.views.MvxGridView")]
    public class MvxGridView
        : GridView
    {
        private ICommand _itemClick;
        private ICommand _itemLongClick;

        private bool _itemClickOverloaded;
        private bool _itemLongClickOverloaded;

        public MvxGridView(Context context, IAttributeSet attrs)
            : this(context, attrs, new MvxAdapter(context))
        {
        }

        public MvxGridView(Context context, IAttributeSet attrs, IMvxAdapter adapter)
            : base(context, attrs)
        {
            // Note: Any calling derived class passing a null adapter is responsible for setting
            // it's own itemTemplateId
            if (adapter == null)
                return;

            var itemTemplateId = MvxAttributeHelpers.ReadListItemTemplateId(context, attrs);
            adapter.ItemTemplateId = itemTemplateId;
            Adapter = adapter;
        }

        protected MvxGridView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public new IMvxAdapter Adapter
        {
            get
            {
                return base.Adapter as IMvxAdapter;
            }
            set
            {
                var existing = Adapter;
                if (existing == value)
                    return;

                if (existing != null && value != null)
                {
                    value.ItemsSource = existing.ItemsSource;
                    value.ItemTemplateId = existing.ItemTemplateId;
                }

                base.Adapter = value;

                if (existing != null)
                    existing.ItemsSource = null;
            }
        }

        [MvxSetToNullAfterBinding]
        public IEnumerable ItemsSource
        {
            get { return Adapter.ItemsSource; }
            set { Adapter.ItemsSource = value; }
        }

        public int ItemTemplateId
        {
            get { return Adapter.ItemTemplateId; }
            set { Adapter.ItemTemplateId = value; }
        }

        public new ICommand ItemClick
        {
            get
            {
                return _itemClick;
            }
            set
            {
                _itemClick = value;
                if (_itemClick != null)
                    EnsureItemClickOverloaded();
            }
        }

        private void EnsureItemClickOverloaded()
        {
            if (_itemClickOverloaded)
                return;

            _itemClickOverloaded = true;
            base.ItemClick += ItemOnClick;
        }

        private void ItemOnClick(object sender, ItemClickEventArgs e)
        {
            ExecuteCommandOnItem(ItemClick, e.Position);
        }

        public new ICommand ItemLongClick
        {
            get
            {
                return _itemLongClick;
            }
            set
            {
                _itemLongClick = value;
                if (_itemLongClick != null)
                    EnsureItemLongClickOverloaded();
            }
        }

        private void EnsureItemLongClickOverloaded()
        {
            if (_itemLongClickOverloaded)
                return;

            _itemLongClickOverloaded = true;
            base.ItemLongClick += ItemOnLongClick;
        }

        private void ItemOnLongClick(object sender, ItemLongClickEventArgs e)
        {
            ExecuteCommandOnItem(ItemLongClick, e.Position);
        }

        protected virtual void ExecuteCommandOnItem(ICommand command, int position)
        {
            if (command == null)
                return;

            var item = Adapter.GetRawItem(position);
            if (item == null)
                return;

            if (!command.CanExecute(item))
                return;

            command.Execute(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                base.ItemClick -= ItemOnClick;
                base.ItemLongClick -= ItemOnLongClick;
            }

            base.Dispose(disposing);
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            if (NestedScrollingEnabled)
                base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            else
            {
                //expand the view to the full height of it's contents to disable scrolling
                var expandSpec = MeasureSpec.MakeMeasureSpec(MeasuredSizeMask, MeasureSpecMode.AtMost);
                base.OnMeasure(widthMeasureSpec, expandSpec);
                LayoutParameters.Height = MeasuredHeight;
            }
        }
    }
}
