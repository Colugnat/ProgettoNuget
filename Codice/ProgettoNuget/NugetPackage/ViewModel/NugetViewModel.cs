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
        public List<string> RisultatoRicercaNews
        {
            get { return model.RisultatoRicercaNews; }
            set
            {
                model.RisultatoRicercaNews = value;
                OnPropertyChanged("RisultatoRicercaNews");
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
        public IDelegateCommand ShowNewsCommand { get; protected set; }
        public IDelegateCommand SearchNewsCommand { get; protected set; }
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
            ShowNewsCommand = new DelegateCommand(OnShowNews, CanShowNews);
            SearchNewsCommand = new DelegateCommand(OnSearchNews, CanSearchNews);
        }

        private bool CanSearchNews(object arg)
        {
            return true;
        }

        private void OnSearchNews(object obj)
        {
            //Console.WriteLine(File.ReadAllText("logFileNews.txt"));
            if(File.Exists("logFileNews.txt"))
            {
                string[] fileNewsContent = File.ReadAllLines("logFileNews.txt");
                foreach (string newsName in fileNewsContent)
                {
                    string[] getVersion = newsName.Split(':');
                    string[] nameCurrentId = getVersion[1].Split('\\');
                    // Id del pacchetto che si deve ricercare
                    string packageID = nameCurrentId[nameCurrentId.Length - 1].Split('.')[0];

                    // Connessione al databare dei Nuget package
                    IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");

                    // Ricevere la lista di tutti i pacchetti trovati dalla ricerca  
                    IPackage package = repo.Search(packageID, false).First();
                    
                    // Crea una lista
                    RisultatoRicercaNews = new List<string>();
                    
                    if (package.Version.ToString() != getVersion[2])
                    {
                        RisultatoRicercaNews.Add(package.Id);
                    }
                    OnPropertyChanged("RisultatoRicercaNews");
                }
            }
            else
            {
                File.Create("logFileNews.txt");
            }
        }

        private bool CanShowNews(object arg)
        {
            // Controllo se l'utente preme in uno spazio vuoto nella Listbox
            if (NomePacchetto == null)
            {
                RisultatoLog += "Nessun pacchetto selezionato\n";
                OnPropertyChanged("RisultatoLog");
                return false;
            }
            else
                return true;
        }

        private void OnShowNews(object obj)
        {
            string packageID = NomePacchetto;
            // Connessione con il database dei Nuget package
            IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
            // Versione del pacchetto selezionato
            VersionePacchetto = repo.Search(packageID, false).First().Version.ToString();
            OnPropertyChanged("VersionePacchetto");
            // Descrizione del pacchetto selezionato
            var descizione = repo.FindPackagesById(packageID).First().Description.ToString();
            // Creazione della stringa dettagliata con le informazione sul pacchetto corrente
            FrameworkName frameworkName = new FrameworkName("Anything", new Version("3.5"));
            string dependency = string.Join("\n - ", repo.Search(packageID, false).First().GetCompatiblePackageDependencies(frameworkName).Select(x => x));
            if (dependency == "")
                dependency = "No dependency";
            string text = "Name: " + NomePacchetto + "\nVersion: " + VersionePacchetto + "\nDescription: \n" + descizione + "\nDependency: \n - " + dependency;
            RisultatoPacchetto = text;
            OnPropertyChanged("RisultatoPacchetto");
            // Informazione di ciò che è accaduto all'utente che sta utilizzando il programma
            RisultatoLog += "Selezionato pacchetto " + NomePacchetto + "\n";
            OnPropertyChanged("RisultatoLog");
        }

        private bool CanSearch(object arg)
        {
            return true;
            //No exception
        }

        private void OnSearch(object obj)
        {
            // Id del pacchetto che si deve ricercare
            string packageID = InizioRicerca;

            // Connessione al databare dei Nuget package
            IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");

            // Ricevere la lista di tutti i pacchetti trovati dalla ricerca  
            List<IPackage> packages = repo.Search(packageID, false).Take(14).ToList();

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
            // Controllo se l'utente preme in uno spazio vuoto nella Listbox
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
            // Connessione con il database dei Nuget package
            IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
            // Versione del pacchetto selezionato
            VersionePacchetto = repo.Search(packageID, false).First().Version.ToString();
            OnPropertyChanged("VersionePacchetto");
            // Descrizione del pacchetto selezionato
            var descizione = repo.FindPackagesById(packageID).First().Description.ToString();
            // Creazione della stringa dettagliata con le informazione sul pacchetto corrente
            FrameworkName frameworkName = new FrameworkName("Anything", new Version("3.5"));
            string dependency = string.Join("\n - ", repo.Search(packageID, false).First().GetCompatiblePackageDependencies(frameworkName).Select(x => x));
            if (dependency == "")
                dependency = "No dependency";
            string text = "Name: " + NomePacchetto + "\nVersion: " + VersionePacchetto + "\nDescription: \n" + descizione + "\nDependency: \n - " + dependency;
            RisultatoPacchetto = text;
            OnPropertyChanged("RisultatoPacchetto");
            // Informazione di ciò che è accaduto all'utente che sta utilizzando il programma
            RisultatoLog += "Selezionato pacchetto " + NomePacchetto + "\n";
            OnPropertyChanged("RisultatoLog");
        }

        private bool CanSave(object arg)
        {
            // Creazione di un percorso di default in caso non ci sia un percorso scelto
            string defaultPath = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            defaultPath = Path.Combine(defaultPath, "Downloads");
            // Controllo se il percorso è vuoto
            if (File.Exists("logFilePath.txt"))
            {
                if (Percorso == null)
                {
                    if (File.ReadAllText("logFilePath.txt") == "")
                    {
                        Percorso = defaultPath;
                        File.WriteAllText("logFilePath.txt", Percorso);
                    }
                    else
                        Percorso = File.ReadAllText("logFilePath.txt");
                }
            }
            else
            {
                File.Create("logFilePath.txt");
                Percorso = defaultPath;
            }
            return true;
            //System.ArgumentException
        }
        private void OnSave(object obj)
        {
            // Verificare che il percorso esista è se non esiste si crea il percorso
            if (!File.Exists(Percorso)) {
                // Creazione del percorso di default
                string defaultPath = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
                defaultPath = Path.Combine(defaultPath, "Downloads");
                // Controllo se il percorso è vuoto oppure null
                if (Percorso == "" || Percorso == null)
                {
                    Percorso = defaultPath;
                    RisultatoLog += "Nessun percorso scelto, il pacchetto si salverà nel percorso di default (" + Percorso + ")\n";
                    OnPropertyChanged("RisultatoLog");
                }
                // Creazione al collegamento con Nuget package
                IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
                PackageManager packageManager = new PackageManager(repo, Percorso);
                // Scaricamento del pacchetto zip è poi estrarlo nel percorso scelto
                packageManager.InstallPackage(NomePacchetto, SemanticVersion.Parse(VersionePacchetto));
                string pathVersion = Percorso + "\\" + NomePacchetto + "." + VersionePacchetto + ":" + VersionePacchetto;
                string[] fileNewsContent = File.ReadAllLines("logFileNews.txt");
                bool change = true;
                if(fileNewsContent.Length == 0)
                {
                    File.AppendAllText("logFileNews.txt", pathVersion + Environment.NewLine);
                }
                else
                {
                    foreach (string newsName in fileNewsContent)
                    {
                        if (newsName == pathVersion)
                            change = false;
                    }
                    if (change)
                        File.AppendAllText("logFileNews.txt", pathVersion + Environment.NewLine);
                    else
                        change = true;
                }
                File.WriteAllText("logFilePath.txt", Percorso);
                // Informazione per vedere quando il pacchetto ha finito di scaricare
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
                File.WriteAllText("logFilePath.txt", Percorso);
                RisultatoLog += "Selezionato il percorso " + Percorso + "\n";
                OnPropertyChanged("RisultatoLog");
            }
        }
        #endregion
    }
}