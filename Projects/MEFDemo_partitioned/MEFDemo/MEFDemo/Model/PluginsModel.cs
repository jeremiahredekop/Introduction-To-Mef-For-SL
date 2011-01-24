using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using ContractLibrary;
using MEFDemo.Services;
using System.Windows.Threading;

namespace MEFDemo.Model
{
   // [Export]
    public class PluginsModel : IPartImportsSatisfiedNotification
    {
        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<IPlugin> Plugins { get; set; }

        [Import]
        public IDeploymentCatalogService CatalogService { get; set; }

        private List<string> _loadedXaps = new List<string>();

        public PluginsModel()
        {
            CompositionInitializer.SatisfyImports(this);
        }

        public void LoadPluginsAsync()
        {
            CatalogService.AddXap("Extensions/ClockPlugin.xap");
            CatalogService.AddXap("Extensions/NotepadPlugin.xap");
            CatalogService.AddXap("Extensions/CalendarPlugin.xap");
        }

        public void OnImportsSatisfied()
        {
            if (this.PluginsLoaded != null)
            {
                this.PluginsLoaded(this, EventArgs.Empty);
            }
        }

        #region REST

        public event EventHandler PluginsLoaded;

        public event EventHandler<PluginLaunchedEventArgs> PluginLaunched;

        static PluginsModel()
        {
            model = new Lazy<PluginsModel>();
        }

        public static PluginsModel Model
        {
            get
            {
                return (model.Value);
            }
        }
        
        internal void LaunchPlugin(IPlugin plugin)
        {
            if (this.PluginLaunched != null)
            {
                this.PluginLaunched(this, new PluginLaunchedEventArgs()
                {
                    Plugin = plugin
                });
            }
        }
        static Lazy<PluginsModel> model;

        #endregion

    }
}
