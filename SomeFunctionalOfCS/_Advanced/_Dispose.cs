namespace SomeFunctionalOfCS
{
	internal class _Dispose
	{
		//Giai phong tai nguyen

		//dung using de auto goi dispose.
		//no chi co tac dung voi cac object duoc inherit IDisposable
		class A : IDisposable
		{
			List<int> list;
			public void Dispose()
			{
				Console.WriteLine("auto call when end of using");
				list = null;
			}
			~A()
			{
				Console.WriteLine("Huy");
				this.Dispose();
			}
		}

		class ReadFile : IDisposable
		{
			StreamReader sr;
			string path;
			bool _dispose = false;

			public ReadFile(string path)
			{
				this.path = path;
				sr = new StreamReader(this.path);
			}

			public void print()
			{
				string s = sr.ReadToEnd();
				Console.WriteLine(s);
			}

			//Chuan SGK
			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			public void Dispose(bool disposing)
			{
				if (_dispose) return;
				_dispose = true;

				if (disposing)
				{
					sr.Dispose();
					//release managed resourse
				}
				//release unmanaged resources
			}

		}

		void Test()
		{
			A a;
			using (a = new A())
			{
				Console.WriteLine("Do ST");
			}
		}

		public void Test2()
		{
			ReadFile rf = new ReadFile("test.txt");
			rf.print();
			rf.Dispose();
			rf.Dispose();

		}


		public void Main()
		{
			//Test2();

		}
	}
}
