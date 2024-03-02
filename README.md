# The Isle Evrima RCON
Windows application client for accessing a The Isle Evrima RCON server.

![Showcase Connection Form](/docs/form_connection.png)
![Showcase Main Form](/docs/form_main.png)

# Pre-requisites
This project is written in C# using .NET 8 and requires the .NET 8 runtime.

 - .NET 8 Desktop Runtime (https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
 - Enabled RCON on your The Isle Evrima server

# Getting started
1. First install the .NET 8 Desktop Runtime if you don't already have it.
2. Download the latest release (or build the project yourself) and copy the application files wherever you want.
3. Make sure your Evrima server has RCON set up.
4. Run the **TheIsleEvrimaRcon.exe** and connect to your The Isle Evrima server.

# How To Use
The client is very bare bones and does not contain any complex features.

It is split into 3 sections:
1. **Quick commands**, select a command from the dropdown menu, fill out the argument in the textbox on the right (in case of Announce, Ban, Kick, etc. commands) and click Execute. The output will show up in the console window. This is the recommended way of executing commands as the list contains all of currently available Evrima RCON commands.
2. **Console**, displays your entered commands and any output returned from the server. Read-only.
3. **Manual commands**, enter and execute commands yourself. The output in the console window will be raw data returned from the server. (for example in case of the "playerlist" command)

![Main Form Legend](/docs/form_main_sections.png)

# RCON Documentation from Evrima devs
https://docs.google.com/document/d/1JI_qVdKIZrqcVTY2Tqnm1T_Ws3_1r5nINGxfprbWw7w/edit#heading=h.p9tfb89b07jd

# Enabling RCON on Evrima server
To enable RCON on your The Isle Evrima server, open the `Game.ini` config file located in `...\Saved\Config\WindowsServer` and do the following.

Under `[/script/theisle.tigamesession]` add/edit these settings:
```ini
bRconEnabled=true
RconPassword=enter_rcon_password
RconPort=8888
```

And under `[/Script/Engine.Game]` add/edit these settings:
```ini
RconEnabled=true
RconPassword=enter_rcon_password
RconPort=8888
```

**NOTE**: Make sure the password and port are the same in both settings!

Also make sure to open port `8888` (or the port you set in the settings) in **Firewall**!

# Important notes
RCON is an unencrypted protocol, which means the password is sent in plain text over the network!

Be careful with using it outside your server's network!