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
using System.ComponentModel.Composition;
using System.Windows.Media.Imaging;

namespace ClockPlugin
{
    public class ClockPlugin : Plugin
    {
        public ClockPlugin() : base("Clock.png")
        {
            
        }

        public override UserControl CreatePluginUI()
        {
            return new ClockControl();
        }
    }
}
