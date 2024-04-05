using Microsoft.Extensions.Configuration;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Data.SqlClient;

namespace SomeFunctionalOfCS._Database
{
	internal class ADONET_1
	{
		public string GetConnectConfigString()
		{
			var configBuilder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile(@"configConnect.json");
			IConfigurationRoot configurationRoot = configBuilder.Build();
			if (configurationRoot == null) return "Errors";
			return configurationRoot["ConfigLink:Connection1"];
		}
		public void OpenConnect()
		{
			string ConfigString = GetConnectConfigString();
			Console.WriteLine(ConfigString);
			using DbConnection connection = new SqlConnection(ConfigString);
			connection.Open();
			Console.WriteLine(connection.State);
			using DbCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandText = "SELECT * FROM SANPHAM WHERE GIA>100000";
			var reader = command.ExecuteReader();
			Console.WriteLine($"{"Product_Name",10} : {"Price",10}");
			while (reader.Read())
			{
				Console.WriteLine($"{reader["TenSanPham"],10} : {reader["Gia"],10}");
			}

		}

		public void Main()
		{
			OpenConnect();
		}

	}
}
