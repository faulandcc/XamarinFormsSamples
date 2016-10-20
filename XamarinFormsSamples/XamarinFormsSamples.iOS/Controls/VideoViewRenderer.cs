using CoreGraphics;
using Foundation;
using HailiosApp.iOS.Renderers;
using MediaPlayer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(XamarinFormsSamples.Video.VideoView), typeof(VideoViewRenderer))]
namespace HailiosApp.iOS.Renderers
{
    public class VideoViewRenderer : ViewRenderer<XamarinFormsSamples.Video.VideoView, UIVideoView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<XamarinFormsSamples.Video.VideoView> e)
        {
            base.OnElementChanged(e);
            e.NewElement.StopAction = () => {
                this.Control.Stop();
            };
            base.SetNativeControl(new UIVideoView(e.NewElement.UriSource, UIScreen.MainScreen.Bounds));
        }
    }
    
    public class UIVideoView : UIView
    {
        private bool _isPlaying = false;
        private MPMoviePlayerController _moviePlayer;

		public UIVideoView (string uriSource, CGRect frame)
        {
            this.AutoresizingMask = UIViewAutoresizing.All;
            this.ContentMode = UIViewContentMode.ScaleToFill;

            _moviePlayer = new MPMoviePlayerController (NSUrl.FromString(uriSource));
            _moviePlayer.View.ContentMode = UIViewContentMode.ScaleToFill;
            _moviePlayer.View.AutoresizingMask = UIViewAutoresizing.All;
            _moviePlayer.RepeatMode = MPMovieRepeatMode.One;
            _moviePlayer.ControlStyle = MPMovieControlStyle.Default;
            _moviePlayer.ScalingMode = MPMovieScalingMode.AspectFit;
			_moviePlayer.MovieControlMode = MPMovieControlMode.Default;
            this.Frame  = _moviePlayer.View.Frame =  frame;
            Add(_moviePlayer.View);
            _moviePlayer.SetFullscreen (true, true);
            _moviePlayer.Play ();
            _isPlaying = true;
        }

        public void Stop()
		{
	        if (_isPlaying)
	        {
		        _moviePlayer.Stop ();
		        _isPlaying = false;
	        }
        }
    }
}