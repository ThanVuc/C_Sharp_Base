using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomeFunctionalOfCS._Database
{
	internal class EF_1
	{
		[AttributeUsage(AttributeTargets.Property)]
		class GreaterThanZero : ValidationAttribute
		{
			public GreaterThanZero()
			{
				ErrorMessage = "Value must be greater than 0";
			}

			protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
			{
				if (value is int number && number <= 0)
				{
					return new ValidationResult(ErrorMessage);
				}

				return ValidationResult.Success;
			}
		}

		//model
		[Table("Product")]
		class Product
		{
			[Key]
			public string ProductID { get; set; }
			[Required]
			[StringLength(50)]
			public string Name { get; set; }

			[Required]
			[GreaterThanZero]
			public int Price { get; set; }

			[StringLength(50)]
			public string Provider { get; set; }
		}

		//context
		class ProductContext : DbContext
		{
			//storage table
			public DbSet<Product> Products { get; set; }

			private string configString = @" Data Source=localhost;
			Initial Catalog=mydata;
			User ID=sa;
			Password=88888888;
			TrustServerCertificate=True;
			";

			public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => {
				builder
					   //.AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Warning)
					   //.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Debug)
					   .AddConsole();
			});


			//Create Connection
			protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			{
				base.OnConfiguring(optionsBuilder);
				optionsBuilder
					.UseLoggerFactory(loggerFactory)
					.UseSqlServer(configString);
			}
		}

		public async Task CreateDB()
		{
			using (var context = new ProductContext())
			{
				var dbName = context.Database.GetDbConnection().Database;
				Console.WriteLine($"Create DB: {dbName}...");
				bool result = await context.Database.EnsureCreatedAsync();
				if (result)
				{
					Console.WriteLine($"Create {dbName} Successful");
				}
				else
				{
					Console.WriteLine($"Create {dbName} Failed");
				}

			}
		}

		public async Task DeleteDB()
		{
			using (var context = new ProductContext())
			{
				var dbName = context.Database.GetDbConnection().Database;
				Console.WriteLine($"Delete DB: {dbName}...");
				bool result = await context.Database.EnsureDeletedAsync();
				if (result)
				{
					Console.WriteLine($"Delete {dbName} Successful");
				}
				else
				{
					Console.WriteLine($"Delete {dbName} Failed");
				}

			}
		}

		// Insert 1 Product
		public async Task InsertProdut(object p)
		{
			using (var context = new ProductContext())
			{
				await context.Products.AddAsync((Product)p);
				int rows = await context.SaveChangesAsync();
				Console.WriteLine($"Saved {rows} Product");
			}
		}

		//Insert Array of Product
		public async Task InsertProdut(object[] ps)
		{

			using (var context = new ProductContext())
			{
				await context.Products.AddRangeAsync(from p in ps select (Product)p);
				int rows = await context.SaveChangesAsync();
				Console.WriteLine($"Saved {rows} Product");
			}
		}

		//Read
		public async Task ReadProducts()
		{
			using (var context = new ProductContext())
			{
				var products = await context.Products.ToListAsync();

				foreach (var product in products)
				{
					Console.WriteLine($"{product.ProductID,2} {product.Name,15} - {product.Provider}");
				}
				Console.WriteLine();
				Console.WriteLine();

				var result = await (from p in context.Products
									where p.Provider == "CTY A"
									select p).ToListAsync();
				Console.WriteLine("Product in Cty A");
				result.ForEach((product) =>
				{
					Console.WriteLine($"{product.ProductID,2} {product.Name,15} - {product.Provider}");
				});
			}
		}

		// Update
		public async Task RenameProduct(string id, string newName)
		{
			using (var context = new ProductContext())
			{
				var product = await (from p in context.Products where p.ProductID == id select p).FirstOrDefaultAsync();
				if (product != null)
				{
					product.Name = newName;
					Console.WriteLine($"{product.ProductID,2} : {newName}");
					context.SaveChanges();
				}			
			}
		}

		// Delete Product
		public async Task DeleteProduct(string id)
		{
			using (var context = new ProductContext())
			{
				var product = await (from p in context.Products where p.ProductID == id select p).FirstOrDefaultAsync();
				if (product != null)
				{
					context.Remove(product);
					Console.WriteLine($"Deleted {product.Name}");
					context.SaveChanges();
				}
			
			}
		}

		public async Task Main()
		{
			await DeleteDB();
		}
	}
}
