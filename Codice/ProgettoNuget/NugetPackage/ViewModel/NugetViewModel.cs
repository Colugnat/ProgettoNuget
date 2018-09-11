using NugetPackage.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public string Percorso
        {
            get { return model.Percorso; }
            set
            {
                model.Percorso = value;
                OnPropertyChanged("Percorso");
            }
        }


        public IDelegateCommand BrowseCommand { get; protected set; }
        #endregion

        #region =================== costruttori ================
        public NugetViewModel()
        {
            model = new Nuget();
            RegisterCommands();
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
        protected void RegisterCommands()
        {
            BrowseCommand = new DelegateCommand(OnBrowse, canBrowse);
        }

        private bool canBrowse(object arg)
        {
            return true;
        }

        private void OnBrowse(object obj)
        {
            // Codice che permette di aprire una finestra per scegliere il percorso
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.SelectedPath = "C:\\";

            DialogResult result = folderDialog.ShowDialog();
            if (result.ToString() == "OK")
            {
                Percorso = folderDialog.SelectedPath;
                OnPropertyChanged("Percorso");
            }
        }
        #endregion
    }
}
