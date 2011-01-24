using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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

namespace MEFDemo
{
    public interface IDeploymentCatalogService
    {
        void AddXap(string uri, Action<AsyncCompletedEventArgs> completedAction = null);
        void RemoveXap(string uri);
    }
    
}
