using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SomeFunctionalOfCS
{
	//readonly: Only assign value to variable in Constructor (or when initializer (not recommend) )
	class ReadOnlyTest
	{
		public readonly string s;
		
		public ReadOnlyTest()
		{
			s = "123";
		}
		
	}

	class OperatorOverloading
	{
		class Vector
		{
			public int _x { get; set; }
			public int _y { get; set; }

			public void ShowCoordinate() => Console.WriteLine($"Coordinate (X,Y): ({_x},{_y})");

			public Vector(int x, int y)
			{
				_x = x;
				_y = y;
			}

			public Vector()
			{
			}

			public static Vector operator+(Vector v1, Vector v2)
			{
				return new Vector(v1._x+v2._x,v1._y+v2._y);
			}

		}

		public void Main()
		{
			Vector v1 = new Vector(1,2);
			Vector v2 = new Vector(3, 4);
			var v3 = v1 + v2;
			v3.ShowCoordinate();
		}
	}

	class IndexerTest
	{
		class Vector
		{
			public int _x { get; set; }
			public int _y { get; set; }

			public void ShowCoordinate() => Console.WriteLine($"Coordinate (X,Y): ({_x},{_y})");

			public Vector(int x, int y)
			{
				_x = x;
				_y = y;
			}

			public Vector()
			{
			}

			public int this[string i]
			{
				set
				{
					switch (i)
					{
						case "sinh":
							_x = value;
							break;
						case "Tu":
							_y = value;
							break;
						default:
							throw new Exception("Only 1-2");
					}
				}

				get
				{
					switch (i)
					{
						case "sinh":
							return _x;
						case "Tu":
							return _y;
						default:
							throw new Exception("Only 1-2");
					}
				}
			}
		}

		public void Main()
		{
			Vector v1 = new Vector(3,4);
			Console.WriteLine(v1["sinh"]);
			Console.WriteLine(v1["Tu"]);
		}
	}

	internal class _Readonly_OverloadOpearator_Indexer
	{
		
		public void Main()
		{
			IndexerTest it = new IndexerTest();
			it.Main();
		}
	}
}
