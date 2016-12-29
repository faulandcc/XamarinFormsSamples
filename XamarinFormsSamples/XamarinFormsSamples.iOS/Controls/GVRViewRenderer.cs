using CoreGraphics;
using Foundation;
using MediaPlayer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinFormsSamples.GoogleVR;
using XamarinFormsSamples.iOS.Controls;

[assembly: ExportRenderer(typeof(XamarinFormsSamples.GoogleVR.GVRView), typeof(GVRViewRenderer))]
namespace XamarinFormsSamples.iOS.Controls
{
    public class GVRViewRenderer : ViewRenderer<XamarinFormsSamples.GoogleVR.GVRView, UIGVRView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<XamarinFormsSamples.GoogleVR.GVRView> e)
        {
            base.OnElementChanged(e);
            //e.NewElement.StopAction = () =>
            //{
            //    this.Control.Stop();
            //};
            //base.SetNativeControl(new UIVideoView(e.NewElement.UriSource, UIScreen.MainScreen.Bounds));
        }
    }
    
    public class UIGVRView : UIView
    {
        private bool _isPlaying = false;
        private GVRVideoView _videoView;

		public UIGVRView(string uriSource, CGRect frame)
        {
   //         this.AutoresizingMask = UIViewAutoresizing.All;
   //         this.ContentMode = UIViewContentMode.ScaleToFill;

   //         _moviePlayer = new MPMoviePlayerController (NSUrl.FromString(uriSource));
   //         _moviePlayer.View.ContentMode = UIViewContentMode.ScaleToFill;
   //         _moviePlayer.View.AutoresizingMask = UIViewAutoresizing.All;
   //         _moviePlayer.RepeatMode = MPMovieRepeatMode.One;
   //         _moviePlayer.ControlStyle = MPMovieControlStyle.Default;
   //         _moviePlayer.ScalingMode = MPMovieScalingMode.AspectFit;
			//_moviePlayer.MovieControlMode = MPMovieControlMode.Default;
   //         this.Frame  = _moviePlayer.View.Frame =  frame;
   //         Add(_moviePlayer.View);
   //         _moviePlayer.SetFullscreen (true, true);
   //         _moviePlayer.Play ();
            _isPlaying = true;
        }

        public void Stop()
		{
	        if (_isPlaying)
	        {
		        //_moviePlayer.Stop ();
		        _isPlaying = false;
	        }
        }
    }
}