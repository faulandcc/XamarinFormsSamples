using System;
using Xamarin.Forms;
using XamarinFormsSamples.SearchBar;

namespace XamarinFormsSamples
{
	public partial class StartPage : ContentPage
	{
		public StartPage()
		{
			InitializeComponent();
		}

		private void SearchBarSampleButton_OnClicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new SearchBarSample());
		}
	}
}
