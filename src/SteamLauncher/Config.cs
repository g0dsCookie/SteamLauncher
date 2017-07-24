using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace CookieProjects.SteamLauncher
{
	public class Config
	{
		public static Config GlobalConfig { get; } = Load("SteamLauncher.xml");

		public string SteamDirectory { get; set; }

		public void Save(string file)
		{
			var ns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
			var settings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };

			var serializer = new XmlSerializer(typeof(Config));
			using (var xml = XmlWriter.Create(file))
				serializer.Serialize(xml, this);
		}

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
