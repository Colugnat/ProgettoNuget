using System.Collections.ObjectModel;
using System.ComponentModel;

namespace NugetPackage.Model
{
    class Nuget : INotifyPropertyChanged
    {
        #region =================== costanti ===================
        #endregion

        #region =================== membri statici =============
        #endregion

        #region =================== membri & proprietà ===========
        public event PropertyChangedEventHandler PropertyChanged;
        public string Directory { get; set; }
        public string NamePackage { get; set; }
        public string NameNewsPackage { get; set; }
        public string VersionPackage { get; set; }
        public ObservableCollection<string> ResultSearch { get; set; }
        public ObservableCollection<string> ResultSearchNews { get; set; }
        public string StartSearch { get; set; }
        public string ResultPackage { get; set; }
        public string ResultLog { get; set; }
        #endregion

        #region =================== costruttori ================
        #endregion

        #region =================== metodi aiuto ===============
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region =================== metodi generali ============
        #endregion
    }
}
