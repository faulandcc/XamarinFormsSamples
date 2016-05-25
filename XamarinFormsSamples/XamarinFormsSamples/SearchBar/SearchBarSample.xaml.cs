using Xamarin.Forms;

namespace XamarinFormsSamples.SearchBar
{
	public partial class SearchBarSample : ContentPage
	{
		public SearchBarViewModel ViewModel
		{
			get { return this.BindingContext as SearchBarViewModel; }
			set { this.BindingContext = value; }
		}
		public SearchBarSample()
		{
			this.ViewModel = new SearchBarViewModel();
			InitializeComponent();
		}

		private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			if (this.ViewModel.SearchCommand.CanExecute(null))
			{
				this.ViewModel.SearchCommand.Execute(null);
			}
		}
	}
}
