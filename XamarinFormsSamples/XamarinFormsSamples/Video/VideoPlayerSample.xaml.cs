using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamarinFormsSamples.Video
{
	public partial class VideoPlayerSample : ContentPage
	{
		public VideoPlayerSample()
		{
			InitializeComponent();
			this.Appearing += OnAppearing;
		}

		private void OnAppearing(object sender, EventArgs eventArgs)
		{
			VideoView.PlayPause();
		}
	}
}
