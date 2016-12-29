using System;
using Xamarin.Forms;

namespace XamarinFormsSamples.Video
{
    public class VideoView : View
    {
        public Action PlayPauseAction;
        public Action StopAction;

        public event EventHandler VideoPrepared;
        public void OnVideoPrepared()
        {
            this.VideoPrepared?.Invoke(this, new EventArgs());
        }

        public VideoView()
        {
            
        }

        public static readonly BindableProperty UriSourceProperty =
            BindableProperty.Create<VideoView, string>(
                p => p.UriSource, string.Empty);

        public string UriSource
        {
            get { return (string)GetValue(UriSourceProperty); }
            set { SetValue(UriSourceProperty, value); }
        }

        public void Stop()
        {
            if (StopAction != null)
                StopAction();
        }

        public void PlayPause()
        {
            if (PlayPauseAction != null)
            {
                PlayPauseAction();
            }
        }
    }
}
