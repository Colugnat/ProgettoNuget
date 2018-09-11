using NugetPackage.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    
        public List<string> RisultatoRicerca
        {
            get { return model.RisultatoRicerca; }
            set
            {
                model.RisultatoRicerca = value;
                OnPropertyChanged("RisultatoRicerca");
            }
        }

        public string RisultatoPacchetto
        {
            get { return model.RisultatoPacchetto; }
            set
            {
                model.RisultatoPacchetto = value;
                OnPropertyChanged("RisultatoPacchetto");
            }
        }


        public IDelegateCommand BrowseCommand { get; protected set; }
        public IDelegateCommand SaveCommand { get; protected set; }
        public IDelegateCommand ShowCommand { get; protected set; }
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
            BrowseCommand = new DelegateCommand(OnBrowse, CanBrowse);
            SaveCommand = new DelegateCommand(OnSave, CanSave);
            ShowCommand = new DelegateCommand(OnShow, CanShow);
        }

        private bool CanShow(object arg)
        {
            return true;
        }

        private void OnShow(object obj)
        {
            string text = "" + NomePacchetto + "\nVersion: " + VersionePacchetto;
            RisultatoPacchetto = text;
        }

        private bool CanSave(object arg)
        {
            return true;
        }

        private void OnSave(object obj)
        {
            if (Percorso == null)
            {
                Percorso = "C:\\ciao";
            }
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

        private bool CanBrowse(object arg)
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
