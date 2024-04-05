using SomeFunctionalOfCS;
using SomeFunctionalOfCS._Database;
using SomeFunctionalOfCS._NetWorking;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SomeFunction
{
	class Program
	{
		public async static Task Main(string[] arg)
		{
			Console.InputEncoding = Encoding.UTF8;
			Console.OutputEncoding = Encoding.Unicode;

			EF_5 main = new EF_5();
			main.Main();
		}

	}
}
