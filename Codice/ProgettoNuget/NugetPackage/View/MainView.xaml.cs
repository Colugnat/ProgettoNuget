using NuGet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace NugetPackage.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : System.Windows.Controls.UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }
        string[] contentNuget;
        string[] versionNuget;
        private void input_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //ID of the package to be looked up
            string packageID = input.Text;

            //Connect to the official package repository
            IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://api.nuget.org/v3/index.json");

            //Get the list of all NuGet packages with ID 'EntityFramework'       
            List<IPackage> packages = repo.FindPackagesById(packageID).ToList();

            //Filter the list of packages that are not Release (Stable) versions
            packages = packages.Where(item => (item.IsReleaseVersion() == false)).ToList();

            //Iterate through the list and print the full name of the pre-release packages to console
            List<string> items = new List<string>();
            contentNuget = new string[items.Count];
            versionNuget = new string[items.Count];
            int i = 0;
            foreach (IPackage p in packages)
            {
                i++;
                versionNuget[i] = p.Version.ToString();
                contentNuget[i] = "" + p.GetContentFiles();
                items.Add(p.GetFullName());
            }
            listNuget.ItemsSource = items;
        }

        private void browse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.SelectedPath = "C:\\";

            DialogResult result = folderDialog.ShowDialog();
            if (result.ToString() == "OK")
                path.Text = folderDialog.SelectedPath;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            string percorso = path.Text + "\\" + listNuget.SelectedItem + "." + versionNuget[listNuget.SelectedIndex] + ".nupkg";

            // This text is added only once to the file.
            if (!File.Exists(percorso))
            {
                // Create a file to write to.
                string createText = contentNuget[listNuget.SelectedIndex];
                File.WriteAllText(percorso, createText);
            }
        }

        private void listNuget_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = "" + listNuget.SelectedItem;
            version.Text = text;
        }
    }
}
