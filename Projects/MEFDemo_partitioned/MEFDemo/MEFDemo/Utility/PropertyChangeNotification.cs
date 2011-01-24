﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace MEFDemo.Utility
{
  public class PropertyChangeNotification : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected void RaisePropertyChanged(string property)
    {
      if (PropertyChanged != null)
      {
        this.PropertyChanged(this, new PropertyChangedEventArgs(property));
      }
    }
  }
}
