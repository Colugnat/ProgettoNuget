using NugetPackage.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
        public string NomePacchetto
        {
            get { return model.NomePacchetto; }
            set
            {
                model.NomePacchetto = value;
                OnPropertyChanged("NomePacchetto");
            }
        }
        public string VersionePacchetto
        {
            get { return model.VersionePacchetto; }
            set
            {
                model.VersionePacchetto = value;
                OnPropertyChanged("VersionePacchetto");
            }
        }
        public string ContenutoPacchetto
        {
            get { return model.ContenutoPacchetto; }
            set
            {
                model.ContenutoPacchetto = value;
                OnPropertyChanged("ContenutoPacchetto");
            }
        }

        public IDelegateCommand BrowseCommand { get; protected set; }
        public IDelegateCommand SaveCommand { get; protected set; }
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
            SaveCommand = new DelegateCommand(OnSave, canSave);
        }

        private bool canSave(object arg)
        {
            return true;
        }

        private void OnSave(object obj)
        {
            // Creare il percorso per la creazione del file (nome.versione.nupkg)
            string percorso = Percorso + "\\" + NomePacchetto + "." + VersionePacchetto + ".nupkg";

            // Verificare che il percorso esista è se non esiste si crea il percorso
            if (!File.Exists(percorso))
            {
                // contenuto nel file nuget preso dall'array contentNuget
                string createText = ContenutoPacchetto;
                // Creare un file con il contenuto
                File.WriteAllText(percorso, createText);
            }
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
