using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinFormsSamples.Common;

namespace XamarinFormsSamples.SearchBar
{
	public class SearchBarViewModel : ViewModelBase
	{
		private string _searchText;
		private readonly List<string> _allItems = new List<string>();
		private readonly ObservableCollection<string> _items; 


		#region properties

		public string SearchText
		{
			get { return _searchText; }
			set
			{
				if (_searchText != value)
				{
					_searchText = value;
					this.OnPropertyChanged();
				}
			}
		}

		public ObservableCollection<string> Items => _items;

		#endregion


		#region commands

		public ICommand SearchCommand { get; private set; }

		#endregion


		#region ctor

		public SearchBarViewModel()
		{
			this.SearchCommand = new Command(this.ExecuteSearchCommand, this.CanExecuteSearchCommand);

			_allItems.Add("January");
			_allItems.Add("February");
			_allItems.Add("March");
			_allItems.Add("April");
			_allItems.Add("May");
			_allItems.Add("June");
			_allItems.Add("July");
			_allItems.Add("August");
			_allItems.Add("September");
			_allItems.Add("October");
			_allItems.Add("November");
			_allItems.Add("December");

			_items = new ObservableCollection<string>(_allItems);
		}

		#endregion


		#region private methods

		protected virtual bool CanExecuteSearchCommand()
		{
			return true;
		}

		protected virtual void ExecuteSearchCommand()
		{
			this.Items.Clear();
			IEnumerable<string> foundItems;
			if (string.IsNullOrEmpty(this.SearchText))
			{
				foundItems = _allItems;
			}
			else
			{
				foundItems = _allItems.Where(p => p.ToLower().Contains(this.SearchText.ToLower()));
			}
			foreach (var foundItem in foundItems)
			{
				this.Items.Add(foundItem);
			}
		}

		#endregion
	}
}
