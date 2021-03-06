using Eventhooks.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Eventhooks.Wpf.Views
{
    /// <summary>
    /// Interaction logic for SecondView.xaml
    /// </summary>
    [MvxViewFor(typeof(SecondViewModel))]
    public partial class SecondView : MvxWpfView
    {
        public SecondView()
        {
            InitializeComponent();
        }
    }
}
