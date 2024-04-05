using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeFunctionalOfCS
{
	internal class _Delegate
	{
		//used to reference to method
		//declare
		public delegate void ShowLog(string message);

		public void showInfo(string s)
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("info: " + s);
			Console.ResetColor();
		}

		public void showWarning(string s)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("warning: " + s);
			Console.ResetColor();
		}

		public int Add(int a, int b)
		{
			return a + b;
		}

		public void Test(ShowLog log)
		{
			log("abcxyz");
		}

		public void main()
		{
			ShowLog showLog = showInfo;
			ShowLog showLog1 = showWarning;
			ShowLog showLog2 = showInfo;

			////use deletegate
			//showLog?.Invoke("abc");
			//showLog("abcxyz");

			//// 1 delegate can be assigned by multiple method
			//showLog += showInfo;
			//showLog += showInfo;
			//showLog += showInfo;
			//showLog += showWarning;

			//// We can be remove method in delegate equal "-=" operator, the last method in delegate will remove
			//showLog -= showInfo;

			////canbe add delegate with each other
			//var all = showLog + showLog1 + showLog2;
			//all("Hello World!");

			////Declare deletegate shorthand
			//// Func<parameter1, parameter2,..,Return type> deletegate_var; : Use with func have return type
			//Func<int, int, int> fun;
			//fun = Add;
			//Console.WriteLine(fun(1, 2));

			//// Action<parameter1,...> delegate_var; : use with func not have return type
			//Action<string> show;
			//show = showInfo;
			//show?.Invoke("Hello World!");

			Test(showInfo);

		}
	}
}
