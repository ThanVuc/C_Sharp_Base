using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Fluent API
namespace SomeFunctionalOfCS._Database
{
	internal class EF_3
	{
		class Product
		{
			public int ProductID { get; set; }
			public string Name { get; set; }
			public float Price { get; set; }

			public int CateID { get; set; }
			public Category category { get; set; }

			public int? CateID1;
			public virtual Category category1 { get; set; }
		}

		class Category
		{
			public int CategoryID { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }

			public ICollection<Product> Products1 { get; set; }

			public int CateDetailID;
			public CategoryDetail categoryDetail;
		}

		class CategoryDetail
		{
			public int CategoryDetailID { get; set; }
			public DateTime Create { get; set; }
			public DateTime Update { get; set; }

			public int CateID;
			public Category category { get; set; }
		}

		class ShopContext : DbContext
		{
			DbSet<Product> Products { get; set; }
			DbSet<Category> Categories { get; set; }

			private string connectionString = @"Server = LocalHost;Database = Shop1;UID = sa;PWD = 88888888;" +
				"TrustServerCertificate = True";

			public ILoggerFactory loggerFactory = LoggerFactory.Create(configure =>
			{
				configure.AddConsole();
			});

			protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			{
				base.OnConfiguring(optionsBuilder);
				optionsBuilder.UseLoggerFactory(loggerFactory)
					.UseSqlServer(connectionString);
			}

			protected override void OnModelCreating(ModelBuilder modelBuilder)
			{
				base.OnModelCreating(modelBuilder);

				modelBuilder.Entity<Product>(entity =>
				{
					entity.ToTable("Product");
					entity.HasKey(e => e.ProductID);
					entity.HasIndex(e => e.Name).IsUnique(true);

					entity.Property(e => e.Name)
						.IsRequired()
						.HasColumnName("ProductName")
						.HasColumnType("nvarchar(50)")
						.IsUnicode();

					entity.Property(e => e.Price)
						.HasColumnType("Money");

					entity.HasOne(e => e.category)
						.WithMany()
						.HasForeignKey("CateID")
						.OnDelete(DeleteBehavior.Cascade)
						.HasConstraintName("fk_Product_Category");

					entity.HasOne(e => e.category1)
						.WithMany(c => c.Products1)
						.OnDelete(DeleteBehavior.NoAction);
				});

				modelBuilder.Entity<Category>(entity =>
				{
					entity.HasKey(e => e.CategoryID);

					entity.Property(e => e.Name)
						.IsRequired()
						.HasColumnType("nvarchar(50)")
						.IsUnicode();

					entity.HasIndex(e => e.Name).IsUnique();

					entity.Property(e => e.Description)
						.HasColumnType("ntext");

					entity.HasOne(e => e.categoryDetail)
						.WithOne(d => d.category)
						.HasForeignKey<CategoryDetail>(e => e.CategoryDetailID)
						.OnDelete(DeleteBehavior.Cascade);

				});

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

		public async Task Main()
		{
			await DeleteShopDB();
			await CreateShopDB();
		}
	}
}
