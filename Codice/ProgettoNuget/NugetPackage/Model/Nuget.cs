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
        public string Percorso { get; set; }
        public string NomePacchetto { get; set; }
        public string VersionePacchetto { get; set; }
        public List<string> RisultatoRicerca { get; set; }
        public string InizioRicerca { get; set; }
        public string RisultatoPacchetto { get; set; }
        public string RisultatoLog { get; set; }
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
