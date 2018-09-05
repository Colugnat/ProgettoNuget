using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #endregion

        #region =================== costruttori ================
        #endregion

        #region =================== metodi aiuto ===============
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler h = PropertyChanged;
            if (h != null)
            {
                h(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region =================== metodi generali ============
        #endregion
    }
}
