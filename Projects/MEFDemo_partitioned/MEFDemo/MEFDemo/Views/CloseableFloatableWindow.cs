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

namespace MEFDemo.Views
{
  public class CloseableFloatableWindow : FloatableWindow
  {
    public static DependencyProperty CloseCommandProperty =
      DependencyProperty.Register("CloseCommand", typeof(ICommand), typeof(CloseableFloatableWindow), null);

    public static DependencyProperty CloseCommandParameterProperty =
      DependencyProperty.Register("CloseCommandParameter", typeof(object), typeof(CloseableFloatableWindow), null);

    public ICommand CloseCommand
    {
      get
      {
        return ((ICommand)base.GetValue(CloseCommandProperty));
      }
      set
      {
        base.SetValue(CloseCommandProperty, value);
      }
    }
    public object CloseCommandParameter
    {
      get
      {
        return (base.GetValue(CloseCommandParameterProperty));
      }
      set
      {
        base.SetValue(CloseCommandParameterProperty, value);
      }
    }
    protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
    {
      base.OnClosing(e);

      if ((this.CloseCommand != null) && 
        (this.CloseCommand.CanExecute(this.CloseCommandParameter)))
      {
        CloseCommand.Execute(this.CloseCommandParameter);
      }
    }
  }
}
