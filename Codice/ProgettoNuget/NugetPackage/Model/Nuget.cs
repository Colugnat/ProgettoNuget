using System.Collections.ObjectModel;
using System.ComponentModel;

namespace NugetPackage.Model
{
    public class Nuget
    {
        #region =================== costanti ===================
        #endregion

        #region =================== membri statici =============
        #endregion

        #region =================== membri & proprietà ===========
        public string Directory { get; set; }
        public string NamePackage { get; set; }
        public string VersionPackage { get; set; }
        public string VersionNewsPackage { get; set; }
        public ObservableCollection<string> ResultSearch { get; set; }
        public ObservableCollection<string> ResultSearchNews { get; set; }
        public string StartSearch { get; set; }
        public string ResultPackage { get; set; }
        public string ResultLog { get; set; }
        public string DescriptionPackage { get; set; }
        public string DependencyPackage { get; set; }
        #endregion

        #region =================== costruttori ================
        #endregion

        #region =================== metodi aiuto ===============

        #endregion

        #region =================== metodi generali ============
        #endregion
    }
}
