using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ExConsole
{
	class Program
	{
		//public static double Floor(decimal value, int decimalPlaces)
		//{
		//	double adjustment = Math.Pow(10, decimalPlaces);
		//	return Math.Floor(value * adjustment) / adjustment;
		//}

		static void Main()
		{
			double j = 100;
			//for (double i = 0.0; i < 20; i = i + 0.1)
			{
				//double bb = 11231 / 1000.0;
				//var aaa = String.Format("{0:0.00}", Math.Ceiling(bb * j - 0.005));

				double aaa = 15530;
				string result = GetConvertedMoney(aaa, "1000");
				Console.WriteLine(result);
			}


			//Traffic traffic = new Traffic();

			//// 2개의 쓰레드 구동
			//Thread v = new Thread(traffic.ProcessVertical);
			//Thread h = new Thread(traffic.ProcessHorizontal);
			//v.Start();
			//h.Start();

			//// 메인쓰레드에서 데이타 전송
			//for (int i = 0; i < 30; i += 3)
			//{
			//	traffic.AddVertical(new int[] { i, i + 1, i + 2 });
			//	traffic.AddHorizontal(new int[] { i, i + 1, i + 2 });
			//	Thread.Sleep(10);
			//}

			//Thread.Sleep(1000);
			//traffic.Running = false;
		}

		public static string GetConvertedMoney(double money, string rateType)
		{
			var moneyResult = Decimal.Zero;
			var rateResult = Decimal.Zero;

			var isParsedMoney = Decimal.TryParse(money.ToString(), out moneyResult);
			var isParsedRate = Decimal.TryParse(rateType.ToString(), out rateResult);

			if (isParsedMoney == false || isParsedRate == false)
				return "0";

			moneyResult /= rateResult;

			// Floor 오류를 해결하기 위해 Decimal.Floor를 사용 (부동 소수점에 대한 정밀도를 높일 수 있다.)
			var strValue = string.Format("{0:0.00}", Decimal.Floor(moneyResult * 100) / 100);
			return strValue;
		}
	}

	class Traffic
	{
		private bool _running = true;

		// 상하, 좌우 통행 신호 역활을 하는 AutoResetEvent 이벤트들
		private AutoResetEvent _evtVert = new AutoResetEvent(true);
		private AutoResetEvent _evtHoriz = new AutoResetEvent(false);

		private Queue<int> _Qvert = new Queue<int>();
		private Queue<int> _Qhoriz = new Queue<int>();

		// 상하방향의 큐 데이타 처리
		// Vertical 방향의 처리 신호(_evtVert)를 받으면
		// Vertical 큐의 모든 큐 아이템을 처리하고
		// 좌우방향 처리 신호(_evtHoriz)를 보냄
		public void ProcessVertical()
		{
			while (_running)
			{
				// Vertical 방향 처리 신호 기다림
				_evtVert.WaitOne();

				// Vertical 큐의 모든 데이타 처리
				// 큐는 다른 쓰레드에서 엑세스 가능하므로 lock을 건다
				lock (_Qvert)
				{
					while (_Qvert.Count > 0)
					{
						int val = _Qvert.Dequeue();
						Console.WriteLine("Vertical : {0}", val);
					}
				}

				// Horizontal 방향 처리 신호 보냄
				_evtHoriz.Set();
			}

			Console.WriteLine("ProcessVertical : Done");
		}

		// 좌우방향의 큐 데이타 처리
		// Horizontal 방향의 처리 신호(_evtHoriz)를 받으면
		// Horizontal 큐의 모든 큐 아이템을 처리하고
		// 상하방향 처리 신호(_evtHoriz)를 보냄
		public void ProcessHorizontal()
		{
			while (_running)
			{
				_evtHoriz.WaitOne();

				lock (_Qhoriz)
				{
					while (_Qhoriz.Count > 0)
					{
						int val = _Qhoriz.Dequeue();
						Console.WriteLine("Horizontal : {0}", val);
					}
				}

				_evtVert.Set();
			}

			Console.WriteLine("ProcessHorizontal : Done");
		}

		public bool Running
		{
			get { return _running; }
			set { _running = value; }
		}

		public void AddVertical(int[] data)
		{
			lock (_Qvert)
			{
				foreach (var item in data)
				{
					_Qvert.Enqueue(item);
				}
			}
		}

		public void AddHorizontal(int[] data)
		{
			lock (_Qhoriz)
			{
				foreach (var item in data)
				{
					_Qhoriz.Enqueue(item);
				}
			}
		}
	}
}
