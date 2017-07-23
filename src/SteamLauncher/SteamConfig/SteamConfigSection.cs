using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CookieProjects.SteamLauncher.SteamConfig
{
	public class SteamConfigSection : ISteamConfigEntry
	{
		public string Name { get; }

		public SteamConfigType Type => SteamConfigType.Section;

		public ISteamConfigEntry this[string key]
		{
			get
			{
				return this.entries[key];
			}
			set
			{
				this.entries[key] = value;
			}
		}

		Dictionary<string, ISteamConfigEntry> entries = new Dictionary<string, ISteamConfigEntry>();

		public SteamConfigSection(string name)
		{
			this.Name = name;
		}

		public bool Contains(string key) => this.entries.ContainsKey(key);

		public List<ISteamConfigEntry> GetValues()
		{
			return new List<ISteamConfigEntry>(from KeyValuePair<string, ISteamConfigEntry> kvp in this.entries select kvp.Value);
		}
	}
}
