using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using MR.Gestures;
using Xamarin.Forms;
using XamarinFormsSamples.Services;
using Grid = Xamarin.Forms.Grid;

namespace XamarinFormsSamples.GesturePattern
{
	/// <summary>
	/// https://developer.xamarin.com/guides/xamarin-forms/user-interface/gestures/pan/
	/// http://stackoverflow.com/questions/37516830/how-to-create-pin-pattern-password-using-xamarin-form
	/// </summary>
	public class GesturePatternView : MR.Gestures.ContentView
	{
		private readonly List<GestureTouchPoint> _touchPoints = new List<GestureTouchPoint>();
		private readonly StringBuilder _gestureValueBuilder = new StringBuilder();
		private string _lastTouchPointValue;


		#region bindable properties

		/// <summary>
		/// The number of horizontal touch points.
		/// </summary>
		public static readonly BindableProperty HorizontalTouchPointsProperty = BindableProperty.Create<GesturePatternView, int>(x => x.HorizontalTouchPoints, 3, BindingMode.OneWay);

		/// <summary>
		/// The number of vertical touch points.
		/// </summary>
		public static readonly BindableProperty VerticalTouchPointsProperty = BindableProperty.Create<GesturePatternView, int>(x => x.VerticalTouchPoints, 3, BindingMode.OneWay);

		/// <summary>
		/// The gesture pattern value.
		/// </summary>
		public static readonly BindableProperty GesturePatternValueProperty = BindableProperty.Create<GesturePatternView, string>(x => x.GesturePatternValue, null, BindingMode.OneWay);

		/// <summary>
		/// The font to use for the touch points.
		/// NOTE: Using another symbol font also requires a modification in the GestureTouchPointRenderer in the android project.
		/// </summary>
		public static readonly BindableProperty TouchPointFontFamilyProperty = BindableProperty.Create<GesturePatternView, string>(x => x.TouchPointFontFamily, null, BindingMode.OneWay);

		/// <summary>
		/// The text to display on the touch points.
		/// </summary>
		public static readonly BindableProperty TouchPointTextProperty = BindableProperty.Create<GesturePatternView, string>(x => x.TouchPointText, null, BindingMode.OneWay);

		/// <summary>
		/// The text to display on the touched touch points.
		/// </summary>
		public static readonly BindableProperty TouchPointHighlightTextProperty = BindableProperty.Create<GesturePatternView, string>(x => x.TouchPointHighlightText, null, BindingMode.OneWay);

		/// <summary>
		/// The text color to use for the touch points.
		/// </summary>
		public static readonly BindableProperty TouchPointTextColorProperty = BindableProperty.Create<GesturePatternView, Color>(x => x.TouchPointTextColor, Color.Black, BindingMode.OneWay);

		/// <summary>
		/// The text color to use for the touched touch points.
		/// </summary>
		public static readonly BindableProperty TouchPointHighlightTextColorProperty = BindableProperty.Create<GesturePatternView, Color>(x => x.TouchPointHighlightTextColor, Color.Yellow, BindingMode.OneWay);

		#endregion


		#region properties

		/// <summary>
		/// The number of horizontal touch points.
		/// </summary>
		public int HorizontalTouchPoints
		{
			get { return (int)GetValue(VerticalTouchPointsProperty); }
			set { SetValue(VerticalTouchPointsProperty, value); }
		}

		/// <summary>
		/// The number of vertical touch points.
		/// </summary>
		public int VerticalTouchPoints
		{
			get { return (int)GetValue(HorizontalTouchPointsProperty); }
			set { SetValue(HorizontalTouchPointsProperty, value); }
		}

		/// <summary>
		/// The gesture pattern value.
		/// </summary>
		public string GesturePatternValue
		{
			get { return (string)GetValue(GesturePatternValueProperty); }
			set { SetValue(GesturePatternValueProperty, value); }
		}

		/// <summary>
		/// The font to use for the touch points.
		/// Remember, on Android we have to create a renderer to apply the font!
		/// </summary>
		public string TouchPointFontFamily
		{
			get { return (string)GetValue(TouchPointFontFamilyProperty); }
			set { SetValue(TouchPointFontFamilyProperty, value); }
		}

		/// <summary>
		/// The text to display on the touch points.
		/// We can use a symbol font too!
		/// </summary>
		public string TouchPointText
		{
			get { return (string)GetValue(TouchPointTextProperty); }
			set { SetValue(TouchPointTextProperty, value); }
		}

		/// <summary>
		/// The text to display on the touched touch points.
		/// We can use a symbol font too!
		/// </summary>
		public string TouchPointHighlightText
		{
			get { return (string)GetValue(TouchPointHighlightTextProperty); }
			set { SetValue(TouchPointHighlightTextProperty, value); }
		}

		/// <summary>
		/// The text color to use for the touch points.
		/// </summary>
		public Color TouchPointTextColor
		{
			get { return (Color)GetValue(TouchPointTextColorProperty); }
			set { SetValue(TouchPointTextColorProperty, value); }
		}

		/// <summary>
		/// The text color to use for a touched the touch points.
		/// </summary>
		public Color TouchPointHighlightTextColor
		{
			get { return (Color)GetValue(TouchPointHighlightTextColorProperty); }
			set { SetValue(TouchPointHighlightTextColorProperty, value); }
		}

		#endregion


		#region ctor

		/// <summary>
		/// Create gesture pattern view instance.
		/// </summary>
		public GesturePatternView()
		{
			this.Panning += OnPanning;
			this.Panned += OnPanned;
		}

		#endregion


		#region public methods

		public void CreateTouchInterface()
		{
			this.Content = null;
			_touchPoints.Clear();

			if (this.VerticalTouchPoints <= 0 || this.HorizontalTouchPoints <= 0)
			{
				return;
			}

			var grid = new Grid
			{
				RowDefinitions = new RowDefinitionCollection(),
				ColumnDefinitions = new ColumnDefinitionCollection(),
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};
			for (int vIndex = 0; vIndex < this.VerticalTouchPoints; vIndex++)
			{
				grid.RowDefinitions.Add(new RowDefinition());
				if (vIndex < (this.VerticalTouchPoints - 1))
				{
					grid.RowDefinitions.Add(new RowDefinition() { Height = 20 });
				}
			}
			for (int hIndex = 0; hIndex < this.HorizontalTouchPoints; hIndex++)
			{
				grid.ColumnDefinitions.Add(new ColumnDefinition());
				if (hIndex < (this.HorizontalTouchPoints - 1))
				{
					grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 20 });
				}
			}

			for (int vIndex = 0; vIndex < grid.RowDefinitions.Count; vIndex += 2)
			{
				for (int hIndex = 0; hIndex < grid.ColumnDefinitions.Count; hIndex += 2)
				{
					var touchPoint = new GestureTouchPoint()
					{
						Text = this.TouchPointText,
						HighlightText = this.TouchPointHighlightText,
						FontSize = 30,
						FontFamily = string.IsNullOrEmpty(this.TouchPointFontFamily) ? "FontAwesome" : this.TouchPointFontFamily,
						InputTransparent = true,
						TextColor = this.TouchPointTextColor,
						HighlightTextColor = this.TouchPointHighlightTextColor,
						HorizontalTextAlignment = TextAlignment.Center,
						VerticalTextAlignment = TextAlignment.Center,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						VerticalOptions = LayoutOptions.FillAndExpand
					};

					var tpValue = _touchPoints.Count + 1;
					touchPoint.Value = tpValue.ToString();

					touchPoint.SetValue(Grid.RowProperty, vIndex);
					touchPoint.SetValue(Grid.ColumnProperty, hIndex);

					grid.Children.Add(touchPoint);
					_touchPoints.Add(touchPoint);
				}
			}
			this.Content = grid;
		}

		#endregion


		#region private methods

		private bool TouchedTouchPoint(Point location, GestureTouchPoint touchPoint)
		{
			if (location.X >= touchPoint.X &&
				location.X <= (touchPoint.X + touchPoint.Width) &&
				location.Y >= touchPoint.Y &&
				location.Y <= (touchPoint.Y + touchPoint.Height))
			{
				return true;
			}
			return false;
		}

		private void OnPanning(object sender, PanEventArgs e)
		{
			Debug.WriteLine($"Panning: {e.Touches.FirstOrDefault()} {e.DeltaDistance} {e.TotalDistance} {e.NumberOfTouches} {e.Center} {e.Sender}");
			var location = e.Touches.First();

			foreach (var gestureTouchPoint in _touchPoints)
			{
				if (TouchedTouchPoint(location, gestureTouchPoint) && gestureTouchPoint.Value != _lastTouchPointValue)
				{
					gestureTouchPoint.Touch();
					_gestureValueBuilder.Append(gestureTouchPoint.Value);
					_lastTouchPointValue = gestureTouchPoint.Value;
					break;
				}
			}
		}

		private void OnPanned(object sender, PanEventArgs e)
		{
			Debug.WriteLine($"Panned: {e.Touches.FirstOrDefault()} {e.DeltaDistance} {e.TotalDistance} {e.NumberOfTouches} {e.Center} {e.Sender}");
			Debug.WriteLine($"PATTERN VALUE = {_gestureValueBuilder}");

			// Reset the touchpoints.
			foreach (var gestureTouchPoint in _touchPoints)
			{
				gestureTouchPoint.Reset();
			}
			// Clear the recognized gesture values.
			_gestureValueBuilder.Clear();
			_lastTouchPointValue = null;
		}

		#endregion
	}
}
