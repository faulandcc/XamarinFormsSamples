using Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinFormsSamples.Droid.Controls;
using XamarinFormsSamples.GesturePattern;
using View = Android.Views.View;

//[assembly: ExportRenderer(typeof(GestureTouchPoint), typeof(GestureTouchPointRenderer))]
namespace XamarinFormsSamples.Droid.Controls
{

	public class GestureTouchPointRenderer : ButtonRenderer
	{
		#region Overrides of ViewGroup

		protected override bool DrawChild(Canvas canvas, View child, long drawingTime)
		{
			var label = child as global::Android.Widget.Button;
			if (label != null)
			{
				Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, "fontawesome-webfont.ttf");
				label.Typeface = font;
			}
			return base.DrawChild(canvas, child, drawingTime);
		}

		#endregion
	}
}