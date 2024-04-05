using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SomeFunctionalOfCS._NetWorking
{
	class Client
	{
		TcpClient _client;
		public IPAddress _ip { get; set; }
		public int _port { get; set; }

		public Client(IPAddress ip, int port)
		{
			_ip = ip;
			_port = port;
			_client = new TcpClient();
		}

		public async Task StartConnect()
		{
			try
			{
				await _client.ConnectAsync(_ip, _port);
				Console.WriteLine("Was Connected");

				using (NetworkStream stream = _client.GetStream())
				using (StreamWriter writer = new StreamWriter(stream))
				using (StreamReader reader = new StreamReader(stream))
				{
					writer.AutoFlush = true;
					bool quite = false;
					Console.WriteLine("Type Message: ");
					while (!quite)
					{
						string ms = Console.ReadLine();
						await writer.WriteLineAsync(ms);
						if (ms.ToLower() == "exit") quite = true;
						else
						{
							Console.WriteLine(await reader.ReadLineAsync());
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
