using NugetPackage.Helper;
using NugetPackage.Service;

namespace NugetPackage.ViewModel
{
    public class AboutViewModel : BindableBase
    {
        #region =================== costanti ===================
        #endregion

        #region =================== membri statici =============
        #endregion

        #region =================== membri & proprietà ===========
        public string LegalCopyright
        {
            get { return ApplicationVersionService.LegalCopyright + " - Alessandro Colugnat"; }
        }
        public string ProductVersion
        {
            get { return "Versione: " + ApplicationVersionService.ProductVersion; }
        }

        #endregion

        #region =================== costruttori ================
        #endregion

        #region =================== metodi aiuto ===============
        #endregion

        #region =================== metodi generali ============
        #endregion

    }
}
