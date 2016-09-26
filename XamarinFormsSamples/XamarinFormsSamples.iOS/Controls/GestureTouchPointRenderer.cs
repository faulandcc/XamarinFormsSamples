using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinFormsSamples.GesturePattern;
using XamarinFormsSamples.iOS.Controls;

[assembly: ExportRenderer(typeof(GestureTouchPoint), typeof(GestureTouchPointRenderer))]
namespace XamarinFormsSamples.iOS.Controls
{
	public class GestureTouchPointRenderer : LabelRenderer
	{
		#region Overrides of LabelRenderer

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (this.Element != null)
			{
				this.Element.FontFamily = "FontAwesome";
			}
		}

		#endregion
	}
}