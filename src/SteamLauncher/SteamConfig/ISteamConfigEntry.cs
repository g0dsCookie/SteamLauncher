namespace CookieProjects.SteamLauncher.SteamConfig
{
	public interface ISteamConfigEntry
	{
		string Name { get; }

		SteamConfigType Type { get; }
	}
}
