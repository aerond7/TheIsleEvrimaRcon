using System.Net;
using TheIsleEvrimaRconClient;

namespace TheIsleEvrimaRcon
{
    public partial class RconConnectionDialog : Form
    {
        public string Host { get; private set; } = "";
        public int Port { get; private set; }
        public string Password { get; private set; } = "";

        public RconConnectionDialog()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
        }

        private void RconConnectionDialog_Load(object sender, EventArgs e)
        {
            if (File.Exists(Constants.ConnectionFileName))
            {
                var text = File.ReadAllText(Constants.ConnectionFileName);
                var details = text.Split("\n");
                if (details.Length == 3)
                {
                    TxtHost.Text = details[0];
                    TxtPort.Text = details[1];
                    TxtPassword.Text = details[2];
                }

                BtnConnect.Focus();
            }
            else
            {
                TxtHost.Focus();
            }
        }

        private async void BtnConnect_Click(object sender, EventArgs e)
        {
            if (!IPAddress.TryParse(TxtHost.Text, out var address))
            {
                MessageBox.Show("Enter a valid host.", "Invalid host", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(TxtPort.Text, out int port))
            {
                MessageBox.Show("Enter a valid port.", "Invalid port", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtPassword.Text))
            {
                MessageBox.Show("Enter a password.", "Enter password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool connected = false;

            TxtHost.Enabled = false;
            TxtPort.Enabled = false;
            TxtPassword.Enabled = false;
            BtnConnect.Enabled = false;
            BtnConnect.Text = "Connecting...";

            using (var rcon = new EvrimaRconClient(address.ToString(), port, TxtPassword.Text))
            {
                connected = await rcon.ConnectAsync();
            }

            if (!connected)
            {
                TxtHost.Enabled = true;
                TxtPort.Enabled = true;
                TxtPassword.Enabled = true;
                BtnConnect.Enabled = true;
                BtnConnect.Text = "Connect";

                TxtHost.Focus();

                MessageBox.Show("Failed to connect to the RCON server, please check the connection details and try again.", "Connection failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BtnConnect.Text = "Connected";

            this.Host = address.ToString();
            this.Port = port;
            this.Password = TxtPassword.Text;

            File.WriteAllText(Constants.ConnectionFileName, $"{this.Host}\n{this.Port}\n{this.Password}");

            //MessageBox.Show($"Successfully connected to {this.Host}:{this.Port}", "Connected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
