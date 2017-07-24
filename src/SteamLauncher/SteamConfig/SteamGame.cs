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
			var config = SteamUtils.LoadSteamConfig(file);
			
			return new SteamGame
			{
				AppId = Convert.ToInt32(config["appid"].ToString()),
				Name = config["name"].ToString(),
				InstallDir = config["installdir"].ToString()
			};
		}
	}
}
