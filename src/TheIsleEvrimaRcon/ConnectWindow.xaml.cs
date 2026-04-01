using System.IO;
using System.Net;
using System.Windows;
using TheIsleEvrimaRconClient;

namespace TheIsleEvrimaRcon
{
    public partial class ConnectWindow : Window
    {
        private const string ConnectionFile = "rcon.con";

        public string Host     { get; private set; } = "";
        public int    Port     { get; private set; }
        public string Password { get; private set; } = "";

        public ConnectWindow()
        {
            InitializeComponent();
            if (App.AppIcon != null) Icon = App.AppIcon;
            Loaded += ConnectWindow_Loaded;
        }

        private void ConnectWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(ConnectionFile))
            {
                var lines = File.ReadAllLines(ConnectionFile);
                if (lines.Length >= 3)
                {
                    TxtHost.Text = lines[0].Trim();
                    TxtPort.Text = lines[1].Trim();
                    PbPassword.Password        = lines[2].Trim();
                    TxtPasswordVisible.Text    = lines[2].Trim();
                    BtnConnect.Focus();
                    return;
                }
            }
            TxtHost.Focus();
        }

        private async void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (!IPAddress.TryParse(TxtHost.Text.Trim(), out var address))
            {
                MessageBox.Show("Enter a valid host IP address.", "Invalid host", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(TxtPort.Text.Trim(), out int port))
            {
                MessageBox.Show("Enter a valid port number.", "Invalid port", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var password = ChkShowPassword.IsChecked == true
                ? TxtPasswordVisible.Text
                : PbPassword.Password;
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Enter the RCON password.", "Password required", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SetFormEnabled(false);
            BtnConnect.Content = "Connecting…";

            var config = new EvrimaRconClientConfiguration
            {
                Host     = address,
                Port     = port,
                Password = password
            };

            bool connected;
            using (var rcon = new EvrimaRconClient(config))
                connected = await rcon.ConnectAsync();

            if (!connected)
            {
                SetFormEnabled(true);
                BtnConnect.Content = "Connect";
                MessageBox.Show(
                    "Could not connect to the RCON server.\nCheck the host, port and password.",
                    "Connection failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Host     = address.ToString();
            Port     = port;
            Password = password;

            File.WriteAllText(ConnectionFile, $"{Host}\n{Port}\n{Password}");

            var main = new MainWindow(Host, Port, Password);
            main.Show();
            Close();
        }

        private void SetFormEnabled(bool enabled)
        {
            TxtHost.IsEnabled              = enabled;
            TxtPort.IsEnabled              = enabled;
            PbPassword.IsEnabled           = enabled;
            TxtPasswordVisible.IsEnabled   = enabled;
            ChkShowPassword.IsEnabled      = enabled;
            BtnConnect.IsEnabled           = enabled;
        }

        private void ChkShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            TxtPasswordVisible.Text    = PbPassword.Password;
            TxtPasswordVisible.Visibility = Visibility.Visible;
            PbPassword.Visibility         = Visibility.Collapsed;
            TxtPasswordVisible.Focus();
        }

        private void ChkShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            PbPassword.Password        = TxtPasswordVisible.Text;
            PbPassword.Visibility         = Visibility.Visible;
            TxtPasswordVisible.Visibility = Visibility.Collapsed;
            PbPassword.Focus();
        }
    }
}

