using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExWpf
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			MessageBox.Show(System.Reflection.Assembly.GetExecutingAssembly().Location);

			InitializeComponent();
		}

		// VOID EXAMPLE
		private async void button1_Click(object sender, RoutedEventArgs e)
		{
			textBox1.Clear();

			// Start the process and await its completion. DriverAsync is a 
			// Task-returning async method.
			await DriverAsync();

			// Say goodbye.
			textBox1.Text += "\r\nAll done, exiting button-click event handler.";
		}

		async Task DriverAsync()
		{
			// Task<T> 
			// Call and await the Task<T>-returning async method in the same statement.
			int result1 = await TaskOfT_MethodAsync();

			// Call and await in separate statements.
			Task<int> integerTask = TaskOfT_MethodAsync();

			// You can do other work that does not rely on integerTask before awaiting.
			textBox1.Text += String.Format("Application can continue working while the Task<T> runs. . . . \r\n");

			int result2 = await integerTask;

			// Display the values of the result1 variable, the result2 variable, and
			// the integerTask.Result property.
			textBox1.Text += String.Format("\r\nValue of result1 variable:   {0}\r\n", result1);
			textBox1.Text += String.Format("Value of result2 variable:   {0}\r\n", result2);
			textBox1.Text += String.Format("Value of integerTask.Result: {0}\r\n", integerTask.Result);

			// Task
			// Call and await the Task-returning async method in the same statement.
			await Task_MethodAsync();

			// Call and await in separate statements.
			Task simpleTask = Task_MethodAsync();

			// You can do other work that does not rely on simpleTask before awaiting.
			textBox1.Text += String.Format("\r\nApplication can continue working while the Task runs. . . .\r\n");

			await simpleTask;
		}

		// TASK<T> EXAMPLE
		async Task<int> TaskOfT_MethodAsync()
		{
			// The body of the method is expected to contain an awaited asynchronous
			// call.
			// Task.FromResult is a placeholder for actual work that returns a string.
			var today = await Task.FromResult<string>(DateTime.Now.DayOfWeek.ToString());

			// The method then can process the result in some way.
			int leisureHours;
			if (today.First() == 'S')
				leisureHours = 16;
			else
				leisureHours = 5;

			// Because the return statement specifies an operand of type int, the
			// method must have a return type of Task<int>.
			return leisureHours;
		}


		// TASK EXAMPLE
		async Task Task_MethodAsync()
		{
			// The body of an async method is expected to contain an awaited 
			// asynchronous call.
			// Task.Delay is a placeholder for actual work.
			await Task.Delay(2000);
			// Task.Delay delays the following line by two seconds.
			textBox1.Text += String.Format("\r\nSorry for the delay. . . .\r\n");

			// This method has no return statement, so its return type is Task.  
		}
	}
}