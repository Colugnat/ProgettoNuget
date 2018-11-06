using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NugetPackage.Service
{
    public class ApplicationVersionService
    {
        // va prendere le info da <progetto>/Properties/AssemblyInfo.cs
        private static FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
        public static string ProductVersion
        {
            get { return fvi.ProductVersion; }
        }
        public static string LegalCopyright
        {
            get { return fvi.LegalCopyright; }
        }
    }
}
