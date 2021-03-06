using CoreGraphics;
using MvvmCross.Platforms.Mac.Presenters.Attributes;
using MvvmCross.Platforms.Mac.Views;
using Playground.Core.ViewModels;

namespace Playground.Mac
{
    [MvxTabPresentation(TabTitle = "Tab3")]
    public class Tab3View : MvxViewController<Tab3ViewModel>
    {
        public Tab3View() : base()
        {
        }

        public override void LoadView()
        {
            View = new AppKit.NSView(new CGRect(100, 100, 300, 300));
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }
    }
}
