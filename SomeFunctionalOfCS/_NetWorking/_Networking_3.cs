using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace SomeFunctionalOfCS
{
	//Create a Simple server to receive request
	internal class _Networking_3
	{
		class MyHttpServer
		{
			private HttpListener _listener;

			public MyHttpServer(string[] preConfigs)
			{
				if (!HttpListener.IsSupported)
				{
					Console.WriteLine("The device not support this function");
					throw new Exception("DeviceNotSupportException");
				}

				if (preConfigs == null || preConfigs.Length == 0)
				{
					throw new ArgumentException("PreConfigs");
				}

				_listener = new HttpListener();
				foreach (var config in preConfigs)
				{
					_listener.Prefixes.Add(config);
				}
			}

			public async Task StartAsyn()
			{
				_listener.Start();
				Console.WriteLine("Server is start...");
				do
				{
					Console.WriteLine($"{DateTime.Now.ToLongTimeString()} : Waiting client connect...");
					var context = await _listener.GetContextAsync();
					await ProccessRequest(context);

				} while (_listener.IsListening);
			}

			public async Task ProccessRequest(HttpListenerContext context)
			{
				var request = context.Request;
				Console.WriteLine($"{request.HttpMethod} {request.RawUrl} {request.Url.AbsolutePath}");

				var response = context.Response;
				var outputStream = response.OutputStream;

				switch (request.Url.AbsolutePath)
				{
					case "/requestinfo":
						{
							context.Response.Headers.Add("content-type", "text/html");
							context.Response.StatusCode = (int)HttpStatusCode.OK;

							var html = this.GenHTML(request);
							var buffer = Encoding.UTF8.GetBytes(html);
							response.ContentLength64 = buffer.Length;
							await outputStream.WriteAsync(buffer, 0, buffer.Length);
						}
						break;
					case "/":
						{
							var buffer = Encoding.UTF8.GetBytes("Hello World");
							response.ContentLength64 = buffer.Length;
							await outputStream.WriteAsync(buffer, 0, buffer.Length);
						}
						break;
					case "/stop":
						{
							var buffer = Encoding.UTF8.GetBytes("Stop Server");
							response.ContentLength64 = buffer.Length;
							await outputStream.WriteAsync(buffer, 0, buffer.Length);
							_listener.Stop();
						}
						break;
					case "/json":
						{
							var product = new
							{
								Name = "Mac Pro 15",
								Price = 2000,
								Manufacturer = "Apple"
							};
							response.Headers.Add("Content-Type", "application/json");
							var jsonString = JsonConvert.SerializeObject(product);
							var buffer = Encoding.UTF8.GetBytes(jsonString);
							response.ContentLength64 = buffer.LongLength;
							await outputStream.WriteAsync(buffer, 0, buffer.Length);
						}
						break;
					case "/2.png":
						{
							response.Headers.Add("Content-Type", "image/png");
							var buffer = await File.ReadAllBytesAsync("2.png");
							response.ContentLength64 = buffer.Length;
							await outputStream.WriteAsync(buffer, 0, buffer.Length);
						}
						break;
					default:
						{
							response.StatusCode = 404;
							var buffer = Encoding.UTF8.GetBytes("NOT FOUND");
							response.ContentLength64 = buffer.LongLength;
							await outputStream.WriteAsync(buffer,0,buffer.Length);
						}
						break;
				}

				outputStream.Close();
			}

			private string GenHTML(HttpListenerRequest request)
			{
				string format = @"<!DOCTYPE html>
                            <html lang=""en""> 
                                <head>
                                    <meta charset=""UTF-8"">
                                    {0}
                                 </head> 
                                <body>
                                    {1}
                                </body> 
                            </html>";
				string head = "<title>Test WebServer</title>";
				var body = new StringBuilder();
				body.Append("<h1>Request Info</h1>");
				body.Append("<h2>Request Header:</h2>");

				var headers = from key in request.Headers.AllKeys
							  select $"<div>{key} : {string.Join(",", request.Headers.GetValues(key))}</div>";
				body.Append(string.Join("",headers));

				body.Append("<h2>Request properties:</h2>");
				var properties = request.GetType().GetProperties();
                foreach (var property in properties)
                {
					var name_pro = property.Name;
					string value_pro;
					try
					{
						value_pro = property.GetValue(request).ToString();
					}
					catch (Exception e)
					{
						value_pro = e.Message;
					}
					body.Append($"<div>{name_pro} : {value_pro}</div>");
				}
				string html = string.Format(format,head,body.ToString());
				return html;
            }

		}

		public async Task Main()
		{
			var configs = new string[]
			{
				@"http://localhost:8080/"
			};
			MyHttpServer server = new MyHttpServer(configs);
			await server.StartAsyn();
		}
	}
}
