using Microsoft.Practices.ServiceLocation;
using NugetPackage.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetPackage.ViewModel
{
    public class ViewModelLocator
    {
        public static MainViewModel Main
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }
        public static NugetViewModel Nuget
        {
            get { return ServiceLocator.Current.GetInstance<NugetViewModel>(); }
        }
        public static SettingViewModel Setting
        {
            get { return ServiceLocator.Current.GetInstance<SettingViewModel>(); }
        }
        public static InstalledViewModel Installed
        {
            get { return ServiceLocator.Current.GetInstance<InstalledViewModel>(); }
        }
        public static AboutViewModel About
        {
            get { return ServiceLocator.Current.GetInstance<AboutViewModel>(); }
        }
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<SettingViewModel>();
            SimpleIoc.Default.Register<InstalledViewModel>();
            SimpleIoc.Default.Register<AboutViewModel>();
            SimpleIoc.Default.Register<NugetViewModel>(true);
        }
    }
}
