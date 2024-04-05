using System.Text;

namespace SomeFunctionalOfCS
{
	internal class _Asyn_Await
	{
		object lockObject = new object();
		void DoSomeThing(int seconds, string s, ConsoleColor color)
		{
			// Lock để các tiến trình không resetColor lẫn nhau
			// Cũng như tránh việc xung đột khi truy cập tài nguyên

			lock (lockObject)
			{
				Console.ForegroundColor = color;
				Console.WriteLine($"{s}--Start");
				Console.ResetColor();
			}

			for (int i = 1; i <= seconds; i++)
			{
				lock (lockObject)
				{
					Console.ForegroundColor = color;
					Console.WriteLine($"{s}, {i,2}");
					Console.ResetColor();
				}
				Thread.Sleep(1000);
			}

			lock (lockObject)
			{
				Console.ForegroundColor = color;
				Console.WriteLine($"{s}--End");
				Console.ResetColor();
			}
		}

		void CreateTaskObject()
		{
			string s = "123";
			//Không truyền tham số
			/*
			Task t1, t2;
			t1 = new Task(
				() =>
				{
					DoSomeThing(5, "ABC", ConsoleColor.Green);
				}
			);

			t2 = new Task(
				() =>
				{
					DoSomeThing(4, "ABC", ConsoleColor.Red);
				}
			);	*/

			//Truyền 1 object
			Task t1 = new Task(
				(object s) =>
				{
					string? text = (string)s;
					DoSomeThing(4, text, ConsoleColor.Red);

				}, s);

			Task t2 = new Task(
				(object s) =>
				{
					string? text = (string)s;
					DoSomeThing(5, text, ConsoleColor.Green);

				}, s);
			t1.Start();
			t2.Start();
			// Trực tiếp khóa luồng này để chờ
			Task.WaitAll(t1, t2);
		}

		public async Task Task1()
		{
			Task t1 = new(
				(object s) =>
				{
					string? text = (string)s;
					DoSomeThing(5, text, ConsoleColor.Green);
				}, "abc");
			t1.Start();
			await t1;
			Console.WriteLine("T1 Was Complete!");
		}

		public async Task Task2()
		{
			Task t2 = new(
				(object s) =>
				{
					string? text = (string)s;
					DoSomeThing(5, text, ConsoleColor.Red);
				}, "abc");
			t2.Start();
			await t2;
			Console.WriteLine("T2 Was Complete!");
		}

		public async Task<string> _Task1()
		{
			Task<string> t1 = new((object o) =>
			{
				string s = (string)o;
				DoSomeThing(5, s, ConsoleColor.Red);
				return $"Return by {s}";
			}, "T1");
			t1.Start();
			await t1;
			Console.WriteLine("T1 is complete!");
			return t1.Result;
		}

		public async Task<string> _Task2()
		{
			Task<string> t2 = new((object o) =>
			{
				string s = (string)o;
				DoSomeThing(5, s, ConsoleColor.Green);
				return $"Return by {s}";
			}, "T2");
			t2.Start();
			await t2;
			Console.WriteLine("T1 is complete!");
			return t2.Result;
		}

		public async Task Clock()
		{
			Task clock = new(
				() =>
				{
					
					while (true)
					{
							Console.Clear();
							DateTime currentTime = DateTime.Now;
							string formattedTime = currentTime.ToString("HH:mm:ss");
							Console.WriteLine(formattedTime);
							Thread.Sleep(1000);
					}

				}
			);
			clock.Start();
			await clock;
		}

		public void _AdvertisementAg(string s)
		{
			s = s + "   ";
			StringBuilder sb = new StringBuilder(s);
			Console.WriteLine(sb);
			while (true)
			{
					Console.Clear();
					Console.ForegroundColor = ConsoleColor.DarkYellow;
					char tmp = sb[0];
					sb[0] = sb[sb.Length - 1];
					for (int i = s.Length - 1; i >= 2; i--)
					{
						sb[i] = sb[i - 1];
					}
					sb[1] = tmp;
					Console.Write(sb);
					Thread.Sleep(10);
			}
		}

		public async Task Advertisement(string content)
		{
			Task ads = new(() =>
				{
					_AdvertisementAg(content);
				});
				ads.Start();
			await ads;
		}

		//Use "Wait" in Main can be prevent the rest of code below Wait
		public async Task Main()
		{
			Task clock = Clock();
			Task ads = Advertisement("Hello World!  ");
			await Task.WhenAll(ads, clock);
			Console.WriteLine("Complete!");
			// Nhớ gọi bất đồng bộ ở Hàm Main trong Program
		}
	}
}
