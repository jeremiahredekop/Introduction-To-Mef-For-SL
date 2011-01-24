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
using System.Collections.ObjectModel;
using MEFDemo.Model;
using Microsoft.Expression.Interactivity.Core;

namespace MEFDemo.ViewModels
{
  public class LaunchedPluginsViewModel
  {
    public ObservableCollection<LaunchedPluginViewModel> LaunchedPlugins { get; set; }

    public LaunchedPluginsViewModel()
    {
      this.LaunchedPlugins = 
        new ObservableCollection<LaunchedPluginViewModel>();

      PluginsModel.Model.PluginLaunched += OnPluginLaunched;
    }
    void OnPluginLaunched(object sender, PluginLaunchedEventArgs args)
    {
      LaunchedPluginViewModel viewModel = new LaunchedPluginViewModel(
        args.Plugin.CreatePluginUI());

      viewModel.Closed += (s, e) =>
        {
          this.LaunchedPlugins.Remove((LaunchedPluginViewModel)s);
        };

      this.LaunchedPlugins.Add(viewModel);
    }    
  }
}
