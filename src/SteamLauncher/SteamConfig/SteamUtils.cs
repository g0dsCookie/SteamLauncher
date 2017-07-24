using System;
using System.Collections.Generic;
using System.IO;

namespace CookieProjects.SteamLauncher.SteamConfig
{
	/// <summary>
	/// Contains useful methods for steam interaction.
	/// </summary>
	public static class SteamUtils
	{
		/// <summary>
		/// Get or set the steam directory.
		/// </summary>
		public static string SteamDirectory
		{
			get
			{
				var cfg = Config.GlobalConfig;
				if (string.IsNullOrWhiteSpace(cfg.SteamDirectory))
				{
					string reg = @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Valve\Steam\";
					if (!Environment.Is64BitOperatingSystem)
						reg = @"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam\";
					cfg.SteamDirectory = Microsoft.Win32.Registry.GetValue(reg, "InstallPath", @"C:\Program Files(x86)\Steam").ToString();

					if (string.IsNullOrWhiteSpace(cfg.SteamDirectory) || !Directory.Exists(cfg.SteamDirectory))
						cfg.SteamDirectory = Environment.ExpandEnvironmentVariables(@"%ProgramFiles%\Steam");
				}
				return cfg.SteamDirectory;
			}
			set
			{
				Config.GlobalConfig.SteamDirectory = value;
			}
		}

		/// <summary>
		/// Load the given steam configuration file.
		/// </summary>
		/// <param name="file">The steam configuration to parse.</param>
		/// <returns></returns>
		public static SteamConfigSection LoadSteamConfig(string file)
		{
			var parser = new SteamConfigParser(file);
			parser.Parse();
			return parser.RootSection;
		}

		/// <summary>
		/// Load all known libraries from steam.
		/// </summary>
		/// <returns></returns>
		public static string[] LoadSteamLibraries()
		{
			var baseDir = Path.Combine(SteamDirectory, "steamapps");
			var libConfigFile = Path.Combine(baseDir, "libraryfolders.vdf");

			if (!File.Exists(libConfigFile))
				return new string[] { baseDir };

			var libConfig = LoadSteamConfig(libConfigFile);

			var i = 1;
			var libList = new List<string> { baseDir };
			while (libConfig.Contains(i.ToString()))
			{
				libList.Add(Path.Combine(libConfig[i.ToString()].ToString(), "steamapps"));
				i++;
			}
			return libList.ToArray();
		}

		/// <summary>
		/// Load all installed games from steam.
		/// </summary>
		/// <returns></returns>
		public static SteamGame[] LoadGames()
		{
			var libs = LoadSteamLibraries();
			var gameList = new List<SteamGame>();
			foreach (var l in libs)
				gameList.AddRange(LoadGames(l));
			return gameList.ToArray();
		}

		/// <summary>
		/// Load all installed games from the given library.
		/// </summary>
		/// <param name="library">The steam library folder, where to load the games from.</param>
		/// <returns></returns>
		public static SteamGame[] LoadGames(string library)
		{
			var gameList = new List<SteamGame>();
			foreach (var f in Directory.EnumerateFiles(library, "appmanifest_*.acf"))
			{
				var gameConfig = SteamGame.LoadFromAcf(f);
				gameList.Add(gameConfig);
			}
			return gameList.ToArray();
		}
	}
}
