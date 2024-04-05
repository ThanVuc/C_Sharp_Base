using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomeFunctionalOfCS._Database
{
	internal class EF_4
	{
		[Table("article")]
		public class Article
		{
			[Key]
			public int ArticleId { set; get; }

			[StringLength(100)]
			public string Title { set; get; }

			[Column(TypeName = "ntext")]
			public string Description { get; set; }

		}

		public class Tag
		{
			[Key]
			[StringLength(20)]
			public string TagId { set; get; }
			[Column(TypeName = "ntext")]
			public string Content { set; get; }
		}

		public class WebContext : DbContext
		{
			public DbSet<Article> articles { set; get; }        // bảng article
			public DbSet<Tag> tags { set; get; }                // bảng tag

			// chuỗi kết nối với tên db sẽ làm  việc đặt là webdb
			public const string ConnectStrring = @"Data Source=localhost;Initial Catalog=webdb;User ID=SA;Password=88888888;
			TrustServerCertificate = True";
			public ILoggerFactory loggerFactory = LoggerFactory.Create(configure =>
			{
				configure.AddConsole();
			});

			protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			{
				optionsBuilder.UseSqlServer(ConnectStrring);
				optionsBuilder.UseLoggerFactory(loggerFactory);       // bật logger
			}
		}

		public void Main()
		{

		}

	}
}
