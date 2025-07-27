using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WeatherApplication.ViewModel.Commands
{
	public class SearchCommand : ICommand
	{
		// ViewModel instance to access properties and methods
		public WeatherVM VM { get; set; }

		// Event to notify when the command can execute state changes
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		// Constructor to initialize the ViewModel
		public SearchCommand(WeatherVM vm) 
		{
			VM = vm;
		}

		// Method to determine if the command can execute
		public bool CanExecute(object parameter)
		{
			string query = parameter as string;

			if (string.IsNullOrWhiteSpace(query))
				return false;
			return true;
		}

		// Method to execute the command
		public void Execute(object parameter)
		{
			VM.MakeQuery();
		}
	}
}
	
