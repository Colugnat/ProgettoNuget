using NuGet;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace NugetPackage.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }
        private void Input_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //ID of the package to be looked up
            string packageID = input.Text;

            //Connect to the official package repository
            IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");

            //Get the list of all NuGet packages with ID 'EntityFramework'       
            List<IPackage> packages = repo.Search(packageID, false).Take(12).ToList();

            // Crea una lista
            List<string> items = new List<string>();

            // Riempimento degli array e della lista
            foreach (IPackage p in packages)
            {
                items.Add(p.Id);
            }
            // Invio dei dati nella listbox del WPF
            listNuget.ItemsSource = items;
        }
    }
}
