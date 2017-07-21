using CookieProjects.SteamLauncher.SteamConfig;
using System.Diagnostics;
using System.IO;
using WinCommandPalette.Plugin.CommandBase;

namespace CookieProjects.SteamLauncher
{
	public class SteamLauncherCommand : ICommandBase
	{
		public string Name { get; }

		public string Description { get; }

		public System.Drawing.Image Icon => null;

		public bool RunInUIThread => false;

		public string AppId { get; }

		public SteamLauncherCommand(string name, string appid)
		{
			this.Name = name;
			this.Description = $"Run steam game {name}";
			this.AppId = appid;
		}

		public void Execute()
		{
			var steam = Path.Combine(SteamUtils.SteamDirectory, "steam.exe");
			var startInfo = new ProcessStartInfo(steam, $"-applaunch {this.AppId}");
			Process.Start(startInfo);
		}
	}
}
