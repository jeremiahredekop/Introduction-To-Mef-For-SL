using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ContractLibrary;
using Microsoft.Expression.Interactivity.Core;
using MEFDemo.Model;

namespace MEFDemo.ViewModels
{
  public class StartMenuItemViewModel
  {
    public StartMenuItemViewModel(IPlugin plugin)
    {
      this.plugin = plugin;
    }
    public ICommand LaunchCommand
    {
      get
      {
        if (launchCommand == null)
        {
          launchCommand = new ActionCommand(() =>
            {
              PluginsModel.Model.LaunchPlugin(this.plugin);
            });
        }
        return (launchCommand);
      }
    }
    public ImageSource Icon
    {
      get
      {
        return (this.plugin.PluginIcon);
      }
    }
    IPlugin plugin;
    ICommand launchCommand;
  }
}
