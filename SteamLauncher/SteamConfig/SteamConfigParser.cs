using System.IO;
using System.Text.RegularExpressions;

namespace CookieProjects.SteamLauncher.SteamConfig
{
	public class SteamConfigParser
	{
		static Regex MatchValueLine = new Regex("\".*\"\t\t\".*\"", RegexOptions.Singleline);
		static Regex ExtractValueLine = new Regex(@"""(?'tag'.*?)""\t\t""(?'value'.*?)""", RegexOptions.Singleline);

		public string File { get; }

		public ISteamConfigEntry this[string key]
		{
			get
			{
				return this.rootSection[key];
			}
		}

		SteamConfigSection rootSection;

		public SteamConfigParser(string file)
		{
			this.File = file;
		}

		void LoadValue(SteamConfigSection section, string line)
		{
			var pair = ExtractValueLine.Matches(line);
			if (pair.Count < 1)
				throw new SteamConfigValueException(line);

			var key = pair[0].Groups["tag"].Value;
			var value = pair[0].Groups["value"].Value;

			section[key] = new SteamConfigValue(key, value);
		}

		void LoadSection(StreamReader reader, SteamConfigSection section, string name)
		{
			// Skip everything until open bracked reached
			// mostly, this is found on the next line
			while (reader.ReadLine().Trim() != "{") {}

			if (section == null)
				section = this.rootSection = new SteamConfigSection(name);

			var line = "";
			while (!reader.EndOfStream)
			{
				line = reader.ReadLine().Trim();
				if (line == "}") break;

				if (MatchValueLine.IsMatch(line))
					// Line contains a key:value pair
					this.LoadValue(section, line);
				else
				{
					// Line contains a new section
					var newName = line.Trim('"');
					section[newName] = new SteamConfigSection(newName);
					this.LoadSection(reader, section[newName] as SteamConfigSection, newName);
				}
			}
		}

		public void Parse()
		{
			using (var reader = new StreamReader(this.File))
				this.LoadSection(reader, null, reader.ReadLine().Trim('"'));
		}

		public SteamConfigSection GetRoot() => this.rootSection;
	}
}
