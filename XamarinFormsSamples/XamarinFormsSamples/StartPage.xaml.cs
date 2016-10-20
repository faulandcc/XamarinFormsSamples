using System;
using Xamarin.Forms;
using XamarinFormsSamples.GesturePattern;
using XamarinFormsSamples.SearchBar;
using XamarinFormsSamples.Video;

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

		private void GesturePatternSampleButton_OnClicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new GesturePatternSample());
		}

		private void VideoPlayerSample_OnClicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new VideoPlayerSample());
		}
	}
}
