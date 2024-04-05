using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SomeFunctionalOfCS._NetWorking
{
	internal class _Networking_4
	{
		public class TcpServer
		{
			readonly int PortNumber;
			private Dictionary<string, KeyValuePair<TcpClient, TcpClient>> Clients { get; set; }

			private string message="default";

			public TcpServer(int portNumber)
			{
				PortNumber = portNumber;
			}

			public async Task StartListener()
			{
				try
				{
					TcpListener listener = new TcpListener(IPAddress.Any,PortNumber);
					Console.WriteLine($"Listener in Port: {PortNumber}");
					listener.Start();

					while (true)
					{
						Console.WriteLine("Wait Client Connecting...");
						TcpClient client = await listener.AcceptTcpClientAsync();

						Task t = ProccessRequest(client);
					
					}
				} catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}

			private Task ProccessRequest(TcpClient client)
			{
				//delegate to create Task
				Action action = async () =>
				{
					try
					{
						using (client)
						{
							Console.WriteLine("Client is connected");
							using (Stream stream = client.GetStream())
							using (StreamReader reader = new StreamReader(stream))
							using (StreamWriter writer = new StreamWriter(stream))
							{
								writer.AutoFlush = true;
								bool exit = false;
								while (!exit)
								{
									string ms = await reader.ReadLineAsync();
									if (String.Compare(ms, "exit", true) == 0)
									{
										exit = true;
										await writer.WriteLineAsync(ms);
									}
									else
									{
										Console.WriteLine(ms);
										await writer.WriteLineAsync("was receive");
									}
								}
							}
						}
					} catch (Exception e)
					{
						Console.WriteLine(e);
					}
				};

				Task t = new Task(action);
				t.Start();
				return t;
			}

		}

		public async Task Main()
		{
			TcpServer server = new TcpServer(1950);
			await server.StartListener();
		}
	}
}
