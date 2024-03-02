using System.Windows.Forms;
using TheIsleEvrimaRconClient;
using TheIsleEvrimaRconClient.Extensions;

namespace TheIsleEvrimaRcon
{
    public partial class Main : Form
    {
        private readonly EvrimaRconCommand[] CommandsWithArgument =
        {
            EvrimaRconCommand.Announce,
            EvrimaRconCommand.Ban,
            EvrimaRconCommand.Kick,
            EvrimaRconCommand.Custom
        };

        private readonly string host;
        private readonly int port;
        private readonly string password;

        public Main()
        {
            InitializeComponent();
            this.Text = Constants.WindowTitle;

            var connectionDialog = new RconConnectionDialog();
            if (connectionDialog.ShowDialog() != DialogResult.OK)
            {
                Environment.Exit(0);
                return;
            }

            this.host = connectionDialog.Host;
            this.port = connectionDialog.Port;
            this.password = connectionDialog.Password;
            UpdateTitle($"Connected to {connectionDialog.Host}:{connectionDialog.Port}");
            WriteToConsole($"Connected to {connectionDialog.Host}:{connectionDialog.Port}");

            var quickCommands = Enum.GetValues<EvrimaRconCommand>();
            if (quickCommands != null)
            {
                CbQuickCommands.Items.AddRange(quickCommands.Select(qc => qc.ToString()).ToArray());
                CbQuickCommands.SelectedIndex = 0;
            }
        }

        private void UpdateTitle(string info)
        {
            this.Text = Constants.WindowTitle + " - " + info;
        }

        private void WriteToConsole(string message)
        {
            RtbConsole.AppendText($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}] " + message + "\n");
        }

        private async Task ExecuteCommand(string command)
        {
            try
            {
                var splitCmd = command.Split(' ');
                if (string.IsNullOrEmpty(command) || splitCmd.Length <= 0)
                {
                    WriteToConsole("Enter a command");
                    return;
                }

                WriteToConsole("> " + command);
                using (var rcon = new EvrimaRconClient(this.host, this.port, this.password))
                {
                    var connected = await rcon.ConnectAsync();
                    if (connected)
                    {
                        var response = await rcon.SendCommandAsync(splitCmd[0], splitCmd.Length > 1 ? splitCmd[1] : "");
                        WriteToConsole(response);
                    }
                    else
                    {
                        WriteToConsole("Connection lost.");
                    }
                }
            }
            catch
            {
                WriteToConsole("Something went wrong while executing a command.");
            }
        }

        private async Task ExecuteCommand(EvrimaRconCommand command, string arg = "")
        {
            try
            {
                WriteToConsole("> " + command.ToString().ToLower() + " " + arg);
                using (var rcon = new EvrimaRconClient(this.host, this.port, this.password))
                {
                    var connected = await rcon.ConnectAsync();
                    if (connected)
                    {
                        if (command == EvrimaRconCommand.PlayerList)
                        {
                            var players = await rcon.GetPlayerList();
                            WriteToConsole($"Player list ({players.Count} online):\n" + string.Join('\n', players.Select(p => $"{p.PlayerName} (EOS ID: {p.EosId})")));
                            return;
                        }

                        var response = await rcon.SendCommandAsync(command, arg);
                        WriteToConsole(response);
                    }
                    else
                    {
                        WriteToConsole("Connection lost.");
                    }
                }
            }
            catch
            {
                WriteToConsole("Something went wrong while executing a command.");
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            WriteToConsole("Ready to execute commands.");

            TxtRconCommand.Focus();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void RtbConsole_TextChanged(object sender, EventArgs e)
        {
            RtbConsole.SelectionStart = RtbConsole.Text.Length;
            RtbConsole.ScrollToCaret();
        }

        private async void BtnSendCommand_Click(object sender, EventArgs e)
        {
            await ExecuteCommand(TxtRconCommand.Text);
            TxtRconCommand.Focus();
        }

        private void TxtRconCommand_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                BtnSendCommand_Click(sender, e);
            }
        }

        private void TxtQuickCommandArg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                BtnSendQuickCommand_Click(sender, e);
            }
        }

        private void CbQuickCommands_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedCommand = (EvrimaRconCommand)CbQuickCommands.SelectedIndex;
            var isArgCommand = CommandsWithArgument.Contains(selectedCommand);

            TxtQuickCommandArg.Clear();
            TxtQuickCommandArg.Enabled = isArgCommand;

            if (isArgCommand)
            {
                TxtQuickCommandArg.Focus();
            }
        }

        private async void BtnSendQuickCommand_Click(object sender, EventArgs e)
        {
            var selectedCommand = (EvrimaRconCommand)CbQuickCommands.SelectedIndex;
            var isArgCommand = CommandsWithArgument.Contains(selectedCommand);
            var arg = TxtQuickCommandArg.Text;
            TxtQuickCommandArg.Clear();

            if (isArgCommand)
            {
                TxtQuickCommandArg.Focus();
            }

            await ExecuteCommand(selectedCommand, arg);
        }
    }
}
