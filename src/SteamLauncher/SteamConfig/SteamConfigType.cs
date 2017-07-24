namespace CookieProjects.SteamLauncher.SteamConfig
{
	/// <summary>
	/// Describes the type of an entry.
	/// </summary>
	public enum SteamConfigType
	{
		/// <summary>
		/// The entry is a "simple" Key:Value pair stored in <see cref="SteamConfigValue"/>.
		/// </summary>
		Value,
		/// <summary>
		/// The entry is another section stored in <see cref="SteamConfigSection"/>.
		/// </summary>
		Section
	}
}
