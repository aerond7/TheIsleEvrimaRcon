using System.Configuration;
using System.Data;
using System.Windows;

namespace TheIsleEvrimaRcon
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static System.Windows.Media.ImageSource? AppIcon { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            try
            {
                var uri = new Uri("pack://application:,,,/evrima_icon.ico", UriKind.Absolute);
                AppIcon = System.Windows.Media.Imaging.BitmapFrame.Create(uri);
            }
            catch { /* icon is optional */ }
        }
    }
}
