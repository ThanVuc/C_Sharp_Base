using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SomeFunctionalOfCS
{
	internal class _Attribute_Type
	{

		//Tạo attribute riêng
		//Attribute này thêm thông tin vào các class, method, ..
		[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
		class motaAttribute : Attribute
		{
			public string? DetailInfo { get; set; }
			public motaAttribute(string info)
			{
				DetailInfo = info;
			}
		}

		[mota("This is a User class, has user infomation")]
		public class User
		{
			[mota("Name Property")]
			[Required(ErrorMessage ="Must type data")]
			[StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage ="Length Must >= 5 and <=50")]
			public string? Name { get; set; }

			[mota("Age Property")]
			[Range(maximum: 100, minimum: 1, ErrorMessage ="must be > 0 and <=100")]
			public int Age { get; set; }

			[mota("Phone Property")]
			[Phone(ErrorMessage ="Invalid Phone Format")]
			public string? Phone { get; set; }

			[mota("Mail Property")]
			[EmailAddress(ErrorMessage = "Invalid Email Format")]
			public string? Mail { get; set; }

			//Add thêm warning vào đánh dấu phương thức đã lỗi thời
			[Obsolete("Loi Thoi!")]
			public void PrintInfo()
			{
				Console.WriteLine(Name);
			}
		}

		static public void TypeInfo(User user)
		{
			// Lấy thông tin type của a
			
			var t = user.GetType();
			Console.WriteLine(t.Name);
			Console.WriteLine("---------Method---------");
			t.GetMethods().ToList().ForEach(mt => Console.WriteLine(mt.Name));
			Console.WriteLine("---------Field----------");
			//Lấy ra các trường public
			t.GetFields().ToList().ForEach(f => Console.WriteLine(f.Name));

		}

		public void PropertiesInfo(User user)
		{
			// Lấy tên các property của user + Giá trị của các property đó
			PropertyInfo[] properties = user.GetType().GetProperties();
			// Trả về 1 Array các propertyInfo, chứa thông tin về 1 trường Property của Class
            foreach (var property in properties)
            {
				string name = property.Name;
				var value = property.GetValue(user);
				Console.WriteLine($"{name} : {value}");
            }
        }

		public void TakeAttribute(User user)
		{
			var t = user.GetType().GetCustomAttributes(typeof(motaAttribute), true);
			motaAttribute mota1 = (motaAttribute)t.First();
			Console.WriteLine($"{user.GetType()} : {mota1.DetailInfo}");
			foreach (PropertyInfo property in user.GetType().GetProperties())
			{
				foreach (var attr in property.GetCustomAttributes(typeof(motaAttribute),false))
				{
					motaAttribute mota = (motaAttribute)attr;
					if (mota != null)
					{
						string name = property.Name;
						var value = property.GetValue(user);
						Console.WriteLine($"{name} : {value} -> {mota.DetailInfo}");
					}
				}
			}
		}

		public void SomeAttributePopular(User user)
		{
			//Require : check Null, StringLength: Set data length, DataType: set type, Range: Limit range
			//Phone: Check Phone, EmailAddress: check Email
			ValidationContext context = new(user);
			List<ValidationResult> lResult = new(); 
			bool result = Validator.TryValidateObject(user, context, lResult, true);
			if (!result)
			{
				lResult.ForEach(er =>
				{
					//First ở đây lấy xong rồi sẽ chuyển tới item tiếp theo trong Iterator
					Console.WriteLine(er.MemberNames.First());
					Console.WriteLine(er.ErrorMessage);
				});
			}
		}

		public void Main()
		{
			User user = new()
			{
				Name = "Nguyen The Sinh",
				Age = 20,
				Phone= "03534@31156",
				Mail= "Sinhhahaha1@@gmail.com"
			};
			//TypeInfo(user);
			//PropertiesInfo(user);
			//TakeAttribute(user);
			SomeAttributePopular(user);
        }
	}
}
