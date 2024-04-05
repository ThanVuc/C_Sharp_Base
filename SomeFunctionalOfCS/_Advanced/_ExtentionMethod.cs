using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeFunctionalOfCS
{
	static class Extention
	{

		/*
			Extention Method only can be create equal static class and static method
		 */

		public static void Print(this string s, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(s);
		}

		public static double Cos(this double x)
		{
			return Math.Cos(x);
		}
	}

	internal class _ExtentionMethod
	{
		public void main()
		{
			double x = 12.2;
			Console.WriteLine(x.Cos());
		}
	}
}
