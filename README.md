# The Isle Evrima RCON — WPF

A WPF desktop client for managing a **The Isle Evrima** RCON server.
Built on [TheIsleEvrimaRconClient](https://www.nuget.org/packages/TheIsleEvrimaRconClient) v2.1.0, targeting .NET 8.

---

## Pre-requisites

- [.NET 8 Desktop Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- RCON enabled on your The Isle Evrima server (see [Enabling RCON](#enabling-rcon-on-an-evrima-server))

---

## Getting Started

1. Install the .NET 8 Desktop Runtime if you haven't already.
2. Download the latest release (or build the project) and place the files wherever you like.
3. Run **TheIsleEvrimaRcon.exe**.
4. Enter your server IP, RCON port, and password in the connection dialog and click **Connect**.

---

## Connection Dialog

On startup a connection window appears with three fields:

| Field | Description                                         |
| --- |-----------------------------------------------------|
| **Host** | IP address of your server (e.g. `127.0.0.1`)        |
| **Port** | RCON port configured on the server (default `8888`) |
| **Password** | RCON password set in `Game.ini`                     |

- Use **Show password** to toggle password visibility.
- On a successful connection the details are saved to `rcon.con` next to the executable so they are pre-filled next time.

---

## Main Window

The window is split into two panels.

### Left sidebar — Quick Commands

The sidebar shows the connected server name (fetched automatically on load) and connection address at the top, followed by a set of one-click command buttons:

| Button | Description |
| --- | --- |
| **Announce** | Broadcasts the message typed in the field above to all online players |
| **Save** | Saves all game data |
| **Wipe Corpses** | Removes all corpses from the world |
| **Player List** | Lists all online players with their Player IDs |
| **Player Data** | Shows detailed stats (class, growth, health, stamina, location) for every online player |
| **Server Details** | Displays the full server configuration |
| **Kick** | Kicks the player whose ID is entered in the field above the buttons |
| **Ban** | Bans the player whose ID is entered (prompts for confirmation first) |

> Player IDs are EOS or Steam IDs. Use **Player List** to look them up.

### Right panel — Console & Command Input

The right side contains:

- **Console log** — colour-coded output of all activity:
  - **Gold** — commands sent
  - **White** — server responses
  - **Green** — success messages
  - **Red** — errors and connection failures
  - **Grey** — timestamps and informational messages

- **Command bar** (bottom) — type any raw RCON command and press **Enter** or **Send**. The raw server response is printed to the console. Type `help` to see a full list of available commands.

---

## Available RCON Commands

Type `help` in the command bar at any time to print this list to the console.

| Command | Arguments | Description |
| --- | --- | --- |
| `help` | — | Show this help |
| `announce` | `<message>` | Announce a message to all players |
| `directmessage` | `<player>,<message>` | Send a direct message to a player (EOS or Steam ID or Name) |
| `playerlist` | — | List online players |
| `getplayerdata` | — | Detailed stats per player |
| `serverdetails` | — | Server configuration |
| `save` | `<backupName *optional*>` | Save game data |
| `wipecorpses` | — | Remove all corpses |
| `ban` | `<player>,<reason>` | Ban a player (EOS or Steam ID or Name) |
| `kick` | `<player>,<reason>` | Kick a player (EOS or Steam ID or Name) |
| `togglewhitelist` | `<0/1>` | Toggle server whitelist (0=disabled, 1=enabled) |
| `addwhitelistid` | `<playerId>` | Add player(s) to whitelist |
| `removewhitelistid` | `<playerId>` | Remove player(s) from whitelist |
| `toggleglobalchat` | `<0/1>` | Toggle global chat (0=disabled, 1=enabled) |
| `togglehumans` | `<0/1>` | Toggle humans (0=disabled, 1=enabled) |
| `toggleai` | `<0/1>` | Toggle AI spawns (0=disabled, 1=enabled) |
| `disableaiclasses` | `<class[,class]>` | Update AI spawn list |
| `aidensity` | `<0.0-1.0>` | Set AI spawn density |
| `updateplayables` | `<class:enabled/disabled>` | Set playable classes |

---

## Important Notes

- RCON is an **unencrypted** protocol — the password and all commands are transmitted in plain text. Avoid using it over untrusted networks; prefer a local network or VPN.
- Connection details (host, port, password) are stored in plain text in `rcon.con` next to the executable.

---

## Building from Source

```
dotnet build TheIsleEvrimaRcon.csproj
```

Requires .NET 8 SDK. The project uses:

- `TheIsleEvrimaRconClient` v2.1.0 (NuGet)
- `net8.0-windows` target framework
- WPF (`UseWPF`)

