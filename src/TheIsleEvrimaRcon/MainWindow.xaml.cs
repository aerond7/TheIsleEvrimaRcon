using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheIsleEvrimaRconClient;
using TheIsleEvrimaRconClient.Extensions;
using TheIsleEvrimaRconClient.Extensions.Models;

namespace TheIsleEvrimaRcon;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly string _host;
    private readonly int _port;
    private readonly string _password;

    private static readonly SolidColorBrush BrushAccent = new(Color.FromRgb(0xC8, 0xA8, 0x4B));
    private static readonly SolidColorBrush BrushSecondary = new(Color.FromRgb(0x9A, 0x9A, 0x9A));
    private static readonly SolidColorBrush BrushText = new(Color.FromRgb(0xEF, 0xEF, 0xEF));
    private static readonly SolidColorBrush BrushDanger = new(Color.FromRgb(0xC0, 0x39, 0x2B));
    private static readonly SolidColorBrush BrushSuccess = new(Color.FromRgb(0x27, 0xAE, 0x60));

    public MainWindow(string host, int port, string password)
    {
        InitializeComponent();
        if (App.AppIcon != null) Icon = App.AppIcon;
        _host = host;
        _port = port;
        _password = password;

        Title = $"Evrima RCON :: {host}:{port}";
        TxtConnectionInfo.Text = $"{host}:{port}";

        Loaded += async (_, _) =>
        {
            Log($"Connected to {host}:{port}", BrushSuccess);
            Log("Type 'help' for a list of all available commands.", BrushSecondary);
            TxtCommand.Focus();

            // Fetch server name
            try
            {
                using var rcon = BuildClient();
                if (await rcon.ConnectAsync())
                {
                    var details = await rcon.GetServerDetails();
                    TxtServerName.Text = details.Name;
                    Title = $"Evrima RCON :: {details.Name}";
                }
            }
            catch
            {
                /* non-critical, leave default title */
            }
        };
    }

    // ── Logging ──────────────────────────────────────────────────────────

    private void Log(string message, SolidColorBrush? color = null)
    {
        var para = new Paragraph { Margin = new Thickness(0) };
        var timestamp = new Run($"[{DateTime.Now:HH:mm:ss}] ") { Foreground = BrushSecondary };
        var text = new Run(message) { Foreground = color ?? BrushText };
        para.Inlines.Add(timestamp);
        para.Inlines.Add(text);
        RtbConsole.Document.Blocks.Add(para);
    }

    private void RtbConsole_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        RtbConsole.ScrollToEnd();
    }

    // ── RCON helpers ──────────────────────────────────────────────────────

    private EvrimaRconClient BuildClient() =>
        new(new EvrimaRconClientConfiguration
            { Host = System.Net.IPAddress.Parse(_host), Port = _port, Password = _password });

    private async Task RunAsync(Func<EvrimaRconClient, Task> action, string label)
    {
        Log($"> {label}", BrushAccent);
        try
        {
            using var rcon = BuildClient();
            if (!await rcon.ConnectAsync())
            {
                Log("Connection failed.", BrushDanger);
                return;
            }

            await action(rcon);
        }
        catch (Exception ex)
        {
            Log($"Error: {ex.Message}", BrushDanger);
        }
    }

    private async Task RunAsync(Func<EvrimaRconClient, Task<string?>> action, string label)
    {
        Log($"> {label}", BrushAccent);
        try
        {
            using var rcon = BuildClient();
            if (!await rcon.ConnectAsync())
            {
                Log("Connection failed.", BrushDanger);
                return;
            }

            var result = await action(rcon);
            if (!string.IsNullOrWhiteSpace(result)) Log(result);
        }
        catch (Exception ex)
        {
            Log($"Error: {ex.Message}", BrushDanger);
        }
    }

    // ── Raw command ───────────────────────────────────────────────────────

    private async void BtnSend_Click(object sender, RoutedEventArgs e)
    {
        var cmd = TxtCommand.Text.Trim();
        TxtCommand.Clear();
        if (string.IsNullOrWhiteSpace(cmd)) return;

        if (cmd.Equals("help", StringComparison.OrdinalIgnoreCase))
        {
            ShowHelp();
            TxtCommand.Focus();
            return;
        }

        await RunAsync(async rcon =>
        {
            var response = await rcon.SendCommandAsync(cmd);
            if (!string.IsNullOrWhiteSpace(response)) Log(response);
        }, cmd);

        TxtCommand.Focus();
    }

    private void ShowHelp()
    {
        Log("─── Available Commands ───────────────────────────────", BrushAccent);
        Log("  help                                      Show this help", BrushSecondary);
        Log("  announce <message>                        Announce to all players", BrushSecondary);
        Log("  directmessage <player>,<message>          Send a direct message to a player (EOS or Steam ID or Name)", BrushSecondary);
        Log("  playerlist                                List online players", BrushSecondary);
        Log("  getplayerdata                             Detailed stats per player", BrushSecondary);
        Log("  serverdetails                             Server configuration", BrushSecondary);
        Log("  save <backupName *optional*>              Save game data", BrushSecondary);
        Log("  wipecorpses                               Remove all corpses", BrushSecondary);
        Log("  ban <player>,<reason>                     Ban a player (EOS or Steam ID or Name)", BrushSecondary);
        Log("  kick <player>,<reason>                    Kick a player (EOS or Steam ID or Name)", BrushSecondary);
        Log("  togglewhitelist <0/1>                     Toggle server whitelist (0=disabled, 1=enabled)", BrushSecondary);
        Log("  addwhitelistid <playerId>                 Add player(s) to whitelist", BrushSecondary);
        Log("  removewhitelistid <playerId>              Remove player(s) from whitelist", BrushSecondary);
        Log("  toggleglobalchat <0/1>                    Toggle global chat (0=disabled, 1=enabled)", BrushSecondary);
        Log("  togglehumans <0/1>                        Toggle humans (0=disabled, 1=enabled)", BrushSecondary);
        Log("  toggleai <0/1>                            Toggle AI spawns (0=disabled, 1=enabled)", BrushSecondary);
        Log("  disableaiclasses <class[,class]>          Update AI spawn list", BrushSecondary);
        Log("  aidensity <0.0-1.0>                       Set AI spawn density", BrushSecondary);
        Log("  updateplayables <class:enabled/disabled>  Set playable classes", BrushSecondary);
        Log("─────────────────────────────────────────────────────", BrushAccent);
    }

    private void TxtCommand_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            e.Handled = true;
            BtnSend_Click(sender, e);
        }
    }

    // ── Sidebar buttons ───────────────────────────────────────────────────

    private async void BtnAnnounce_Click(object sender, RoutedEventArgs e)
    {
        var msg = TxtAnnounce.Text.Trim();
        if (string.IsNullOrWhiteSpace(msg))
        {
            Log("Enter a message to announce.", BrushDanger);
            return;
        }

        await RunAsync(rcon => rcon.SendCommandAsync($"announce {msg}"), $"announce {msg}");
    }

    private async void BtnSave_Click(object sender, RoutedEventArgs e)
        => await RunAsync(rcon => rcon.SendCommandAsync("save"), "save");

    private async void BtnWipeCorpses_Click(object sender, RoutedEventArgs e)
        => await RunAsync(rcon => rcon.SendCommandAsync("wipecorpses"), "wipecorpses");

    private async void BtnPlayerList_Click(object sender, RoutedEventArgs e)
    {
        Log("> playerlist", BrushAccent);
        try
        {
            using var rcon = BuildClient();
            if (!await rcon.ConnectAsync())
            {
                Log("Connection failed.", BrushDanger);
                return;
            }

            List<ServerPlayer> players = await rcon.GetPlayerList();
            if (players.Count == 0)
            {
                Log("No players online.");
                return;
            }

            Log($"Online players ({players.Count}):", BrushSuccess);
            foreach (var p in players)
                Log($"  {p.PlayerName}  -  {p.PlayerId}");
        }
        catch (Exception ex)
        {
            Log($"Error: {ex.Message}", BrushDanger);
        }
    }

    private async void BtnPlayerData_Click(object sender, RoutedEventArgs e)
    {
        Log("> getplayerdata", BrushAccent);
        try
        {
            using var rcon = BuildClient();
            if (!await rcon.ConnectAsync())
            {
                Log("Connection failed.", BrushDanger);
                return;
            }

            List<PlayerData> data = await rcon.GetPlayerData();
            if (data.Count == 0)
            {
                Log("No player data returned.");
                return;
            }

            Log($"Player data ({data.Count}):", BrushSuccess);
            foreach (var p in data)
                Log($"  {p.Name} [{p.Gender} {p.Class}] growth:{p.Growth:P0} hp:{p.Health:P0} stamina:{p.Stamina:P0} hunger:{p.Hunger:P0} thirst:{p.Thirst:P0}");
        }
        catch (Exception ex)
        {
            Log($"Error: {ex.Message}", BrushDanger);
        }
    }

    private async void BtnServerDetails_Click(object sender, RoutedEventArgs e)
    {
        Log("> serverdetails", BrushAccent);
        try
        {
            using var rcon = BuildClient();
            if (!await rcon.ConnectAsync())
            {
                Log("Connection failed.", BrushDanger);
                return;
            }

            ServerDetails d = await rcon.GetServerDetails();
            Log($"Server: {d.Name}", BrushSuccess);
            Log($"  Map: {d.Map}  |  Players: {d.CurrentPlayers}/{d.MaxPlayers}");
            Log($"  Whitelist: {d.WhitelistEnabled}  |  AI: {d.SpawnAIEnabled}  |  Humans: {d.HumansEnabled}");
            Log($"  Global Chat: {d.GlobalChatEnabled}  |  Mutations: {d.MutationsEnabled}");
            Log($"  Day: {d.DayLengthMinutes}m  |  Night: {d.NightLengthMinutes}m");
        }
        catch (Exception ex)
        {
            Log($"Error: {ex.Message}", BrushDanger);
        }
    }

    private async void BtnKick_Click(object sender, RoutedEventArgs e)
    {
        var id = TxtPlayerId.Text.Trim();
        if (string.IsNullOrWhiteSpace(id))
        {
            Log("Enter a Player ID to kick.", BrushDanger);
            return;
        }

        await RunAsync(rcon => rcon.SendCommandAsync($"kick {id},You have been kicked from the server."), $"kick {id},You have been kicked from the server.");
    }

    private async void BtnBan_Click(object sender, RoutedEventArgs e)
    {
        var id = TxtPlayerId.Text.Trim();
        if (string.IsNullOrWhiteSpace(id))
        {
            Log("Enter a Player ID to ban.", BrushDanger);
            return;
        }

        var confirm = MessageBox.Show(
            $"Ban player {id}?",
            "Confirm ban", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (confirm != MessageBoxResult.Yes) return;

        await RunAsync(rcon => rcon.SendCommandAsync($"ban {id},You have been banned from the server."), $"ban {id},You have been banned from the server.");
    }
}