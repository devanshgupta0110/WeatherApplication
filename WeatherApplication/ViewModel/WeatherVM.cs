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

		public ObservableCollection<City> Cities { get; set; }

		private CurrentConditions currentConditions;

		public CurrentConditions CurrentConditions
		{
			get { return currentConditions; }
			set
			{
				currentConditions = value;
				OnPropertyChanged("CurrentConditions");
			}
		}

		private City selectedCity;

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

		public SearchCommand SearchCommand { get; set; }

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

		private async void GetCurrentConditions()
		{
			if (SelectedCity == null) return;
			Query = string.Empty;
			Cities.Clear();
			CurrentConditions = await AccuWeatherHelper.GetCurrentConditions(SelectedCity.Key);
		}

		public async void MakeQuery()
		{ 
		   var cities = await AccuWeatherHelper.GetCities(Query);

		   Cities.Clear();

			foreach (var city in cities)
			{
				Cities.Add(city);
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			System.Diagnostics.Debug.WriteLine($"PropertyChanged: {propertyName}");
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
