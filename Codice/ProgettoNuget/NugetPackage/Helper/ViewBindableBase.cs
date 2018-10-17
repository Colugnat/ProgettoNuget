using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetPackage.Helper
{
    public abstract class ViewBindableBase
    {
        public abstract string Name { get; }
        protected abstract void RegisterMessages();   // per la comunicazione tra vm
        protected abstract void RegisterCommands();   // per la gestione di comandi / eventi

    }
}
