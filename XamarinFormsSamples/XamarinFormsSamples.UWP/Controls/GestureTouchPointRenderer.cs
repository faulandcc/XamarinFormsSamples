using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using XamarinFormsSamples.GesturePattern;
using XamarinFormsSamples.UWP.Controls;

//[assembly: ExportRenderer(typeof(GestureTouchPoint), typeof(GestureTouchPointRenderer))]
namespace XamarinFormsSamples.UWP.Controls
{

	public class GestureTouchPointRenderer : LabelRenderer
	{
		#region Overrides of LabelRenderer

		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				this.Control.FontFamily = new FontFamily("/Assets/fontawesome-webfont.ttf#FontAwesome");
			}
		}

		#endregion
	}
}