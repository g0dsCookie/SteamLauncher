using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace CookieProjects.SteamLauncher
{
	/// <summary>
	/// Holds all configuration variables for <see cref="SteamLauncherPlugin"/>
	/// </summary>
	public class Config
	{
		/// <summary>
		/// Get the global configuration.
		/// </summary>
		public static Config GlobalConfig { get; } = Load("SteamLauncher.xml");

		/// <summary>
		/// Get or set the path to the steam directory.
		/// </summary>
		public string SteamDirectory { get; set; }

		/// <summary>
		/// Save the current configuration.
		/// </summary>
		/// <param name="file">The file where to store the configuration.</param>
		public void Save(string file)
		{
			var ns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
			var settings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };

			var serializer = new XmlSerializer(typeof(Config));
			using (var xml = XmlWriter.Create(file))
				serializer.Serialize(xml, this);
		}

		/// <summary>
		/// Load the configuration.
		/// </summary>
		/// <param name="file">The file where to load the configuration from.</param>
		/// <returns></returns>
		public static Config Load(string file)
		{
			if (!File.Exists(file))
			{
				var config = new Config
				{
					SteamDirectory = string.Empty
				};
				return config;
			}

			var serializer = new XmlSerializer(typeof(Config));
			using (var stream = new StreamReader(file))
				return serializer.Deserialize(stream) as Config;
		}
	}
}
