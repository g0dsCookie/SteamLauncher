using System;

namespace CookieProjects.SteamLauncher.SteamConfig
{
	public class SteamConfigValueException : Exception
	{
		public string Line { get; }

		public SteamConfigValueException(string line)
			: base($"Unknown line {line}")
		{
			this.Line = line;
		}
	}
}
