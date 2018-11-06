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
        public string PathInstalledPackage
        {
            get { return Nuget.PathInstalledPackage; }
            set
            {
                Nuget.PathInstalledPackage = value;
                OnPropertyChanged("PathInstalledPackage");
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
        public IDelegateCommand SearchInstalledCommand { get; protected set; }
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
            SearchInstalledCommand = new DelegateCommand(OnSearchInstalled, CanSearchInstalled);
        }

        private bool CanSearchInstalled(object arg)
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

        private void OnSearchInstalled(object obj)
        {
            string[] fileNewsContent = File.ReadAllLines("logFileNews.txt");

            // Create a list for put the name package inside
            InstalledPackage = new ObservableCollection<string>();
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
                // Add to the list
                InstalledPackage.Add(package.Id);
            }
        }

        private bool CanShowInstalled(object arg)
        {
            // Check if one package is selected
            if (NameInstalledPackage == null)
            {
                ResultLog += "No package selected\n";
                return false;
            }
            else
                return true;
            //System.InvalidOperationException
        }

        private void OnShowInstalled(object obj)
        {
            string packageID = NameInstalledPackage;

            // Connection with the Nuget database
            IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
            // Version of the selected package
            string versionPackage = "";
            string[] fileNewsContent = File.ReadAllLines("logFileNews.txt");
            // Create a list for put the name package inside
            for (int i = 0; i < fileNewsContent.Count(); i++)
            {
                string[] getVersion = fileNewsContent[i].Split(':');
                string[] nameCurrentId = getVersion[1].Split('\\');

                if (packageID == nameCurrentId[nameCurrentId.Length - 1])
                {
                    PathInstalledPackage = getVersion[0] + ":" + getVersion[1] + "." + getVersion[2];
                    versionPackage = getVersion[getVersion.Length - 1];
                }
            }

            // Description of the selected package
            string descriptionPackage = repo.FindPackagesById(packageID).First().Description.ToString();
            // Check the dependency of the Nuget package
            FrameworkName frameworkName = new FrameworkName("Anything", new Version("3.5"));
            string dependencyPackage = string.Join("\n - ", repo.Search(packageID, false).First().GetCompatiblePackageDependencies(frameworkName).Select(x => x));
            if (dependencyPackage == "")
                dependencyPackage = "No dependency";
            dependencyPackage = " - " + dependencyPackage;
            // Put all the information inside a string
            string text = "Name: " + packageID + "\nVersion: " + versionPackage + "\nPath: \n" + PathInstalledPackage + "\nDescription: \n" + descriptionPackage + "\nDependency: \n" + dependencyPackage;
            ResultInstalledPackage = text;
            // Information about what happend in the programm
            ResultLog += "Selected package " + packageID + "\n";
        }

        private bool CanDelete(object arg)
        {
            return true;
        }

        private void OnDelete(object obj)
        {
            if (PathInstalledPackage != null)
            {
                Directory.Delete(PathInstalledPackage, true);

                // Read all lines inside the logFileNews.txt
                string[] fileNewsContent = File.ReadAllLines("logFileNews.txt");
                int i = 0;
                int d = 0;
                // Check if the file already exist inside the file
                foreach (string newsName in fileNewsContent)
                {
                    string[] getVersion = newsName.Split(':');
                    string package = getVersion[1].Split('\\')[getVersion[1].Split('\\').Length - 1];
                    if (package == NameInstalledPackage)
                    {
                        d = i;
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }
                // If already exist the package, modify the path and the package inside the file
                List<string> linesList = File.ReadAllLines("logFileNews.txt").ToList();
                linesList.RemoveAt(d);
                File.WriteAllLines("logFileNews.txt", linesList.ToArray());
                OnSearchInstalled(obj);
                // Information about what happend in the programm
                ResultLog += "Deleted package " + NameInstalledPackage + " in the path: " + PathInstalledPackage + "\n";
            }
        }
        #endregion
    }
}
