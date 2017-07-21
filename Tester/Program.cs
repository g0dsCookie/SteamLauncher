using CookieProjects.SteamLauncher.SteamConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
	class Program
	{
		static void Main(string[] args)
		{
			var parser = new SteamConfigParser(@"C:\Program Files (x86)\Steam\steamapps\appmanifest_292030.acf");
			parser.Parse();
			Console.WriteLine($"AppId = {parser["appid"]}");
			Console.WriteLine($"Name = {parser["name"]}");
			Console.ReadKey();
		}
	}
}
