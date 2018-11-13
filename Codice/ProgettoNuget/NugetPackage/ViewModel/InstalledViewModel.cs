using NuGet;
using NugetPackage.Helper;
using NugetPackage.Model;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Versioning;
using System.Text;
using System.Text.RegularExpressions;
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
        public string Directory
        {
            get { return Nuget.Directory; }
            set
            {
                Nuget.Directory = value;
                OnPropertyChanged("Directory");
            }
        }
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
        public IDelegateCommand GenerateCommand { get; protected set; }
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
            GenerateCommand = new DelegateCommand(OnGenerate, CanGenerate);
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
                IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
                PackageManager packageManager = new PackageManager(repo, Directory);
                packageManager.UninstallPackage(NameInstalledPackage);
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

        private bool CanGenerate(object arg)
        {
            return true;
        }

        private void OnGenerate(object obj)
        {
            // Initialize a pdf document object
            PdfDocument pdf = new PdfDocument();
            // Title of the document
            pdf.Info.Title = "Installed package";
            // Add the first page
            PdfPage pdfPage = pdf.AddPage();
            // Settings for the graphics
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Arial", 11, XFontStyle.Regular);
            XFont fontTitle = new XFont("Arial", 12, XFontStyle.Bold);
            XFont fontStartTitle = new XFont("Arial", 18, XFontStyle.Bold);
            string[] fileNewsContent = File.ReadAllLines("logFileNews.txt");
            // Create a list for put the name package inside
            int newLine = 40;
            graph.DrawString("Nuget package", fontStartTitle, XBrushes.Black, new XRect(pdfPage.Width.Point/2 - 80, newLine, pdfPage.Width.Point - 20, pdfPage.Height.Point - 20), XStringFormats.TopLeft);
            newLine += 60;
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
                graph.DrawString("Title:", fontTitle, XBrushes.Black, new XRect(40, newLine, pdfPage.Width.Point - 20, pdfPage.Height.Point - 20), XStringFormats.TopLeft);
                graph.DrawString(packageID, font, XBrushes.Black, new XRect(140, newLine, pdfPage.Width.Point - 20, pdfPage.Height.Point - 20), XStringFormats.TopLeft);
                newLine += 20;
                graph.DrawString("Version:", fontTitle, XBrushes.Black, new XRect(40, newLine, pdfPage.Width.Point - 20, pdfPage.Height.Point - 20), XStringFormats.TopLeft);
                graph.DrawString(getVersion[getVersion.Length - 1], font, XBrushes.Black, new XRect(140, newLine, pdfPage.Width.Point - 20, pdfPage.Height.Point - 20), XStringFormats.TopLeft);
                newLine += 20;
                graph.DrawString("Dependency:", fontTitle, XBrushes.Black, new XRect(40, newLine, pdfPage.Width.Point - 20, pdfPage.Height.Point - 20), XStringFormats.TopLeft);
                FrameworkName frameworkName = new FrameworkName("Anything", new Version("3.5"));
                string dependencyPackage = string.Join(", ", repo.Search(packageID, false).First().GetCompatiblePackageDependencies(frameworkName).Select(x => x));
                if (dependencyPackage == "")
                    dependencyPackage = "No dependency";
                string[] resLineDependencys = Regex.Split(dependencyPackage, ", ");
                foreach (string resLineDependency in resLineDependencys)
                {
                    graph.DrawString("- " + resLineDependency, font, XBrushes.Black, new XRect(140, newLine, pdfPage.Width.Point - 20, pdfPage.Height.Point - 20), XStringFormats.TopLeft);
                    newLine += 20;
                }
                newLine += 5;
                graph.DrawString("", font, XBrushes.Black, new XRect(140, newLine, pdfPage.Width.Point - 20, pdfPage.Height.Point - 20), XStringFormats.TopLeft);
                newLine += 20;
                if (newLine >= 700)
                {
                    pdfPage = pdf.AddPage();
                    graph = XGraphics.FromPdfPage(pdfPage);
                    newLine = 40;
                }

            }
            string pdfFilename = "InstalledNuget.pdf";
            // Save the file in the specific folder
            try
            {
                pdf.Save(Directory + "\\" + pdfFilename);
                ResultLog += "Generated PDF file in the path " + Directory + "\n";
            } catch (IOException)
            {
                ResultLog += "PDF already in use\n";
            }
        }
        #endregion
    }
}
