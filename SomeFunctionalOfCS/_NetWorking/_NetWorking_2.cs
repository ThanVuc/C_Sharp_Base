using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;

namespace SomeFunctionalOfCS
{
	internal class _NetWorking_2
	{
		//SocketHttpHandler
		public async Task Set_Get_Cookie(string url)
		{
			using var handler = new SocketsHttpHandler();
			CookieContainer cookies = new CookieContainer();

			handler.CookieContainer = cookies;
			handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

			Dictionary<string, string> dic = new Dictionary<string, string>()
			{
				{"Key1","Value1" },
				{"Key2","Value2" },
				{"Key3","Value3" }
			};

			FormUrlEncodedContent content = new FormUrlEncodedContent(dic);

			HttpClient client = new HttpClient(handler);
			HttpRequestMessage request = new HttpRequestMessage();
			request.Method = HttpMethod.Post;
			request.Content = content;
			request.RequestUri = new Uri(url);
			request.Headers.Add("User-Agent", "Mozilla/5.0");

			var response = await client.SendAsync(request);
			cookies.GetCookies(new Uri(url)).ToList().ForEach(c => Console.WriteLine($"{c.Name} : {c.Value}"));
			string html = await response.Content.ReadAsStringAsync();
			Console.WriteLine(html);
		}

		//DelegateHandler + Stratification

		class ClientHttpHandlerPipe
		{
			class SendMyHttp : HttpClientHandler
			{
				public SendMyHttp(CookieContainer cookies)
				{
					CookieContainer = cookies;
					AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
					UseCookies = true;
					AllowAutoRedirect = true;
				}

				protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
				{
					Console.WriteLine($"Sending request to: {request.RequestUri}...");
					var response= await base.SendAsync(request, cancellationToken);
					Console.WriteLine("Request was sent!");
					return response;
				}
			}

			class ChangeUri : DelegatingHandler
			{
				public ChangeUri(HttpMessageHandler innerHandler) : base(innerHandler)
				{
				}

				protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
				{
					var host = request.RequestUri.Host.ToLower();
					Console.WriteLine($"Check in RequestUri: {host}");
					if (host.Contains("google.com"))
					{
						Console.WriteLine("Request appropriate condition: is changing...");
						request.RequestUri = new Uri("https://github.com/");
						Console.WriteLine($"Was Change become: {request.RequestUri}");
					}
					return base.SendAsync(request, cancellationToken);
				}
			}

			class DenyAccessFacebook : DelegatingHandler
			{
				public DenyAccessFacebook(HttpMessageHandler innerHandler) : base(innerHandler)
				{
				}

				protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
				{
					var host = request.RequestUri.Host.ToLower();
					Console.WriteLine($"Check in DenyAccessFacebook: {host}");
					if (host.Contains("facebook.com"))
					{
						var response = new HttpResponseMessage(HttpStatusCode.OK);
						response.Content = new StringContent("It is Facebook - Access Was Deny");
						return await Task.FromResult<HttpResponseMessage>(response);
					}

					return await base.SendAsync(request, cancellationToken);
				}
			}

			public async Task Main()
			{
				string url = @"https://youtube.com/";
				CookieContainer cookies = new CookieContainer();
				cookies.Add(new Uri(url),new Cookie("key1","Value1"));

				SendMyHttp bottomHandler = new SendMyHttp(cookies);
				ChangeUri changeUri = new ChangeUri(bottomHandler);
				DenyAccessFacebook denyAccessFacebook = new DenyAccessFacebook(changeUri);

				HttpClient httpClient = new HttpClient(denyAccessFacebook);

				var response = await httpClient.GetAsync(new Uri(url));
				response.EnsureSuccessStatusCode();
				var html = await response.Content.ReadAsStringAsync();

				Console.WriteLine("The Cookies: ");
				cookies.GetCookies(new Uri(url)).ToList().ForEach(cookie =>
				{
					Console.WriteLine(cookie);
				});

				Console.WriteLine(html);


			}
		}

		public async Task Main()
		{
			//ClientHttpHandlerPipe p = new ClientHttpHandlerPipe();
			//await p.Main();
		}
	}
}
