namespace SomeFunctionalOfCS
{
	internal class _Lambda
	{
		//lambda: anonymous function. Is a method but missing name
		//syntax: (declare var) => expression; (or {code})
		//lambda canbe use to assign deletegate, pass to method with role is parameters..
		public delegate void ShowLog(string message);
		public void main()
		{
			//lambda to assign delegate
			ShowLog showLog = (string s) => Console.WriteLine("Info: " + s);
			showLog("abcxyz");

			//with Func and Action
			Func<int, int, int> sum = (int x, int y) => x + y;
			Console.WriteLine(sum(1, 5));

			Action<string> show = (string s) => Console.WriteLine("Show: " + s);
			show("123123");

			//definition method
			int Sum(int x, int y) => x + y;
			Console.WriteLine(Sum(12, 123));
		}
	}
}
