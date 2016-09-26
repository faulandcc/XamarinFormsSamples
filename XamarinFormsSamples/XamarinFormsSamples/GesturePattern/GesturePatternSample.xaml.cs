using ContentPage = Xamarin.Forms.ContentPage;

namespace XamarinFormsSamples.GesturePattern
{
	public partial class GesturePatternSample : ContentPage
	{
		public GesturePatternSample()
		{
			InitializeComponent();
			this.MyGesturePatternView.CreateTouchInterface();
		}
	}
}
