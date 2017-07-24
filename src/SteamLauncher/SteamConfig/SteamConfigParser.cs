using System.IO;
using System.Text.RegularExpressions;

namespace CookieProjects.SteamLauncher.SteamConfig
{
	/// <summary>
	/// Contains methods to parse a steam configuration file.
	/// </summary>
	public class SteamConfigParser
	{
		/// <summary>
		/// Simply matches a key:value par line.
		/// </summary>
		static Regex MatchValueLine = new Regex("\".*\"\t\t\".*\"", RegexOptions.Singleline);

		/// <summary>
		/// Extracts the key and value from a key:value line.
		/// </summary>
		static Regex ExtractValueLine = new Regex(@"""(?'tag'.*?)""\t\t""(?'value'.*?)""", RegexOptions.Singleline);

		/// <summary>
		/// Get the current file path.
		/// </summary>
		public string File { get; }

		/// <summary>
		/// Get the value of the given key.
		/// </summary>
		/// <param name="key">The key to look for.</param>
		/// <returns></returns>
		public ISteamConfigEntry this[string key]
		{
			get
			{
				return this.RootSection[key];
			}
		}

		/// <summary>
		/// The root section of the current parser.
		/// </summary>
		public SteamConfigSection RootSection { get; private set; }

		/// <summary>
		/// Initializes a new instance of <see cref="SteamConfigParser"/>.
		/// </summary>
		/// <param name="file"></param>
		public SteamConfigParser(string file)
		{
			this.File = file;
		}

		/// <summary>
		/// Parse the given line as key:value pair.
		/// </summary>
		/// <param name="section">The current section, where to store the key:value pair.</param>
		/// <param name="line">The line that should be parsed.</param>
		void LoadValue(SteamConfigSection section, string line)
		{
			var pair = ExtractValueLine.Matches(line);
			if (pair.Count < 1)
				throw new SteamConfigValueException(line);

			var key = pair[0].Groups["tag"].Value;
			var value = pair[0].Groups["value"].Value;

			section[key] = new SteamConfigValue(key, value);
		}

		/// <summary>
		/// Parse the whole section.
		/// </summary>
		/// <param name="reader">The <see cref="StreamReader"/> where to read from.</param>
		/// <param name="section">The current section, where to store all the current entries.</param>
		/// <param name="name">The name of the current section.</param>
		void LoadSection(StreamReader reader, SteamConfigSection section, string name)
		{
			// Skip everything until open bracked reached
			// mostly, this is found on the next line
			while (reader.ReadLine().Trim() != "{") {}

			if (section == null)
				section = this.RootSection = new SteamConfigSection(name);

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

		/// <summary>
		/// Parses the file in <see cref="SteamConfigParser.File"/>.
		/// </summary>
		public void Parse()
		{
			using (var reader = new StreamReader(this.File))
				this.LoadSection(reader, null, reader.ReadLine().Trim('"'));
		}
	}
}
