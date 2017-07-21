using CookieProjects.SteamLauncher.SteamConfig;
using System.Collections.Generic;
using WinCommandPalette.Plugin;
using WinCommandPalette.Plugin.CommandBase;

namespace CookieProjects.SteamLauncher
{
	public class SteamLauncherPlugin : WCPPlugin
	{
		public override PluginMeta PluginMeta => new PluginMeta()
		{
			Author = "g0dsCookie",
			Description = "Launch your steam games from WinCommandPalette."
		};

		public override List<ICommandBase> AutoRegisterCommands { get; } = new List<ICommandBase>();

		public override void OnLoad()
		{
			var games = SteamUtils.LoadGames();
			foreach (var g in games)
				AutoRegisterCommands.Add(new SteamLauncherCommand(g.Name, g.AppId.ToString()));

			base.OnLoad();
		}
	}
}
