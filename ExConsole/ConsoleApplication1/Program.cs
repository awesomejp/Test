using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
	public class Program
	{
		public static void Main(string[] args)
		{
			DerivedClass md = new DerivedClass();
			DerivedClass md1 = new DerivedClass(1);

			//var task = DoWork();
			//Console.WriteLine("DoWork called");
			//// We call .Result because Main cannot be async
			//Console.WriteLine(task.Result);
			//Console.ReadLine();
		}

		public class BaseClass
		{
			int num;

			public BaseClass()
			{
				Console.WriteLine("in BaseClass()");
			}

			public BaseClass(int i)
			{
				num = i;
				Console.WriteLine("in BaseClass(int i)");
			}

			public int GetNum()
			{
				return num;
			}
		}

		public class DerivedClass : BaseClass
		{
			// This constructor will call BaseClass.BaseClass() 
			public DerivedClass()
				: base()
			{
				Console.WriteLine("in DerivedClass()");
			}

			// This constructor will call BaseClass.BaseClass(int i) 
			public DerivedClass(int i)
				: base(i)
			{
				Console.WriteLine("in DerivedClass(int i)");
			}
		}
	}
}
