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
            // stringa del pacchetto che si deve cercare
            string packageID = input.Text;

            // Connessione con il sito ufficiale dei pacchetti Nuget
            IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://api.nuget.org/v3/index.json");

            // Prendere la lista dei pacchetti Nuget con la stringa per il filtro ricerca     
            List<IPackage> packages = repo.FindPackagesById(packageID).ToList();

            // Filtra i pacchetti che sono stati rilasciati
            packages = packages.Where(item => (item.IsReleaseVersion() == false)).ToList();

            // Crea una lista
            List<string> items = new List<string>();
            // Due array con i contenuti dei file con versione
            contentNuget = new string[items.Count];
            versionNuget = new string[items.Count];
            int i = 0;
            // Riempimento degli array e della lista
            foreach (IPackage p in packages)
            {
                i++;
                versionNuget[i] = p.Version.ToString();
                contentNuget[i] = "" + p.GetContentFiles();
                items.Add(p.GetFullName());
            }
            // Invio dei dati nella listbox del WPF
            listNuget.ItemsSource = items;
        }

        private void listNuget_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Modificare la casella per vedere altri degli del pacchetto da installare
            string text = "" + listNuget.SelectedItem + "\nVersion: " + versionNuget[listNuget.SelectedIndex];
            version.Text = text;
        }
    }
}
