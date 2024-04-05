using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SomeFunctionalOfCS
{
	internal class _FileHandle
	{
		class Product
		{
			public int ID { get; set; }
			public double Price { get; set; }
			public string? Name { get; set; }

			public void Save(Stream stream)
			{
				var bytes_id = BitConverter.GetBytes(ID);
				stream.Write(bytes_id,0,4);

				var bytes_price = BitConverter.GetBytes(Price);
				stream.Write(bytes_price, 0, 8);

				var bytes_name = Encoding.UTF8.GetBytes(Name);
				var bytes_length = BitConverter.GetBytes(bytes_name.Length);
				stream.Write(bytes_length, 0, 4);
				stream.Write(bytes_name, 0, bytes_name.Length);
			}

			public void EnCode(Stream stream)
			{
				//ID
				byte[] bytes_id = new byte[4];
				stream.Read(bytes_id, 0, 4);
				ID = BitConverter.ToInt32(bytes_id);

				//Price
				byte[] bytes_price = new byte[8];
				stream.Read(bytes_price, 0, 8);
				Price = BitConverter.ToDouble(bytes_price);

				//Name
				byte[] bytes_length = new byte[4];
				stream.Read(bytes_length,0,4);
				int count = BitConverter.ToInt32(bytes_length);
				
				byte[] bytes_name = new byte[count];
				stream.Read(bytes_name, 0, count);
				Name = Encoding.UTF8.GetString(bytes_name);
			}
		}
		public void Main()
		{
			Product p = new Product()
			{
				ID= 1,
				Name= "Chao Long",
				Price= 105.5
			};
			string filepath = "test.txt";
			using (var fileStream = new FileStream(path: filepath, mode: FileMode.Open, access: FileAccess.ReadWrite, share: FileShare.Read))
			{
				p.EnCode(fileStream);
			}
			Console.WriteLine($"ID: {p.ID}, Name:{p.Name}, Price:{p.Price}");
				

			//using (var fileStream = new FileStream(path: filepath, mode: FileMode.Open, access: FileAccess.ReadWrite, share: FileShare.Read))
			//{
			//	byte[] buffer = Encoding.UTF8.GetBytes(s);
			//	fileStream.Write(buffer,0,buffer.Length);


				//byte[] buffer = new byte[256];
				//int nums = fileStream.Read(buffer, 0, buffer.Length);
				//string _s = Encoding.UTF8.GetString(buffer, 0, nums);
				//Console.WriteLine(_s);
			//}

			//using (var sr= new StreamReader(filepath))
			//{
			//	string line;
			//	while ((line = sr.ReadLine()) != null)
			//	{
			//		Console.WriteLine(line + line.Length);
			//	}
			//}

			//using (var sw= new StreamWriter(filepath))
			//{
			//	sw.WriteLine(s);
			//	Console.WriteLine("\nDa ghi vao file");
			//	sw.Close();
			//}
		}
	}
}
