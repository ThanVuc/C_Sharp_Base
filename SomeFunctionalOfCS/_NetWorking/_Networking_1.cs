using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;

namespace SomeFunctionalOfCS
{
	internal class _Networking_1
	{
		protected string url { get; set; }
		protected Uri uri { get; set; }

		public _Networking_1()
		{
			url = "https://dictionary.cambridge.org/vi/dictionary/english/";
			uri = new Uri(url);
		}
		public _Networking_1(string _url)
		{
			url = _url;
			uri = new Uri(url);
		}

		public class _BaseMethod
		{
			Uri uri { get; set; }

			public _BaseMethod(string url)
			{
				uri = new Uri(url);
			}

			public void URI()
			{
				uri.GetType().GetProperties().ToList().ForEach(property =>
				{
					Console.WriteLine($"{property.Name} {property.GetValue(uri)}");
				});
				// Type of Segment: string[]
				Console.WriteLine($"Segment: {string.Join("", uri.Segments)}");
			}

			public void DNS()
			{
				var hostEntry = Dns.GetHostEntry(uri.Host);
				Console.WriteLine($"Host {uri.Host} IP:");
				hostEntry.AddressList.ToList().ForEach(ip =>
				{
					Console.WriteLine(ip);
				});
			}

			public async Task Ping()
			{
				Task task = new Task(() =>
				{
					var ping = new Ping();
					var pingReply = ping.Send(uri.Host);
					Console.WriteLine(pingReply.Status);
					if (pingReply.Status == IPStatus.Success)
					{
						Console.WriteLine(pingReply.RoundtripTime);
						Console.WriteLine(pingReply.Address);
					}
				});
				task.Start();
				await task;
			}
		}

		public class _HttpClient
		{
			public static HttpClient httpClient = new HttpClient();
			public static bool disposed = false;

			public static void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(httpClient);
			}

			public static void Dispose(bool disposing)
			{
				if (disposed) return;
				disposed = true;

				if (disposing)
				{
					httpClient.Dispose();
				}
			}

			public static async Task<string> GetWebContent(string url)
			{
				try
				{
					HttpResponseMessage ms = await httpClient.GetAsync(url);
					Console.WriteLine(ms.IsSuccessStatusCode);
					Console.WriteLine(ms.ReasonPhrase);
					Console.WriteLine(ms.RequestMessage);
					ms.EnsureSuccessStatusCode();
					httpClient.DefaultRequestHeaders.Add("Pragma", "no-cache");
					ShowAllHeaders(ms.Headers);
					string html = await ms.Content.ReadAsStringAsync();
					return html;
				}
				catch (Exception e)
				{
					Console.WriteLine($"Exception: {e.Message}");
					return "Error";
				}
			}
			public static void ShowAllHeaders(HttpResponseHeaders headers)
			{
				Console.WriteLine("Headers: ");
				foreach (var header in headers)
				{
					Console.WriteLine($"{header.Key,15} {String.Join(",", header.Value)}");
				}
			}

			public static async Task<byte[]> GetWebContentByBytes(string url)
			{
				try
				{
					HttpResponseMessage ms = await httpClient.GetAsync(url);
					Console.WriteLine($"{ms.IsSuccessStatusCode} {ms.ReasonPhrase}");
					ms.EnsureSuccessStatusCode();
					httpClient.DefaultRequestHeaders.Add("Pragma", "no-cache");
					ShowAllHeaders(ms.Headers);
					byte[] html = await ms.Content.ReadAsByteArrayAsync();
					return html;
				}
				catch (Exception e)
				{
					Console.WriteLine($"Exception: {e.Message}");
					return null;
				}
			}

			public static void DownLoadImageIntoFile(string url, string fileName)
			{
				Task<byte[]> bytes = _HttpClient.GetWebContentByBytes(url);
				using (FileStream fstream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
				{
					fstream.Write(bytes.Result, 0, bytes.Result.Length);
				}
			}

			// optimize equal read by Stream
			/*
			public async static Task DownLoadStream(string url, string fileName)
			{
				try
				{
					HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);

					using Stream stream = await httpResponseMessage.Content.ReadAsStreamAsync();
					using FileStream streamWrite = File.OpenWrite(fileName);

					const int SIZEBUFFER = 500;
					byte[] buffer = new byte[SIZEBUFFER];
					int nums;

					do
					{
						nums = await stream.ReadAsync(buffer, 0, SIZEBUFFER);
						streamWrite.Write(buffer,0,nums);
					} while (nums != 0);


				} catch (Exception e)
				{
					Console.WriteLine(e);
				}

			}
			*/

			public async static Task DownLoadStream(string url, string fileName)
			{
				try
				{
					HttpResponseMessage ms = await httpClient.GetAsync(url);
					
					using FileStream fstream = File.OpenWrite(fileName);
					using Stream stream = await ms.Content.ReadAsStreamAsync();

					const int BUFFERSIZE = 500;
					byte[] buffer = new byte[BUFFERSIZE];
					int nums = -1;

					do
					{
						nums = await stream.ReadAsync(buffer, 0, BUFFERSIZE);
						await fstream.WriteAsync(buffer, 0, nums);
					} while (nums != 0);

				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}

			// Use SendAsync to Request Web instead of GetAsync
			public async static Task GetWebContentBySendAsync(string url)
			{
				try
				{
					HttpRequestMessage httpMessageRequest = new HttpRequestMessage();
					httpMessageRequest.Method = HttpMethod.Get;
					httpMessageRequest.RequestUri = new Uri(url);
					httpMessageRequest.Headers.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("UTF8"));
					httpMessageRequest.Headers.Add("User-Agent", "Mozilla/5.0");

					HttpResponseMessage ms = await httpClient.SendAsync(httpMessageRequest);
					Console.WriteLine(ms.IsSuccessStatusCode);
					string html = await ms.Content.ReadAsStringAsync();
					ShowAllHeaders(ms.Headers);
					Console.WriteLine(html);
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}

			public async static Task PostData(string url)
			{
				Dictionary<string, string> parameters = new Dictionary<string, string>();
				parameters.Add("Key1","Value1");
				parameters.Add("Key2", "Value2");
				parameters.Add("Key3", "Value3");


				FormUrlEncodedContent content = new FormUrlEncodedContent(parameters);
				HttpRequestMessage httpRequestMs = new HttpRequestMessage();
				httpRequestMs.Method = HttpMethod.Post;
				httpRequestMs.RequestUri = new Uri(url);
				httpRequestMs.Content = content;

				HttpResponseMessage response= await httpClient.SendAsync(httpRequestMs);

				ShowAllHeaders(response.Headers);
				string html = await response.Content.ReadAsStringAsync();
				Console.WriteLine(html);

			}

			public async static Task PostJsonData(string url)
			{
				string data = @"{
					""Key1"": ""Value1"",
					""Key2"": ""Value2"",
					""Key3"": ""Value3""
				}";

				Console.WriteLine(data);

				StringContent content = new StringContent(data, Encoding.UTF8,"application/json");

				HttpRequestMessage httpRequestMs = new HttpRequestMessage();
				httpRequestMs.Method = HttpMethod.Post;
				httpRequestMs.RequestUri = new Uri(url);
				httpRequestMs.Content = content;

				HttpResponseMessage response = await httpClient.SendAsync(httpRequestMs);

				ShowAllHeaders(response.Headers);
				string html = await response.Content.ReadAsStringAsync();
				Console.WriteLine(html);
			}

			public async static Task PostMultiData(string url)
			{
				string data = @"{
					""Key1"": ""Value1"",
					""Key2"": ""Value2"",
					""Key3"": ""Value3""
				}";

				//Console.WriteLine(data);

				var content = new MultipartFormDataContent();
				
				Stream filestream = File.OpenRead("1.txt");
				var fileUpload = new StreamContent(filestream);
				content.Add(fileUpload, "fileUpload", "ABC.XYZ");
				var dic = new Dictionary<string, string>()
				{
					{"Key1", "1"},
					{"Key2", "2"},
					{"Key3", "3"},
					{"Key4", "4"},
				};

				var json = JsonSerializer.Serialize(dic);
				Console.WriteLine(json);
				var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

                content.Add(jsonContent,"JsonContent");

				HttpRequestMessage httpRequestMs = new HttpRequestMessage();
				httpRequestMs.Method = HttpMethod.Post;
				httpRequestMs.RequestUri = new Uri(url);
				httpRequestMs.Content = content;

				HttpResponseMessage response = await httpClient.PostAsync(url,content);

				ShowAllHeaders(response.Headers);
				string html = await response.Content.ReadAsStringAsync();
				Console.WriteLine(html);
			}

		}


		public async Task Main()
		{
			string url = "https://postman-echo.com/post";
			string fileName = "2.png";
			await _HttpClient.PostMultiData(url);
			_HttpClient.Dispose();
		}
	}
}
