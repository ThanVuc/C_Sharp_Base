using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Reflection;

namespace SomeFunctionalOfCS
{
	internal class _DI_DependencyInjection
	{
		interface IB
		{
			public void Print();
		}
		interface IC
		{
			public void Print();
		}

		class C : IC
		{
			public void Print()
			{
				Console.WriteLine("Hello you, C");
			}
		}

		class B : IB
		{
			public readonly IC _c;
			public B(IC c)
			{
				_c = c;
			}

			public void Print()
			{
				Console.WriteLine("Hello you, B");
				_c.Print();
			}
		}

		class B1 : IB
		{
			public Person _p;
			public B1(Person p)
			{
				_p = p;
			}

			public void Print()
			{
				Console.WriteLine("Hello you, B11111");
				_p.Print();
			}
		}

		class B2 : IB
		{
			public readonly IC _c;
			string _s;
			public B2(IC c, string s)
			{
				_c = c;
				_s = s;
			}

			public void Print()
			{
				Console.WriteLine("Hello you, B");
				Console.WriteLine(_s);
				_c.Print();
			}
		}

		class A
		{
			public readonly IB _b;
			public A(IB b)
			{
				_b = b;
			}

			public void Print()
			{
				Console.WriteLine("Hello you, A");
				_b.Print();
			}
		}


		class Person
		{
			public string Name { get; set; }
			public int Age { get; set; }

			public bool gender;
			public virtual bool Gender { get; set; }

			public virtual void Print() { }
		}

		class Boy : Person
		{
			public override bool Gender { get => gender; set => gender = true; }
			public override void Print()
			{
				Console.WriteLine(Gender + "Nam");
			}

		}

		class Girl : Person
		{
			public override bool Gender { get => gender; set => gender = false; }
			public override void Print()
			{
				Console.WriteLine(Gender + "Nu");
			}
		}

		class MyServiceOption
		{
			public string? Data1 { set; get; }
			public int Data2 { set; get; }
		}

		class MyService
		{
			public string? Data1 { set; get; }
			public int Data2 { set; get; }

			public MyService(IOptions<MyServiceOption> options)
			{
				var _option = options.Value;
				Data1 = _option.Data1;
				Data2 = _option.Data2;
			}

			public void PrintData()
			{
				Console.WriteLine(Data1 + " / " + Data2);
			}
		}


		public IConfigurationRoot GetFileConfig(string fileName)
		{
			return new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory()+@"\ConfigFolder")
				.AddJsonFile(fileName)
				.Build();
		}

		public void DI_Completely()
		{

			var services = new ServiceCollection();

			//DelegateRegister(services);

			//IOption
			/*
			services.AddSingleton<MyService>();
			services.Configure<MyServiceOption>(
				(option) =>
				{
					option.Data1 = "Hello New Year!";
					option.Data2 = 2024;
				}
			);
			*/

			//Config From File
			IConfigurationRoot configurationRoot = GetFileConfig("configOption.json");
			services.AddSingleton<MyService>();
			services.Configure<MyServiceOption>(
				// Pass the part Json appropriate with class (key = Field & Setter, Getter & Value = type)
				configurationRoot.GetSection("ConfigSection")
			);

			var provider = services.BuildServiceProvider();

			GetSimpleServiecs(provider);
		}

		public static void GetSimpleServiecs(IServiceProvider provider)
		{
			var a = provider.GetService<MyService>();
			a.PrintData();
		}

		public void DI_Design()
		{
			IC c = new C();
			IB b = new B(c);
			A a = new A(b);
			a.Print();
		}

		public void RegisterSimpleService(IServiceCollection services)
		{
			// Test create 1 simple DI Design
			// Can be use with astract class and this instance
			// also with inheritance
			services.AddSingleton<IC, C>();
			services.AddSingleton<IB, B>();
			services.AddSingleton<A>();
		}

		static B2 FactoryRegister(IServiceProvider provider)
		{
			var provided = provider.GetService<IC>();
			var b2 = new B2(provided, "string");
			return b2;
		}

		public void DelegateRegister(IServiceCollection services)
		{
			services.AddSingleton<IC, C>();
			//delegate
			services.AddSingleton<IB, B2>(
					(IServiceProvider provider) =>
					{
						var b2 = new B2(
							provider.GetService<IC>(),
							"Hello World!"
						);
						return b2;
					}
				//or use Factory: FactoryRegister
				);
		}


		public void Main()
		{
			//DI_Design();
			DI_Completely();
		}
	}
}
