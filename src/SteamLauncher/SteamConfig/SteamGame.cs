using System;

namespace CookieProjects.SteamLauncher.SteamConfig
{
	public struct SteamGame
	{
		public int AppId;

		public string Name;

		public string InstallDir;

		public static SteamGame LoadFromAcf(string file)
		{
			var parser = new SteamConfigParser(file);
			parser.Parse();

			return new SteamGame
			{
				AppId = Convert.ToInt32(parser["appid"].ToString()),
				Name = parser["name"].ToString(),
				InstallDir = parser["installdir"].ToString()
			};
		}
	}
}
