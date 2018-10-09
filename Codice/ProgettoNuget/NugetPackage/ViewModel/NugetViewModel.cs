using NuGet;
using NugetPackage.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
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
        public string Directory
        {
            get { return model.Directory; }
            set {
                model.Directory = value;
                OnPropertyChanged("Directory");
            }
        }
        public string NamePackage
        {
            get { return model.NamePackage; }
            set {
                model.NamePackage = value;
                OnPropertyChanged("NamePackage");
            }
        }
        public string VersionPackage
        {
            get { return model.VersionPackage; }
            set {
                model.VersionPackage = value;
                OnPropertyChanged("VersionPackage");
            }
        }
        public ObservableCollection<string> ResultSearch
        {
            get { return model.ResultSearch; }
            set {
                model.ResultSearch = value;
                OnPropertyChanged("ResultSearch");
            }
        }
        public ObservableCollection<string> ResultSearchNews
        {
            get { return model.ResultSearchNews; }
            set
            {
                model.ResultSearchNews = value;
                OnPropertyChanged("ResultSearchNews");
            }
        }
        public string ResultPackage
        {
            get { return model.ResultPackage; }
            set {
                model.ResultPackage = value;
                OnPropertyChanged("ResultPackage");
            }
        }
        public string StartSearch
        {
            get { return model.StartSearch; }
            set {
                model.StartSearch = value;
                OnPropertyChanged("StartSearch");
            }
        }
        public string ResultLog
        {
            get { return model.ResultLog; }
            set
            {
                model.ResultLog = value;
                OnPropertyChanged("ResultLog");
            }
        }
        public IDelegateCommand BrowseCommand { get; protected set; }
        public IDelegateCommand SaveCommand { get; protected set; }
        public IDelegateCommand ShowCommand { get; protected set; }
        public IDelegateCommand SearchCommand { get; protected set; }
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
            SearchNewsCommand = new DelegateCommand(OnSearchNews, CanSearchNews);
        }

        private bool CanSearchNews(object arg)
        {
            // Check if the file logFileNews.txt is created
            if (!File.Exists("logFileNews.txt"))
            {
                // Create file
                File.Create("logFileNews.txt");
            }
            // Control the internet connection
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                ResultLog = "No internet connection";
                OnPropertyChanged("ResultLog");
                return false;
            }
        }

        private void OnSearchNews(object obj)
        {
            string[] fileNewsContent = File.ReadAllLines("logFileNews.txt");

            // Create a list for put the name package inside
            ResultSearchNews = new ObservableCollection<string>();
            foreach (string newsName in fileNewsContent)
            {
                string[] getVersion = newsName.Split(':');
                string[] nameCurrentId = getVersion[1].Split('\\');
                // Id of the package inside the file logFileNews.txt
                string packageID = nameCurrentId[nameCurrentId.Length - 1];

                // Connection with the Nuget database
                IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");

                // Search all the file with the specified ID
                IPackage package = repo.Search(packageID, false).First();

                // Check if the package already installed have the last version
                if (package.Version.ToString() != getVersion[2])
                {
                    // Add to the list
                    ResultSearchNews.Add(package.Id);
                }
            }
            OnPropertyChanged("ResultSearchNews");
        }

        private bool CanSearch(object arg)
        {
            // Control the internet connection
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private void OnSearch(object obj)
        {
            // Id of the package
            string packageID = StartSearch;

            // Connection with the database
            IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");

            // Receive the list of all package searched 
            List<IPackage> packages = repo.Search(packageID, false).Take(14).ToList();

            // Create a list
            ResultSearch = new ObservableCollection<string>();

            // Add the package to the list
            foreach (IPackage p in packages)
            {
                ResultSearch.Add(p.Id);
            }
            OnPropertyChanged("ResultSearch");
        }
        private bool CanShow(object arg)
        {
            // Check if one package is selected
            if (NamePackage == null)
            {
                ResultLog += "No package selected\n";
                OnPropertyChanged("ResultLog");
                return false;
            }
            else
                return true;
            //System.InvalidOperationException
        }

        private void OnShow(object obj)
        {
            string packageID = NamePackage;
            // Connection with the Nuget database
            IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
            // Version of the selected package
            VersionPackage = repo.Search(packageID, false).First().Version.ToString();
            OnPropertyChanged("VersionPackage");
            // Description of the selected package
            var description = repo.FindPackagesById(packageID).First().Description.ToString();
            // Check the dependency of the Nuget package
            FrameworkName frameworkName = new FrameworkName("Anything", new Version("3.5"));
            string dependency = string.Join("\n - ", repo.Search(packageID, false).First().GetCompatiblePackageDependencies(frameworkName).Select(x => x));
            if (dependency == "")
                dependency = "No dependency";
            // Put all the information inside a string
            string text = "Name: " + NamePackage + "\nVersion: " + VersionPackage + "\nDescription: \n" + description + "\nDependency: \n - " + dependency;
            ResultPackage = text;
            OnPropertyChanged("ResultPackage");
            // Information about what happend in the programm
            ResultLog += "Selected package " + NamePackage + "\n";
            OnPropertyChanged("ResultLog");
        }

        private bool CanSave(object arg)
        {
            // Creation of a default path
            string defaultPath = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            defaultPath = Path.Combine(defaultPath, "Downloads");
            // Check if LogFilePath already exist
            if (File.Exists("logFilePath.txt"))
            {
                // Check if the Directory field is empty
                if (Directory == null)
                {
                    // If logFilePath.txt is empty, use the default path
                    if (File.ReadAllText("logFilePath.txt") == "")
                    {
                        Directory = defaultPath;
                        File.WriteAllText("logFilePath.txt", Directory);
                    }
                    else
                        Directory = File.ReadAllText("logFilePath.txt");
                }
            }
            else
            {
                // Create the logFilePath.txt
                File.Create("logFilePath.txt");
                Directory = defaultPath;
            }
            return true;
            //System.ArgumentException
        }
        private void OnSave(object obj)
        {
            if (NamePackage != null)
            {
                // Verify the path exist
                if (!File.Exists(Directory))
                {
                    // Creation of the default path
                    string defaultPath = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
                    defaultPath = Path.Combine(defaultPath, "Downloads");
                    // Control if the variable Directory is empty or null
                    if (Directory == "" || Directory == null)
                    {
                        Directory = defaultPath;
                        ResultLog += "No path setted, the package will be saved in the default path (" + Directory + ")\n";
                        OnPropertyChanged("ResultLog");
                    }
                    // Connection with the Nuget database
                    IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
                    PackageManager packageManager = new PackageManager(repo, Directory);
                    // Downloading and unzipping the Nuget package selected
                    packageManager.InstallPackage(NamePackage, SemanticVersion.Parse(VersionPackage));
                    // Create a information about where and what version of the package is installed
                    string pathVersion = Directory + "\\" + NamePackage + ":" + VersionPackage;
                    // Read all lines inside the logFileNews.txt
                    string[] fileNewsContent = File.ReadAllLines("logFileNews.txt");
                    bool change = true;
                    if (fileNewsContent.Length == 0)
                    {
                        // If file is empty put the first information
                        File.AppendAllText("logFileNews.txt", pathVersion + Environment.NewLine);
                    }
                    else
                    {
                        int i = 0;
                        int d = 0;
                        // Check if the file already exist inside the file
                        foreach (string newsName in fileNewsContent)
                        {
                            string[] getVersion = newsName.Split(':');
                            string package = getVersion[1].Split('\\')[getVersion[1].Split('\\').Length - 1];
                            if (package == NamePackage)
                            {
                                d = i;
                                change = false;
                                break;
                            }
                            else
                            {
                                i++;
                                change = true;
                            }
                        }
                        if (change)
                        {
                            // If it's a new package, write inside the file
                            File.AppendAllText("logFileNews.txt", pathVersion + Environment.NewLine);
                        }
                        else
                        {
                            // If already exist the package, modify the path and the package inside the file
                            List<string> linesList = File.ReadAllLines("logFileNews.txt").ToList();
                            linesList.RemoveAt(d);
                            File.WriteAllLines("logFileNews.txt", linesList.ToArray());
                            File.AppendAllText("logFileNews.txt", pathVersion + Environment.NewLine);
                            OnSearchNews(obj);
                            change = true;
                        }
                    }
                    // Write inside the logFilePath.txt the new directory
                    File.WriteAllText("logFilePath.txt", Directory);
                    // Information when the package is downloaded in the computer
                    ResultLog += "Package " + NamePackage + " saved with dependency in the path " + Directory + "\n";
                    OnPropertyChanged("ResultLog");
                }
            }
            else
            {
                ResultLog += "No package selected\n";
                OnPropertyChanged("ResultLog");
            }
        }

        private bool CanBrowse(object arg)
        {
            return true;
            //No exception
        }

        private void OnBrowse(object obj)
        {
            // Code that allows you to open a window to choose the path to save the Nuget package
            FolderBrowserDialog folderDialog = new FolderBrowserDialog {
                SelectedPath = "C:\\"
            };

            DialogResult result = folderDialog.ShowDialog();
            if (result.ToString() == "OK") {
                Directory = folderDialog.SelectedPath;
                OnPropertyChanged("Directory");
                File.WriteAllText("logFilePath.txt", Directory);
                ResultLog += "Selected path " + Directory + "\n";
                OnPropertyChanged("ResultLog");
            }
        }
        #endregion
    }
}