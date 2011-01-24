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
using System.IO;

namespace NotepadPlugin
{
  public partial class NotepadUserControl : UserControl, INotifyPropertyChanged
  {
    public NotepadUserControl()
    {
      InitializeComponent();

      this.Loaded += (s, e) =>
        {
          this.DataContext = this;
        };
    }

    public string Text
    {
      get
      {
        return (_Text);
      }
      set
      {
        _Text = value;
        this.RaisePropertyChanged("Text");
      }
    }
    string _Text;

    void OnOpen(object sender, EventArgs args)
    {
      OpenFileDialog dialog = new OpenFileDialog()
      {
        Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
      };
      if (dialog.ShowDialog() == true)
      {
        using (FileStream stream = dialog.File.OpenRead())
        {
          using (StreamReader reader = new StreamReader(stream))
          {
            this.Text = reader.ReadToEnd();
            reader.Close();
          }
          stream.Close();
        }
      }
    }
    void OnSave(object sender, EventArgs args)
    {
      SaveFileDialog dialog = new SaveFileDialog()
      {
        Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
      };
      if (dialog.ShowDialog() == true)
      {
        using (Stream stream = dialog.OpenFile())
        {
          using (StreamWriter writer = new StreamWriter(stream))
          {
            writer.Write(this.Text);
            writer.Close();
          }
          stream.Close();
        }
      }
    }
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
