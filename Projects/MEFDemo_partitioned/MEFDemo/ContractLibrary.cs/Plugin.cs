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
using System.ComponentModel.Composition;
using System.Windows.Media.Imaging;

namespace ContractLibrary
{
    [InheritedExport(typeof(IPlugin))]
    public abstract class Plugin : IPlugin
    {
        protected Plugin(string image)
        {
            _uri = String.Format("{0};component/Resources/Images/{1}", this.GetType().Name, image);
        }
        
        private ImageSource _image;
        private string _uri;

        public abstract UserControl CreatePluginUI();

        public ImageSource PluginIcon
        {
            get
            {
                if (_image == null)
                {
                    BitmapImage source = new BitmapImage();
                    source.SetSource(Application.GetResourceStream(
                      new Uri(_uri, UriKind.Relative)).Stream);

                    _image = source;
                }
                return (_image);
            }
        }
        
    }
}
