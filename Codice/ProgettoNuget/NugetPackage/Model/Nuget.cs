using System.Collections.ObjectModel;
using System.ComponentModel;

namespace NugetPackage.Model
{
    public static class Nuget
    {
        #region =================== costanti ===================
        #endregion

        #region =================== membri statici =============
        #endregion

        #region =================== membri & proprietà ===========
        public static string Directory { get; set; }
        public static string NamePackage { get; set; }
        public static string VersionPackage { get; set; }
        public static string VersionNewsPackage { get; set; }
        public static ObservableCollection<string> ResultSearch { get; set; }
        public static ObservableCollection<string> ResultSearchNews { get; set; }
        public static string StartSearch { get; set; }
        public static string ResultPackage { get; set; }
        public static string ResultLog { get; set; }
        public static string DescriptionPackage { get; set; }
        public static string DependencyPackage { get; set; }
        public static ObservableCollection<string> InstalledPackage { get; set; }
        public static string NameInstalledPackage { get; set; }
        public static string ResultInstalledPackage { get; set; }
        public static string PathInstalledPackage { get; set; }
        #endregion

        #region =================== costruttori ================
        #endregion

        #region =================== metodi aiuto ===============

        #endregion

        #region =================== metodi generali ============
        #endregion
    }
}
