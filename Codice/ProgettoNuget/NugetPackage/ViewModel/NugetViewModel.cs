﻿using NuGet;
using NugetPackage.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace NugetPackage.ViewModel
{
    public class NugetViewModel : INotifyPropertyChanged
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
            set {
                model.Percorso = value;
                OnPropertyChanged("Percorso");
            }
        }
        public string NomePacchetto
        {
            get { return model.NomePacchetto; }
            set {
                model.NomePacchetto = value;
                OnPropertyChanged("NomePacchetto");
            }
        }
        public string VersionePacchetto
        {
            get { return model.VersionePacchetto; }
            set {
                model.VersionePacchetto = value;
                OnPropertyChanged("VersionePacchetto");
            }
        }
        public List<string> RisultatoRicerca
        {
            get { return model.RisultatoRicerca; }
            set {
                model.RisultatoRicerca = value;
                OnPropertyChanged("RisultatoRicerca");
            }
        }
        public string RisultatoPacchetto
        {
            get { return model.RisultatoPacchetto; }
            set {
                model.RisultatoPacchetto = value;
                OnPropertyChanged("RisultatoPacchetto");
            }
        }
        public string InizioRicerca
        {
            get { return model.InizioRicerca; }
            set {
                model.InizioRicerca = value;
                OnPropertyChanged("InizioRicerca");
            }
        }
        public string RisultatoLog
        {
            get { return model.RisultatoLog; }
            set
            {
                model.RisultatoLog = value;
                OnPropertyChanged("RisultatoLog");
            }
        }
        public IDelegateCommand BrowseCommand { get; protected set; }
        public IDelegateCommand SaveCommand { get; protected set; }
        public IDelegateCommand ShowCommand { get; protected set; }
        public IDelegateCommand SearchCommand { get; protected set; }
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region =================== metodi generali ============
        protected void RegisterCommands()
        {
            BrowseCommand = new DelegateCommand(OnBrowse, CanBrowse);
            SaveCommand = new DelegateCommand(OnSave, CanSave);
            ShowCommand = new DelegateCommand(OnShow, CanShow);
            SearchCommand = new DelegateCommand(OnSearch, CanSearch);
        }

        private bool CanSearch(object arg)
        {
            return true;
            //No exception
        }

        private void OnSearch(object obj)
        {
            //ID of the package to be looked up
            string packageID = InizioRicerca;

            //Connect to the official package repository
            IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");

            //Get the list of all NuGet packages with ID 'EntityFramework'       
            List<IPackage> packages = repo.Search(packageID, false).Take(17).ToList();

            // Crea una lista
            RisultatoRicerca = new List<string>();

            // Riempimento degli array e della lista
            foreach (IPackage p in packages)
            {
                RisultatoRicerca.Add(p.Id);
            }
            OnPropertyChanged("RisultatoRicerca");
        }
        private bool CanShow(object arg)
        {
            if (NomePacchetto == null)
            {
                RisultatoLog += "Nessun pacchetto selezionato\n";
                OnPropertyChanged("RisultatoLog");
                return false;
            }
            else
                return true;
            //System.InvalidOperationException
        }

        private void OnShow(object obj)
        {
            string packageID = NomePacchetto;
            IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
            VersionePacchetto = repo.Search(packageID, false).First().Version.ToString();
            OnPropertyChanged("VersionePacchetto");
            var descizione = repo.FindPackagesById(packageID).First().Description.ToString();
            FrameworkName frameworkName = new FrameworkName("Anything", new Version("4.0"));
            string text = "Name: " + NomePacchetto + "\nVersion: " + VersionePacchetto + "\nDescription: \n" + descizione;
            RisultatoPacchetto = text;
            OnPropertyChanged("RisultatoPacchetto");
            RisultatoLog += "Selezionato pacchetto " + NomePacchetto + "\n";
            OnPropertyChanged("RisultatoLog");
        }

        private bool CanSave(object arg)
        {
            string defaultPath = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            defaultPath = Path.Combine(defaultPath, "Downloads");
            if (Percorso == null)
                Percorso = defaultPath;
            return true;
            //System.ArgumentException
        }
        private void OnSave(object obj)
        {
            // Verificare che il percorso esista è se non esiste si crea il percorso
            if (!File.Exists(Percorso)) {
                // Creare un file con il contenuto
                string defaultPath = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
                defaultPath = Path.Combine(defaultPath, "Downloads");
                if (Percorso == "" || Percorso == null)
                {
                    Percorso = defaultPath;
                    RisultatoLog += "Nessun percorso scelto, il pacchetto si salverà nel percorso di default (" + Percorso + ")\n";
                    OnPropertyChanged("RisultatoLog");
                }
                IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
                PackageManager packageManager = new PackageManager(repo, Percorso);
                packageManager.InstallPackage(NomePacchetto, SemanticVersion.Parse(VersionePacchetto));
                RisultatoLog += "Pacchetto " + NomePacchetto + " salvato con le rispettive dipendenze nel percorso " + Percorso + "\n";
                OnPropertyChanged("RisultatoLog");
            }
        }

        private bool CanBrowse(object arg)
        {
            return true;
            //No exception
        }

        private void OnBrowse(object obj)
        {
            // Codice che permette di aprire una finestra per scegliere il percorso per salvare il pacchetto Nuget
            FolderBrowserDialog folderDialog = new FolderBrowserDialog {
                SelectedPath = "C:\\"
            };

            DialogResult result = folderDialog.ShowDialog();
            if (result.ToString() == "OK") {
                Percorso = folderDialog.SelectedPath;
                OnPropertyChanged("Percorso");
                RisultatoLog += "Selezionato il percorso " + Percorso + "\n";
                OnPropertyChanged("RisultatoLog");
            }
        }
        #endregion
    }
}