using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SomeFunctionalOfCS
{
	internal class _CreatePackage
	{
		// Nuget: Package storage
		// Download Package: CLI in Nuget
		// Remove: dotnet remove package pakage_name
		// Restore: dotnet restore

		// Tự build packet:
		// 1. Tạo project, tạo các hàm cần sử dụng trong thư viện
		// 2. dotnet build -> sinh ra 1 file: ../prj_name.dll
		// 3. Có thể update 1 số thông tin trong <PropertyGroup> như <PacketID>, <Version>, <Author>.. để rõ ràng
		
		// Dùng các project tự build:
		// dotnet add ../tenprj.csproj(build xong cho) reference ../thuvien.csproj(Tên namespace)

		// publish lên nuget:
		// 1. Tạo tài khoản: profile -> API key
		// 2. Copy API key
		// Notes: Khi build xong, dotnet pack để đóng gói
		// 3. dotnet nuget push .nupkg --api-key <key> --source https://api.nuget.org/v3/index.json
		// 4. Vào packetManaged đẩy lên
		class Product
		{
			public string Name { get; set; }
			public DateTime Expiry { get; set; }
			public string[] Sizes { get; set; }
		}
		public void Main()
		{
			//download Newtonsoft.Json
			string json = @"
			{
				'Name': 'Banh Mi',
				'Expiry': '2008-12-28T00:00:00',
				'Sizes': [
					'Small',
					'Normal'
				]
			}";

			Product m = JsonConvert.DeserializeObject<Product>(json);

			//Console.WriteLine(m.Name);

			JArray array = new JArray();
			array.Add("Manual text");
			array.Add(new DateTime(2000, 5, 23));

			JObject o = new JObject();
			o["MyArray"] = array;
			Console.WriteLine(o["MyArray"]);
		}
	}
}
