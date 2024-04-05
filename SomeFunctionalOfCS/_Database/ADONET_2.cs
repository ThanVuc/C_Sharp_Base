using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeFunctionalOfCS._Database
{
	internal class ADONET_2
	{
		public string getConnectString()
		{
			var configRoot = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("configConnect.json").Build();
			return configRoot["ConfigLink:Connection1"];
		}
		public void OpenConnect()
		{
			string connectString = getConnectString();
			DbConnection connection = new SqlConnection(connectString);

			connection.Open();

			HandleSql(connection);

			connection.Close();
		}

		public void HandleSql(DbConnection connection)
		{
			var cmd = new SqlCommand("getProducts",(SqlConnection)connection);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(new SqlParameter()
			{
				ParameterName = "@id",
				SqlDbType = SqlDbType.Int,
				Value = 5
			});
			
			using var reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					Console.WriteLine($"{reader["DanhmucID"]} : {reader["TenSanPham"]}");
				}
			} else
			{
				Console.WriteLine("None Result return");
			}
		}

		public void Main()
		{
			OpenConnect();
		}


	}
}
