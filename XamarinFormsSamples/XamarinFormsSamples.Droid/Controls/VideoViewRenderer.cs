using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinFormsSamples.Droid.Controls;

[assembly: ExportRenderer(typeof(XamarinFormsSamples.Video.VideoView), typeof(VideoViewRenderer))]
namespace XamarinFormsSamples.Droid.Controls
{
    public class VideoViewRenderer : ViewRenderer<XamarinFormsSamples.Video.VideoView, VideoView>
    {
        VideoView _videoview;


        public VideoViewRenderer()
        {
        }
        
        protected override void OnElementChanged(ElementChangedEventArgs<XamarinFormsSamples.Video.VideoView> e)
        {
            base.OnElementChanged(e);
            e.NewElement.StopAction = () =>
            {
                this._videoview.StopPlayback();
            };
            e.NewElement.PlayPauseAction = () =>
            {
                if (this._videoview.IsPlaying)
                {
                    this._videoview.Pause();
                }
                else
                {
                    this._videoview.Start();
                }
            };

            _videoview = new VideoView(Context);
            _videoview.Prepared += (sender, args) =>
            {
                e.NewElement.OnVideoPrepared();
            };
            Android.Net.Uri uri = Android.Net.Uri.Parse(e.NewElement.UriSource.Replace("https", "http"));
            _videoview.SetVideoURI(uri);

            base.SetNativeControl(_videoview);
            Control.Layout(0, 0, 200, 200);

            MediaController vidControl = new MediaController(Context);
            vidControl.SetAnchorView(_videoview);
            _videoview.SetMediaController(vidControl);
        }
    }
}