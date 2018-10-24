using NugetPackage.Helper;
using NugetPackage.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetPackage.ViewModel
{
    public class InstalledViewModel : BindableBase
    {
        #region =================== costanti ===================
        #endregion

        #region =================== membri statici =============
        #endregion

        #region =================== membri & proprietà ===========
        public ObservableCollection<string> InstalledPackage
        {
            get { return Nuget.InstalledPackage; }
            set
            {
                Nuget.InstalledPackage = value;
                OnPropertyChanged("InstalledPackage");
            }
        }
        public string NameInstalledPackage
        {
            get { return Nuget.NameInstalledPackage; }
            set
            {
                Nuget.NameInstalledPackage = value;
                OnPropertyChanged("NameInstalledPackage");
            }
        }
        public string ResultInstalledPackage
        {
            get { return Nuget.ResultInstalledPackage; }
            set
            {
                Nuget.ResultInstalledPackage = value;
                OnPropertyChanged("ResultInstalledPackage");
            }
        }
        public string ResultLog
        {
            get { return Nuget.ResultLog; }
            set
            {
                Nuget.ResultLog = value;
                OnPropertyChanged("ResultLog");
            }
        }
        public IDelegateCommand DeleteCommand { get; protected set; }
        public IDelegateCommand ShowInstalledCommand { get; protected set; }
        #endregion

        #region =================== costruttori ================
        public InstalledViewModel()
        {
            RegisterCommands();
        }
        #endregion

        #region =================== metodi aiuto ===============
        #endregion

        #region =================== metodi generali ============
        protected void RegisterCommands()
        {
            ShowInstalledCommand = new DelegateCommand(OnShowInstalled, CanShowInstalled);
            DeleteCommand = new DelegateCommand(OnDelete, CanDelete);
        }

        private bool CanShowInstalled(object arg)
        {
            return true;
        }

        private void OnShowInstalled(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanDelete(object arg)
        {
            return true;
        }

        private void OnDelete(object obj)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
