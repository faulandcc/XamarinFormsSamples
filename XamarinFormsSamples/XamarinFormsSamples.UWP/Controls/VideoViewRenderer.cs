using System;
using Windows.Media.Playback;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using XamarinFormsSamples.GesturePattern;
using XamarinFormsSamples.UWP.Controls;
using XamarinFormsSamples.Video;

[assembly: ExportRenderer(typeof(XamarinFormsSamples.Video.VideoView), typeof(VideoViewRenderer))]
namespace XamarinFormsSamples.UWP.Controls
{

	public class VideoViewRenderer : ViewRenderer<XamarinFormsSamples.Video.VideoView, MediaElement>
	{
		private MediaElement _mediaElement;
		private bool _isPlaying;

		#region Overrides of ViewRenderer<VideoView,MediaElement>

		protected override void OnElementChanged(ElementChangedEventArgs<VideoView> e)
		{
			base.OnElementChanged(e);

			e.NewElement.StopAction = () =>
			{
				_mediaElement.Stop();
				_isPlaying = false;
			};
			e.NewElement.PlayPauseAction = () =>
			{
				if (_isPlaying)
				{
					_mediaElement.Pause();
					_isPlaying = false;
				}
				else
				{
					_mediaElement.Play();
					_isPlaying = true;
				}
			};

			_mediaElement = new MediaElement();
			_mediaElement.Source = new Uri(e.NewElement.UriSource);
			_mediaElement.AreTransportControlsEnabled = true;
			base.SetNativeControl(_mediaElement);
		}

		#endregion
	}
}