using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SomeFunctionalOfCS._Database
{
	internal class ADONET_3
	{
		public string GetConnectionString()
		{
			var configRoot = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("configConnect.json")
				.Build();
			return configRoot["ConfigLink:Connection1"];
		}

		public void OpenConnection()
		{
			using var connection = new SqlConnection(GetConnectionString());
			connection.Open();

			AdapderHandle(connection);

			connection.Close();
		}

		public void AdapderHandle(SqlConnection connection)
		{
			SqlDataAdapter adapter = new SqlDataAdapter();
			adapter.TableMappings.Add("Table", "NhanVien");
			
			//Select
			adapter.SelectCommand = new SqlCommand(@"SELECT NhanvienID,Ten,Ho FROM NhanVien", connection);

			//	INSERT
			adapter.InsertCommand = new SqlCommand(@"Insert into NhanVien (Ten,Ho) VALUES (@Ten,@Ho)", connection);
			adapter.InsertCommand.Parameters.Add("@Ten", SqlDbType.NVarChar, 255, "Ten");
			adapter.InsertCommand.Parameters.Add("@Ho", SqlDbType.NVarChar, 255, "Ho");

			//	Update
			adapter.UpdateCommand = new SqlCommand(@"UPDATE Nhanvien SET Ten=@Ten, Ho=@Ho WHERE NhanVienID=@NhanVienID", connection);
			adapter.UpdateCommand.Parameters.Add(new SqlParameter
			{
				ParameterName = "@Ten",
				SqlDbType = SqlDbType.NVarChar,
				Size = 255,
				SourceColumn = "Ten"
			});
			adapter.UpdateCommand.Parameters.Add(new SqlParameter
			{
				ParameterName = "@Ho",
				SqlDbType = SqlDbType.NVarChar,
				Size = 255,
				SourceColumn = "Ho"
			});
			adapter.UpdateCommand.Parameters.Add(new SqlParameter
			{
				ParameterName = "@NhanVienID",
				SqlDbType = SqlDbType.Int,
				SourceColumn = "NhanVienID",
				SourceVersion = DataRowVersion.Original
			});

			//Delete
			adapter.DeleteCommand = new SqlCommand(@"Delete From NhanVien Where NhanVienID=@NhanVienID",connection);
			adapter.DeleteCommand.Parameters.Add(new SqlParameter()
			{
				ParameterName = "@NhanVienID",
				SqlDbType = SqlDbType.Int,
				SourceColumn = "NhanVienID",
				SourceVersion = DataRowVersion.Original
			});
			var dataSet = new DataSet();
			adapter.Fill(dataSet);
			var table = dataSet.Tables["NhanVien"];

			//	INSERT
			//table.Rows.Add(null,"Sinh","Nguyen");

			//	Update
			//var row = table.Rows[10];
			//row["Ten"] = "Tran";
			//row["Ho"] = "Duat";

			//Delete
			//var row = table.Rows[10];
			//row.Delete();

			//adapter.Update(dataSet);

			ShowTable(table);
		}

		public void ShowTable(DataTable table)
		{
			Console.WriteLine("Bảng: " + table.TableName);
			// Hiện thị các cột
			foreach (DataColumn column in table.Columns)
			{
				Console.Write($"{column.ColumnName,15}");
			}
			Console.WriteLine();

			int numsCol = table.Columns.Count;
			int count = 0;
			foreach (DataRow row in table.Rows)
			{
				Console.Write($"{count,15}");
				for (int i = 0; i < numsCol; i++)
				{
					Console.Write($"{row[i],15}");
				}
				Console.WriteLine();
				count++;
			}

		}

		public void Main()
		{
			OpenConnection();
		}
	}
}
