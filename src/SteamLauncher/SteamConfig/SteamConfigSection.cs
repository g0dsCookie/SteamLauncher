using System.Collections.Generic;
using System.Linq;

namespace CookieProjects.SteamLauncher.SteamConfig
{
	/// <summary>
	/// Holds all entries of a section.
	/// </summary>
	public class SteamConfigSection : ISteamConfigEntry
	{
		/// <summary>
		/// Get the name of the current section.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Get the type of the current entry.
		/// </summary>
		public SteamConfigType Type => SteamConfigType.Section;

		/// <summary>
		/// Get the entry with given key.
		/// </summary>
		/// <param name="key">The key to look for.</param>
		/// <returns></returns>
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

		/// <summary>
		/// Initialize a new instance of <see cref="SteamConfigSection"/>.
		/// </summary>
		/// <param name="name">The name of the current section.</param>
		public SteamConfigSection(string name)
		{
			this.Name = name;
		}
		
		/// <summary>
		/// Check if the given key exists.
		/// </summary>
		/// <param name="key">The key to look for.</param>
		/// <returns>Returns true if the key exists.</returns>
		public bool Contains(string key) => this.entries.ContainsKey(key);

		/// <summary>
		/// Return all values as <see cref="List{ISteamConfigEntry}"/>.
		/// </summary>
		/// <returns></returns>
		public List<ISteamConfigEntry> GetValues()
		{
			return new List<ISteamConfigEntry>(from KeyValuePair<string, ISteamConfigEntry> kvp in this.entries select kvp.Value);
		}
	}
}
