using NuGet;
using System.Collections.Generic;
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

        private string percorso;

        public string Percorso
        {
            get { return percorso; }
            set { percorso = value; }
        }

        private string nomePacchetto;

        public string NomePacchetto
        {
            get { return nomePacchetto; }
            set { nomePacchetto = value; }
        }

        private string versionePacchetto;

        public string VersionePacchetto
        {
            get { return versionePacchetto; }
            set { versionePacchetto = value; }
        }

        private IEnumerable<IPackageFile> contenutoPacchetto;

        public IEnumerable<IPackageFile> ContenutoPacchetto
        {
            get { return contenutoPacchetto; }
            set { contenutoPacchetto = value; }
        }
        private List<string> risultatoRicerca;

        public List<string> RisultatoRicerca
        {
            get { return risultatoRicerca; }
            set { risultatoRicerca = value; }
        }

        private string inizioRicerca;

        public string InizioRicerca
        {
            get { return inizioRicerca; }
            set { inizioRicerca = value; }
        }

        public string RisultatoPacchetto { get; set; }
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
