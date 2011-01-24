using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Threading;

namespace ClockPlugin
{
  public partial class ClockControl : UserControl, INotifyPropertyChanged
  {
    public ClockControl()
    {
      InitializeComponent();

      this.Loaded += (s, e) =>
        {
          this.DataContext = this;

          DispatcherTimer timer = new DispatcherTimer();
          timer.Interval = new TimeSpan(0, 0, 1);
          timer.Tick += (a, b) =>
            {
              this.Time = DateTime.Now;
            };
          timer.Start();
        };
    }
    public DateTime Time
    {
      get
      {
        return (_Time);
      }
      set
      {
        _Time = value;
        this.RaisePropertyChanged("Time");
      }
    }
    DateTime _Time;

    void RaisePropertyChanged(string property)
    {
      if (this.PropertyChanged != null)
      {
        this.PropertyChanged(this, new PropertyChangedEventArgs(property));
      }
    }
    public event PropertyChangedEventHandler PropertyChanged;
  }
}
