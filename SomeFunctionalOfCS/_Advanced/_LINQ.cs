using static SomeFunctionalOfCS._LINQ;

namespace SomeFunctionalOfCS
{
	internal class _LINQ
	{
		public class Product
		{
			public int ID { set; get; }
			public string Name { set; get; }         // tên
			public double Price { set; get; }        // giá
			public string[] Colors { set; get; }     // các màu sắc
			public int Brand { set; get; }           // ID Nhãn hiệu, hãng
			public Product(int id, string name, double price, string[] colors, int brand)
			{
				ID = id; Name = name; Price = price; Colors = colors; Brand = brand;
			}
			// Lấy chuỗi thông tin sản phẳm gồm ID, Name, Price
			override public string ToString()
			   => $"{ID,3} {Name,12} {Price,5} {Brand,2} {string.Join(",", Colors)}";

		}

		public class Brand
		{
			public string Name { set; get; }
			public int ID { set; get; }
		}	

		public void Main()
		{
			var brands = new List<Brand>() {
				new Brand{ID = 1, Name = "Công ty AAA"},
				new Brand{ID = 2, Name = "Công ty BBB"},
				new Brand{ID = 4, Name = "Công ty CCC"},
			};

			var products = new List<Product>()
			{
				new (1, "Bàn trà",    400, new string[] {"Xám", "Xanh"},         2),
				new (2, "Tranh treo", 400, new string[] {"Vàng", "Xanh"},        1),
				new (3, "Đèn trùm",   500, new string[] {"Trắng"},               3),
				new (4, "Bàn học",    200, new string[] {"Trắng", "Xanh"},       1),
				new (5, "Túi da",     300, new string[] {"Đỏ", "Đen", "Vàng"},   2),
				new (6, "Giường ngủ", 500, new string[] {"Trắng"},               2),
				new (7, "Tủ áo",      450, new string[] {"Trắng"},               3),
			};

			//APILINQ(products, brands);
			//ReviewAPILINQ(products,brands);
			//SyntaxLINQ(products, brands);
		}

		public void APILINQ(List<Product> products, List<Brand> brands)
		{
			products.Select(
				(p) =>
				{
					return p.Name;
				}
			);

			products.Where(
				(p) =>
				{
					return p.Name.Contains("tr");
				}
			);

			products.SelectMany(
				(p) =>
				{
					return p.Colors;
				}
			);

			products.Min((p) => p.Price);

			products.Join(
				brands,
				//if p==b -> delegate(p,b)
				p => p.Brand,
				b => b.ID,
				(p, b) =>
				{
					return new
					{
						product_name = p.Name,
						brand = b.Name
					};
				}
			);

			brands.GroupJoin(
				products,
				b => b.ID,
				p => p.Brand,
				//brand, IEnumurable
				(brand, product) =>
				{
					return new
					{
						brand = brand.Name,
						pros = product
					};
				}
			);

			//Take: Lấy ra n phần tử đầu tiên
			products.Take(4);//.ToList().ForEach(l => Console.WriteLine(l));


			//Skip: Bỏ qua n phần tử đầu tiên
			products.Skip(2);//.ToList().ForEach(l => Console.WriteLine(l));

			//OrderBy
			products.OrderBy(
				p => p.Price
			);//.ToList().ForEach(p => Console.WriteLine(p));
			products.OrderByDescending(p => p.Price);//.ToList().ForEach(p => Console.WriteLine(p));

			//Đảo ngược, không cần return
			products.Reverse();

			//GroupBy: Nhóm các sản phẩm có cùng giá trị

			products.GroupBy(
					p => p.Price
				);

			//dictinct: Loại bỏ mấy thằng trùng nhau(duy nhất)
			products.SelectMany(p => p.Colors).Distinct();//.ToList().ForEach(p => Console.WriteLine(p));

			//Single, SingleOrDefault: nhiều thằng or not found => Lỗi, trả về đúng 1 thằng. Default: Không tìm thấy => null
			products.Single(p => p.Price == 200);
			//products.Single(p => p.Price==1000);
			//products.SingleOrDefault(p => p.Price==1000);

			//Any: Kiểm tra xem thằng nào đó có tồn tại không.
			products.Any(p => p.Price == 200);
			products.Any(p => p.Price == 1);

			//All: Kiểm tra xem tất cả các sản phẩm có thỏa 1 condition nào đó không.
			products.All(p => p.Price >= 200);

			//Count: Đếm tất cả các thằng có mặt trong product(phế), or nhận 1 delegate, đếm những phần tử thỏa condition
			products.Count();
			var count = products.Count(p => p.Price >= 300);
			Console.WriteLine(count);

			//foreach (var group in kq)
			//{
			//	Console.WriteLine(group.Key);
			//	foreach (var p in group)
			//	{
			//		Console.WriteLine(p);
			//	}
			//}
		}

		public void ReviewAPILINQ(List<Product> products, List<Brand> brands)
		{
			//in ra tên sản phẩm, màu sắc, thương hiệu có giá nằm giữa 300 và 400, giảm dần theo giá
			var kq = products.OrderByDescending(p => p.Price)
					 .Join(brands, p => p.Brand, b => b.ID, (p, b) => new
					 {
						 productName = p.Name,
						 color = p.Colors.Select(s => s),
						 brand = b.Name,
						 price = p.Price
					 }).Where(p => 300 <= p.price && p.price <= 400);
            foreach (var item in kq)
            {
				Console.Write(item.productName + ", ");
                foreach (var item1 in item.color)
                {
					Console.Write(item1 + " ");
                }
				Console.WriteLine(", " + item.brand + " " + item.price);

            }

		}

		public void SyntaxLINQ(List<Product> products, List<Brand> brands)
		{
			/*
				1) Xác định nguồn: from ele in IEnumerable
					query
				2) Lấy dữ liệu: select, group by,..
			 */
			//gia = 400, lay name
			var qr = from product in products
					 where product.Price == 400
					 select product.Name;
			//qr.ToList().ForEach(a => Console.WriteLine(a));


			//gia ==400, lay xanh
			var qr1 = from product in products
					 from color in product.Colors
					 where product.Price <= 500 && color == "Xanh"
					 orderby product.Price
					 select product;
			//qr1.ToList().ForEach(a => Console.WriteLine(a));

			//Nhóm các sản phẩm theo giá, trả về giá + các sản phẩm + số lượng
			var qr2 = from product in products
					  group product by product.Price into gr
					  orderby gr.Key
					  let count= gr.Count()
					  select new
					  {
						  price= gr.Key,
						  products= gr.ToList(),
						  nums= count
					  };

			//        foreach (var item in qr2)
			//        {
			//Console.WriteLine($"{item.price}, {item.nums} Product: ");
			//            foreach (var item1 in item.products)
			//            {
			//	Console.WriteLine(item1);
			//            }
			//        }

			// Lấy tên thương hiện những sp có giá từ 400 -> 500, Nếu không có tham chiếu tới brand thì bỏ
			var qr3 = from product in products
					  join brand in brands on product.Brand equals brand.ID
					  where product.Price >= 400 && product.Price <= 500
					  orderby product.Price
					  select new
					  {
						  productName = product.Name,
						  price = product.Price,
						  brand = brand.Name
					  };
			//qr3.ToList().ForEach(item => Console.WriteLine(item));

			// Lấy tên thương hiện những sp có giá từ 400 -> 500, Nếu không có tham chiếu tới brand trả về null vẫn lấy
			var qr4 = from product in products
					  join brand in brands on product.Brand equals brand.ID into t
					  from b in t.DefaultIfEmpty()
					  where product.Price >= 400 && product.Price <= 500
					  orderby product.Price
					  select new
					  {
						  productName = product.Name,
						  price = product.Price,
						  brand = (b!=null) ? b.Name : "None"
					  };
			qr4.ToList().ForEach(item => Console.WriteLine(item));


		}

	}
}
