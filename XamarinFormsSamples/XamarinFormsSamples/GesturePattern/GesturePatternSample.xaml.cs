using ContentPage = Xamarin.Forms.ContentPage;

namespace XamarinFormsSamples.GesturePattern
{
	public partial class GesturePatternSample : ContentPage
	{
		public GestureSampleViewModel ViewModel
		{
			get { return this.BindingContext as GestureSampleViewModel; }
			set { this.BindingContext = value; }
		}


		public GesturePatternSample()
		{
			this.ViewModel = new GestureSampleViewModel();

			InitializeComponent();
			this.MyGesturePatternView.CreateTouchInterface();

			// We can use the event to get the complete gesture or bind a view model (see label in XAML).
			this.MyGesturePatternView.GesturePatternCompleted += (sender, args) =>
			{
				DisplayAlert("Gesture", args.GesturePatternValue, "Ok", "Cancel");
			};
		}
	}
}
