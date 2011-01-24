using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;

namespace MEFDemo.ViewModels
{
  public class LaunchedPluginViewModel
  {
    public event EventHandler Closed;

    public LaunchedPluginViewModel(UserControl content)
    {
      this.content = content;
    }
    public ICommand CloseCommand
    {
      get
      {
        if (this.closeCommand == null)
        {
          this.closeCommand = new ActionCommand(() =>
          {
            if (Closed != null)
            {
              Closed(this, EventArgs.Empty);
            }
          });
        }
        return (this.closeCommand);
      }
    }
    public UserControl Content
    {
      get
      {
        return (this.content);
      }
    }
    UserControl content;
    ICommand closeCommand;
  }
}
