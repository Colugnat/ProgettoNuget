using NuGet;
using NugetPackage.Helper;
using NugetPackage.Model;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NugetPackage.ViewModel
{
    public class SettingViewModel : BindableBase
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

        public bool IsFastDownloader
        {
            get { return Nuget.IsFastDownloader; }
            set
            {
                Nuget.IsFastDownloader = value;
                OnPropertyChanged("IsFastDownloader");
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

        public IDelegateCommand BrowseCommand { get; protected set; }
        public IDelegateCommand CheckCommand { get; protected set; }
        #endregion

        #region =================== costruttori ================
        public SettingViewModel()
        {
            RegisterCommands();
        }
        #endregion

        #region =================== metodi aiuto ===============
        #endregion

        #region =================== metodi generali ============
        protected void RegisterCommands()
        {
            BrowseCommand = new DelegateCommand(OnBrowse, CanBrowse);
            CheckCommand = new DelegateCommand(OnCheck, CanCheck);
        }

        private bool CanCheck(object arg)
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

        private void OnCheck(object obj)
        {
            if(IsFastDownloader)
            {
                IsFastDownloader = false;
                File.WriteAllText("logFileCheck.txt", "0");
            }
            else
            {
                IsFastDownloader = true;
                File.WriteAllText("logFileCheck.txt", "1");
            }
        }

        private bool CanBrowse(object arg)
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
        }

        private void OnBrowse(object obj)
        {
            // Code that allows you to open a window to choose the path to save the Nuget package
            FolderBrowserDialog folderDialog = new FolderBrowserDialog
            {
                SelectedPath = "C:\\"
            };

            DialogResult result = folderDialog.ShowDialog();
            if (result.ToString() == "OK")
            {
                Directory = folderDialog.SelectedPath;
                OnPropertyChanged("Directory");
                File.WriteAllText("logFilePath.txt", Directory + "\\NugetPackage");
                ResultLog += "Selected path " + Directory + "\n";
                Directory += "\\NugetPackage";
                OnPropertyChanged("ResultLog");
            }
        }
        #endregion
    }
}