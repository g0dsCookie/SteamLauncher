namespace CookieProjects.SteamLauncher.SteamConfig
{
	public class SteamConfigValue : ISteamConfigEntry
	{
		public string Name { get; }

		public SteamConfigType Type => SteamConfigType.Value;

		public string Value { get; }
		
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
