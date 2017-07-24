using System;

namespace CookieProjects.SteamLauncher.SteamConfig
{
	/// <summary>
	/// Contains informations for a steam game.
	/// </summary>
	public struct SteamGame
	{
		/// <summary>
		/// Get the application id of the game.
		/// </summary>
		public int AppId;

		/// <summary>
		/// Get the application name of the game.
		/// </summary>
		public string Name;

		/// <summary>
		/// Get the installation directory name.
		/// </summary>
		public string InstallDir;

		/// <summary>
		/// Parse the given .acf file into a <see cref="SteamGame"/>.
		/// </summary>
		/// <param name="file">The file where to load the game informations from.</param>
		/// <returns></returns>
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
