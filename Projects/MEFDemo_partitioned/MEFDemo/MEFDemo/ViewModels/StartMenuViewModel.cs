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
using MEFDemo.Utility;
using Microsoft.Expression.Interactivity.Core;
using MEFDemo.Model;
using System.Collections.ObjectModel;
using ContractLibrary;

namespace MEFDemo.ViewModels
{
  public class StartMenuViewModel : PropertyChangeNotification
  {
    public StartMenuViewModel()
    {
      this.Plugins = new ObservableCollection<StartMenuItemViewModel>();
      PluginsModel.Model.PluginsLoaded += OnModelPluginsLoaded;
    }
    void OnModelPluginsLoaded(object sender, EventArgs e)
    {
      this.Plugins.Clear();

      foreach (var item in PluginsModel.Model.Plugins)
      {
        this.Plugins.Add(new StartMenuItemViewModel(item));
      }
      base.RaisePropertyChanged("PluginCount");
    }
    public int PluginCount
    {
      get
      {
        return (this.Plugins.Count);
      }
    }
    public ObservableCollection<StartMenuItemViewModel> Plugins
    {
      get
      {
        return (_Plugins);
      }
      set
      {
        _Plugins = value;
        base.RaisePropertyChanged("Plugins");
      }
    }
    ObservableCollection<StartMenuItemViewModel> _Plugins;
    
    public ICommand StartMenuCommand
    {
      get
      {
        if (this.startMenuCommand == null)
        {
          this.startMenuCommand = new ActionCommand(() =>
            {
              PluginsModel.Model.LoadPluginsAsync();
            });
        }
        return (this.startMenuCommand);
      }
    }
    ICommand startMenuCommand;
  }
}