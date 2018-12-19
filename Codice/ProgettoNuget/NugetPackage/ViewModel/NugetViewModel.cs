using NuGet;
using NugetPackage.Helper;
using NugetPackage.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Versioning;
using System.Threading.Tasks;

namespace NugetPackage.ViewModel
{
    public class NugetViewModel : BindableBase
    {
        #region =================== costanti ===================
        #endregion

        #region =================== membri statici =============
        #endregion

        #region =================== membri & proprietà ===========
        public string Directory
        {
            get { return Nuget.Directory; }
            set {
                Nuget.Directory = value;
                OnPropertyChanged("Directory");
            }
        }
        public string NamePackage
        {
            get { return Nuget.NamePackage; }
            set {
                Nuget.NamePackage = value;
                OnPropertyChanged("NamePackage");
            }
        }
        public string VersionPackage
        {
            get { return Nuget.VersionPackage; }
            set {
                Nuget.VersionPackage = value;
                OnPropertyChanged("VersionPackage");
            }
        }
        public string VersionNewsPackage
        {
            get { return Nuget.VersionNewsPackage; }
            set
            {
                Nuget.VersionNewsPackage = value;
                OnPropertyChanged("VersionNewsPackage");
            }
        }
        public ObservableCollection<string> ResultSearch
        {
            get { return Nuget.ResultSearch; }
            set {
                Nuget.ResultSearch = value;
                OnPropertyChanged("ResultSearch");
            }
        }
        public ObservableCollection<string> ResultSearchNews
        {
            get { return Nuget.ResultSearchNews; }
            set
            {
                Nuget.ResultSearchNews = value;
                OnPropertyChanged("ResultSearchNews");
            }
        }
        public string ResultPackage
        {
            get { return Nuget.ResultPackage; }
            set {
                Nuget.ResultPackage = value;
                OnPropertyChanged("ResultPackage");
            }
        }
        public string StartSearch
        {
            get { return Nuget.StartSearch; }
            set {
                Nuget.StartSearch = value;
                OnPropertyChanged("StartSearch");
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
        public string DescriptionPackage
        {
            get { return Nuget.DescriptionPackage; }
            set
            {
                Nuget.DescriptionPackage = value;
                OnPropertyChanged("DescriptionPackage");
            }
        }
        public string DependencyPackage
        {
            get { return Nuget.DependencyPackage; }
            set
            {
                Nuget.DependencyPackage = value;
                OnPropertyChanged("DependencyPackage");
            }
        }
        public bool IsFastDownloader
        {
            get { return Nuget.IsFastDownloader; }
            set
            {
                Nuget.IsFastDownloader = value;
                OnPropertyChanged("IsFastDownloader");
            }
        }
        public IDelegateCommand SaveCommand { get; protected set; }
        public IDelegateCommand SaveFastCommand { get; protected set; }
        public IDelegateCommand ShowCommand { get; protected set; }
        public IDelegateCommand SearchCommand { get; protected set; }
        public IDelegateCommand SearchNewsCommand { get; protected set; }
        public IDelegateCommand CheckDeletedCommand { get; protected set; }
        #endregion

        #region =================== costruttori ================
        public NugetViewModel()
        {
            RegisterCommands();
        }
        #endregion

        #region =================== metodi aiuto ===============
        #endregion

        #region =================== metodi generali ============
        protected void RegisterCommands()
        {
            SaveCommand = new DelegateCommand(OnSave, CanSave);
            SaveFastCommand = new DelegateCommand(OnSaveFast, CanSaveFast);
            ShowCommand = new DelegateCommand(OnShow, CanShow);
            SearchCommand = new DelegateCommand(OnSearch, CanSearch);
            SearchNewsCommand = new DelegateCommand(OnSearchNews, CanSearchNews);
            CheckDeletedCommand = new DelegateCommand(OnCheckDeleted, CanCheckDeleted);
        }

        private bool CanCheckDeleted(object arg)
        {
            // Create the directory for put the package inside
            System.IO.Directory.CreateDirectory(Directory);
            // Check if the file logFileNews.txt is created
            if (!File.Exists("logFileNews.txt"))
            {
                // Create file
                File.Create("logFileNews.txt");
                return false;
            }
            // Check the internet connection
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
                ResultLog = "No internet connection\n";
                return false;
            }
        }

        private void OnCheckDeleted(object obj)
        {
            string[] fileNewsContent = File.ReadAllLines("logFileNews.txt");
            string[] dirs = System.IO.Directory.GetDirectories(Directory);
            string[] checkExists = new string[fileNewsContent.Length];
            int i = 0;
            foreach (string newsName in fileNewsContent)
            {
                foreach (string dire in dirs)
                {
                    string[] getVersion = newsName.Split(':');
                    string[] nameCurrentId = getVersion[1].Split('\\');
                    // Id of the package inside the file logFileNews.txt
                    string packageID = nameCurrentId[nameCurrentId.Length - 1];
                    if(dire.Contains(packageID + "." + getVersion[getVersion.Length - 1]))
                    {
                        checkExists[i] = packageID + "." + getVersion[getVersion.Length - 1] + ":" + "1";
                        break;
                    }
                    else
                    {
                        checkExists[i] = packageID + "." + getVersion[getVersion.Length - 1] + ":" + "0";
                    }
                }
                i++;
            }
            int y = 0;
            foreach (string checkExist in checkExists)
            {
                if(dirs.Length == 0)
                {
                    List<string> linesList = File.ReadAllLines("logFileNews.txt").ToList();
                    linesList.RemoveAt(y);
                    File.WriteAllLines("logFileNews.txt", linesList.ToArray());
                    if (y == 0)
                        y = 0;
                    else
                        y--;
                }
                else
                {
                    string[] getVersion = checkExist.Split(':');
                    // Id of the package inside the file logFileNews.txt
                    string package = getVersion[0];
                    if (getVersion[getVersion.Length - 1] == "0")
                    {
                        List<string> linesList = File.ReadAllLines("logFileNews.txt").ToList();
                        linesList.RemoveAt(y);
                        File.WriteAllLines("logFileNews.txt", linesList.ToArray());
                        if (y == 0)
                            y = 0;
                        else
                            y--;
                    }
                    else
                    {
                        y++;
                    }
                }
            }
        }

        private bool CanSearchNews(object arg)
        {
            // Check if the file logFileNews.txt is created
            if (!File.Exists("logFileNews.txt"))
            {
                // Create file
                File.Create("logFileNews.txt");
                return false;
            }
            // Check the internet connection
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
                ResultLog = "No internet connection\n";
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
        }

        private bool CanSearch(object arg)
        {
            // Check the internet connection
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
                ResultLog = "No internet connection\n";
                return false;
            }
        }

        private async void OnSearch(object obj)
        {
            // Id of the package
            string packageID = StartSearch;
            // Connection with the database
            IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
            List<IPackage> packages = new List<IPackage>();
            // Receive the list of all package searched 
            // Create a Action for use the asynchonous method
            Action newAction = new Action(() => { packages = repo.Search(packageID, false).Take(15).ToList(); });
            // Wait for the keyboard input
            await Task.Run(newAction);

            // Create a list
            ResultSearch = new ObservableCollection<string>();

            // Add the package to the list
            foreach (IPackage p in packages)
            {
                ResultSearch.Add(p.Id);
            }
        }

        private bool CanShow(object arg)
        {
            // Check if one package is selected
            if (NamePackage == null)
            {
                ResultLog += "No package selected\n";
                return false;
            }
            else
                return true;
            //System.InvalidOperationException
        }

        private void OnShow(object obj)
        {
            string packageID = NamePackage;
            StartSearch = NamePackage;
            // Connection with the Nuget database
            IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
            // Version of the selected package
            VersionPackage = repo.Search(packageID, false).First().Version.ToString();
            string[] fileNewsContent = File.ReadAllLines("logFileNews.txt");

            bool news = false;
            // Create a list for put the name package inside
            for (int i = 0; i < fileNewsContent.Count(); i++)
            {
                string[] getVersion = fileNewsContent[i].Split(':');
                string[] nameCurrentId = getVersion[1].Split('\\');
                // Id of the package inside the file logFileNews.txt
                string package = nameCurrentId[nameCurrentId.Length - 1];
                if ((packageID == package) && (getVersion[2] != VersionPackage))
                {
                    news = true;
                    VersionNewsPackage = getVersion[2];
                }
            }

            // Description of the selected package
            DescriptionPackage = repo.FindPackagesById(packageID).First().Description.ToString();
            // Check the dependency of the Nuget package
            FrameworkName frameworkName = new FrameworkName("Anything", new Version("3.5"));
            DependencyPackage = string.Join("\n - ", repo.Search(packageID, false).First().GetCompatiblePackageDependencies(frameworkName).Select(x => x));
            if (DependencyPackage == "")
                DependencyPackage = "No dependency";
            DependencyPackage = " - " + DependencyPackage;
            if(!news)
            {
                // Put all the information inside a string
                string text = "Name: " + NamePackage + "\nVersion: " + VersionPackage + "\nDescription: \n" + DescriptionPackage + "\nDependency: \n" + DependencyPackage;
                ResultPackage = text;
            } else
            {
                // Put all the information inside a string
                string text = "Name: " + NamePackage + "\nVersion: " + VersionNewsPackage + " -> " + VersionPackage + "\nDescription: \n" + DescriptionPackage + "\nDependency: \n" + DependencyPackage;
                ResultPackage = text;
            }
            // Information about what happend in the programm
            ResultLog += "Selected package " + NamePackage + "\n";
        }

        private bool CanSaveFast(object arg)
        {
            // Check if LogFilePath already exist
            if (File.Exists("logFileCheck.txt"))
            {
                // If logFilePath.txt is empty, use the default path
                if (File.ReadAllText("logFileCheck.txt") == "")
                {
                    File.WriteAllText("logFileCheck.txt", "0");
                }
                else
                {
                    if (File.ReadAllText("logFileCheck.txt") == "1")
                        IsFastDownloader = true;
                    else
                        IsFastDownloader = false;
                }
            }
            else
            {
                // Create the logFilePath.txt
                File.Create("logFileCheck.txt");
            }
            return true;
        }

        private void OnSaveFast(object obj)
        {
            if(IsFastDownloader)
            {
                OnSave(obj);
            }
        }

        private bool CanSave(object arg)
        {
            // Check if LogFilePath already exist
            if (File.Exists("logFileCheck.txt"))
            {
                // If logFilePath.txt is empty, use the default path
                if (File.ReadAllText("logFileCheck.txt") == "")
                {
                    File.WriteAllText("logFileCheck.txt", "0");
                }
                else
                {
                    if (File.ReadAllText("logFileCheck.txt") == "1")
                        IsFastDownloader = true;
                    else
                        IsFastDownloader = false;
                }
            }
            else
            {
                // Create the logFilePath.txt
                File.Create("logFileCheck.txt");
            }
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
                }
            }
            else
            {
                ResultLog += "No package selected\n";
            }
        }
        #endregion
    }
}