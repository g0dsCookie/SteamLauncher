namespace CookieProjects.SteamLauncher.SteamConfig
{
	/// <summary>
	/// Interface for all configuration entries.
	/// </summary>
	public interface ISteamConfigEntry
	{
		/// <summary>
		/// Get the name of the current entry.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Get the type of the current entry.
		/// </summary>
		SteamConfigType Type { get; }
	}
}
