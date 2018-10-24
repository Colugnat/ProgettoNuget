using NugetPackage.Helper;
using NugetPackage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetPackage.ViewModel
{
    public class MainViewModel : BindableBase
    {
        private BindableBase currentViewModelBase;

        public BindableBase CurrentViewModel
        {
            get { return currentViewModelBase; }
            set
            {
                SetProperty(ref currentViewModelBase, value);
            }
        }
        public IDelegateCommand NugetPageCommand { get; private set; }
        public IDelegateCommand SettingPageCommand { get; private set; }
        public IDelegateCommand InstalledPageCommand { get; private set; }
        public MainViewModel()
        {
            SettingPageCommand = new DelegateCommand(OnSettingPage, CanSettingPage);
            NugetPageCommand = new DelegateCommand(OnNugetPage, CanNugetPage);
            InstalledPageCommand = new DelegateCommand(OnInstalledPage, CanInstalledPage);
            CurrentViewModel = ViewModelLocator.Nuget;
        }

        private bool CanInstalledPage(object arg)
        {
            return true;
        }

        private void OnInstalledPage(object obj)
        {
            CurrentViewModel = ViewModelLocator.Installed;
        }

        private bool CanSettingPage(object arg)
        {
            return true;
        }
        private void OnSettingPage(object obj)
        {
            CurrentViewModel = ViewModelLocator.Setting;
        }
        private bool CanNugetPage(object arg)
        {
            return true;
        }
        private void OnNugetPage(object obj)
        {
            CurrentViewModel = ViewModelLocator.Nuget;
        }
    }
}
