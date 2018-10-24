using MahApps.Metro.Controls.Dialogs;
using NuGet;
using NugetPackage.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace NugetPackage.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void logArea_TextChanged(object sender, TextChangedEventArgs e)
        {
            logArea.ScrollToEnd();
        }
        private void logArea_Loaded(object sender, RoutedEventArgs e)
        {
            logArea.ScrollToEnd();
        }
    }
}
