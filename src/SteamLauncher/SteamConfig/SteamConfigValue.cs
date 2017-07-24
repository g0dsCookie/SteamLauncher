namespace CookieProjects.SteamLauncher.SteamConfig
{
	/// <summary>
	/// Contains a "simple" Key:Value pair.
	/// </summary>
	public class SteamConfigValue : ISteamConfigEntry
	{
		/// <summary>
		/// Get the name (key) of the current value.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Get the type of the current entry.
		/// </summary>
		public SteamConfigType Type => SteamConfigType.Value;

		/// <summary>
		/// Get the value of the current entry.
		/// </summary>
		public string Value { get; }
		
		/// <summary>
		/// Initialize a new instance of <see cref="SteamConfigValue"/>.
		/// </summary>
		/// <param name="name">The name (key) of the current entry.</param>
		/// <param name="value">The value of the current entry.</param>
		public SteamConfigValue(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}

		public override string ToString()
		{
			return this.Value;
		}
	}
}
