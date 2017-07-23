using System;

namespace CookieProjects.SteamLauncher.SteamConfig
{
	public class SteamConfigValueException : Exception
	{
		public string Line { get; }

		public SteamConfigValueException(string line)
			: base(Localization.strings.SteamConfigValueException.Replace("{{line}}", line))
		{
			this.Line = line;
		}
	}
}
