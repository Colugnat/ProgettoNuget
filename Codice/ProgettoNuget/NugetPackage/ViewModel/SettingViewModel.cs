using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetPackage.ViewModel
{
    class SettingViewModel
    {
        // Variable
        private IDialogCoordinator dialogCoordinator;

        // Constructor
        public SettingViewModel(IDialogCoordinator instance)
        {
            dialogCoordinator = instance;
        }

        // Methods
        private async Task FooMessageAsync()
        {
            await dialogCoordinator.ShowMessageAsync(this, "HEADER", "MESSAGE");
        }

        private async Task FooProgressAsync()
        {
            // Show...
            ProgressDialogController controller = await dialogCoordinator.ShowProgressAsync(this, "HEADER", "MESSAGE");
            controller.SetIndeterminate();

            // Do your work... 

            // Close...
            await controller.CloseAsync();
        }
    }
}
