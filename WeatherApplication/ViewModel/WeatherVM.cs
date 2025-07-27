using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApplication.Model;
using WeatherApplication.ViewModel.Commands;
using WeatherApplication.ViewModel.Helpers;



namespace WeatherApplication.ViewModel
{
	public class WeatherVM : INotifyPropertyChanged
	{

		// Query string for searching cities.
	
		private string query;
		public string Query
		{
			get { return query; }
			set
			{
				if (query != value)
				{
					query = value;
					OnPropertyChanged("Query");
				}
			}
		}

		// Collection of cities that match the search query.
		public ObservableCollection<City> Cities { get; set; }

		// Current conditions for the selected city.
		private CurrentConditions currentConditions;

		// currentConditions property to hold the current weather conditions.
		public CurrentConditions CurrentConditions
		{
			get { return currentConditions; }
			set
			{
				currentConditions = value;
				OnPropertyChanged("CurrentConditions");
			}
		}

		// Selected city from the list of cities.
		private City selectedCity;

		// selectedCity property to hold the currently selected city.
		public City SelectedCity
		{
			get { return selectedCity; }
			set
			{
				if (selectedCity != value)
				{
					selectedCity = value;
					OnPropertyChanged("SelectedCity");
					GetCurrentConditions();
				}
			}
		}

		// Command to search for cities based on the query.
		public SearchCommand SearchCommand { get; set; }

		// Constructor for the WeatherVM class.
		public WeatherVM()
		{
			if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
			{ 
			SelectedCity = new City
			{
				LocalizedName = "New York"
			};
			CurrentConditions = new CurrentConditions
			{
				WeatherText = "Partly Cloudy",
				Temperature = new Temperature
				{
					Metric = new Units { Value = "21" },
					
				}
				
			};
			}

			SearchCommand = new SearchCommand(this);

			Cities = new ObservableCollection<City>();
		}

		// Method to get the current weather conditions for the selected city.
		private async void GetCurrentConditions()
		{
			if (SelectedCity == null) return;
			Query = string.Empty;
			Cities.Clear();
			CurrentConditions = await AccuWeatherHelper.GetCurrentConditions(SelectedCity.Key);
		}

		// Method to make a query to search for cities based on the query string.
		public async void MakeQuery()
		{ 
		   var cities = await AccuWeatherHelper.GetCities(Query);

		   Cities.Clear();

			foreach (var city in cities)
			{
				Cities.Add(city);
			}
		}

		// INotifyPropertyChanged implementation to notify the view of property changes.
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			System.Diagnostics.Debug.WriteLine($"PropertyChanged: {propertyName}");
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
