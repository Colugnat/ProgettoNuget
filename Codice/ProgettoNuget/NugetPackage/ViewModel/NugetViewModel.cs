using NugetPackage.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetPackage.ViewModel
{
    class NugetViewModel : INotifyPropertyChanged
    {
        #region =================== costanti ===================
        #endregion

        #region =================== membri statici =============
        #endregion

        #region =================== membri & proprietà ===========
        public event PropertyChangedEventHandler PropertyChanged;
        private Nuget model;
        #endregion

        #region =================== costruttori ================
        public NugetViewModel()
        {
            model = new Nuget();
        }
        #endregion

        #region =================== metodi aiuto ===============
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler h = PropertyChanged;
            if (h != null)
            {
                h(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region =================== metodi generali ============
        #endregion
    }
}
