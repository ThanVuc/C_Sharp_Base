using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomeFunctionalOfCS._Database
{
	internal class EF_2
	{
		//Model Product
		[Table("Product")]
		class Product
		{
			public Product()
			{
			}

			public Product(string name, decimal price, int categoryID)
			{
				Name = name;
				Price = price;
				CateID = categoryID;
			}
			public Product(string name, decimal price, Category category)
			{
				Name = name;
				Price = price;
				Category = category;
			}



			[Key]
			public int ProductID { get; set; }

			[Required]
			[StringLength(50)]
			[Column("ProductName")]
			public string Name { get; set; }

			[Column(TypeName = "Money")]
			public Decimal Price { get; set; }

			// Easy to Access, Assign, Query
			public int CateID { set; get; }

			[ForeignKey("CateID")]
			public Category Category { set; get; }

			public int? CateID1 { set; get; }
			[ForeignKey("CateID1")]
			[InverseProperty("Products")]
			public virtual Category Category1 { set; get; }
		}

		[Table("Category")]
		class Category
		{
			public Category()
			{
			}

			public Category(string name, string description)
			{
				Name = name;
				Description = description;
			}

			[Key]
			public int CategoryID { get; set; }

			[Column("CategoryName")]
			public string Name { get; set; }

			[Column(TypeName = "ntext")]
			public string Description { get; set; }

			public List<Product> Products { get; set; }
		}

		class ShopContext : DbContext
		{
			public DbSet<Product> Products { get; set; }
			public DbSet<Category> Category { get; set; }

			private string connectString = "Server = localhost;Database = Shop;UID = sa;PWD = 88888888" +
				";TrustServerCertificate = true";

			public ILoggerFactory loggerFactory = LoggerFactory.Create(configure => configure.AddConsole());

			protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			{
				base.OnConfiguring(optionsBuilder);
				optionsBuilder
					.UseLoggerFactory(loggerFactory)
					.UseSqlServer(connectString);
			}
		}

		public async Task CreateShopDB()
		{
			using (var context = new ShopContext())
			{
				var dbName = context.Database.GetDbConnection().Database;
				Console.WriteLine($"Start Create DB: {dbName}");

				var status = await context.Database.EnsureCreatedAsync();
				if (status)
					Console.WriteLine($"Create {dbName} Successful");
				else
					Console.WriteLine($"Create {dbName} Fail");
			}
		}

		public async Task DeleteShopDB()
		{
			using (var context = new ShopContext())
			{
				var dbName = context.Database.GetDbConnection().Database;
				Console.WriteLine($"Start Delete DB: {dbName}");

				var status = await context.Database.EnsureDeletedAsync();
				if (status)
					Console.WriteLine($"Delete {dbName} Successful");
				else
					Console.WriteLine($"Delete {dbName} Fail");
			}
		}

		public async Task InsertRecord()
		{
			using var context = new ShopContext();
			var cateList = new List<Category>()
			{
				new ("Electric","This Is Electric Products"),
				new ("Family Device","Collection of Family Device"),
				new ("Toys","Toy of Children")
			};

			await context.Category.AddRangeAsync(cateList);
			context.SaveChanges();

			var eleCate = (from c in context.Category where c.CategoryID == 1 select c).FirstOrDefault();
			var toyCate = (from c in context.Category where c.CategoryID == 3 select c).FirstOrDefault();


			List<Product> productList = new List<Product>()
			{
				new Product("KeyBoard",200000,1),
				new Product("Broom",10000,2),
				new Product("Plastic Gun",10000,3),
				new Product("Cable Network",40000,eleCate),
				new Product("puzzle",50000,toyCate)
			};

			await context.Products.AddRangeAsync(productList);
			Console.WriteLine("Inserted to DB");
			context.SaveChanges();

		}

		public async Task TakeReference()
		{
			using var context = new ShopContext();
			var product = await (from p in context.Products where p.CateID == 3 select p).FirstOrDefaultAsync();

			var e = context.Entry(product);
			await e.Reference(p => p.Category).LoadAsync();

			if (product.Category != null)
			{
				Console.WriteLine($"{product.Category.Name} : {product.Category.Description}");
			} else
			{
				Console.WriteLine("Not exist!");
			}
		}

		public async Task TakeCollectionNavigation()
		{
			using var context = new ShopContext();
			var cate = await (from c in context.Category where c.CategoryID == 3 select c).FirstOrDefaultAsync();

			var e = context.Entry(cate);
			await e.Collection(c => c.Products).LoadAsync();

			if (cate.Products != null)
			{
				cate.Products.ForEach(p =>
				{
					Console.WriteLine($"{p.Name} : {p.Price}");
				});
			}
			else Console.WriteLine("Not exist!");
		}

		public async Task Main()
		{
			await DeleteShopDB();
			await CreateShopDB();
			//await InsertRecord();
			//await TakeCollectionNavigation();
		}
	}
}
