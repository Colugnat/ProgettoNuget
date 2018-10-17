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
        private Nuget model;
        public string Directory
        {
            get { return model.Directory; }
            set
            {
                model.Directory = value;
                OnPropertyChanged("Directory");
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
        #endregion

        #region =================== costruttori ================
        public SettingViewModel()
        {
            model = new Nuget();
            RegisterCommands();
        }
        #endregion

        #region =================== metodi aiuto ===============
        #endregion

        #region =================== metodi generali ============
        protected void RegisterCommands()
        {
            BrowseCommand = new DelegateCommand(OnBrowse, CanBrowse);
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
                File.WriteAllText("logFilePath.txt", Directory);
                ResultLog += "Selected path " + Directory + "\n";
                OnPropertyChanged("ResultLog");
            }
        }
        #endregion
    }
}