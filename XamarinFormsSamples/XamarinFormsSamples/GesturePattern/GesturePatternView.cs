using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using MR.Gestures;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Grid = Xamarin.Forms.Grid;

namespace XamarinFormsSamples.GesturePattern
{
	public delegate void GesturePatternCompletedEventHandler(object sender, GesturePatternCompletedEventArgs e);

	/// <summary>
	/// The gesture pattern view. 
	/// </summary>
	public class GesturePatternView : MR.Gestures.ContentView
	{
		private readonly List<GestureTouchPoint> _touchPoints = new List<GestureTouchPoint>();
		private readonly StringBuilder _gestureValueBuilder = new StringBuilder();
		private string _lastTouchPointValue;
	    private SKCanvasView _canvas;


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


		#region events

		/// <summary>
		/// Raised as soon as the finger was released.
		/// </summary>
		public event GesturePatternCompletedEventHandler GesturePatternCompleted;
		protected virtual void OnGesturePatternCompleted(string gesturePatternValue)
		{
			this.GesturePatternValue = gesturePatternValue;
			this.GesturePatternCompleted?.Invoke(this, new GesturePatternCompletedEventArgs()
			{
				GesturePatternValue = this.GesturePatternValue
			});
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
					grid.RowDefinitions.Add(new RowDefinition() { Height = 40 });
				}
			}
			for (int hIndex = 0; hIndex < this.HorizontalTouchPoints; hIndex++)
			{
				grid.ColumnDefinitions.Add(new ColumnDefinition());
				if (hIndex < (this.HorizontalTouchPoints - 1))
				{
					grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 40 });
				}
			}

			for (int vIndex = 0; vIndex < grid.RowDefinitions.Count; vIndex += 2)
			{
				for (int hIndex = 0; hIndex < grid.ColumnDefinitions.Count; hIndex += 2)
				{
					var touchPoint = new GestureTouchPoint()
					{
						//Text = this.TouchPointText,
						HighlightText = this.TouchPointHighlightText,
						//FontSize = 30,
						//FontFamily = string.IsNullOrEmpty(this.TouchPointFontFamily) ? "FontAwesome" : this.TouchPointFontFamily,
						InputTransparent = true,
						//TextColor = this.TouchPointTextColor,
						//BackgroundColor = this.TouchPointTextColor,
						HighlightTextColor = this.TouchPointHighlightTextColor,
                        //HorizontalTextAlignment = TextAlignment.Center,
						//VerticalTextAlignment = TextAlignment.Center,
                        HeightRequest = 40,
                        WidthRequest = 40,
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.Center
					};

					var tpValue = _touchPoints.Count + 1;
					touchPoint.Value = tpValue.ToString();

					touchPoint.SetValue(Grid.RowProperty, vIndex);
					touchPoint.SetValue(Grid.ColumnProperty, hIndex);

					grid.Children.Add(touchPoint);
					_touchPoints.Add(touchPoint);
				}
			}

		    _canvas = new SKCanvasView();
		    _canvas.IgnorePixelScaling = true;
            _canvas.EnableTouchEvents = false;
		    _canvas.PaintSurface += (sender, args) =>
		    {
		        var canvas = args.Surface.Canvas;
                canvas.Clear();
                
                SKPaint skPaintTouchPoint = new SKPaint
                {
                    Style = SKPaintStyle.StrokeAndFill,
                    Color = this.TouchPointTextColor.ToSKColor(),
                    StrokeWidth = 3
                };
                SKPaint skPaintTouchPointTouched = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = this.TouchPointHighlightTextColor.ToSKColor(),
                    StrokeWidth = 3
                };
                SKPaint skPaint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.Green,
                    StrokeWidth = 5
                };

                // Paint the touchpoints.
                foreach (var touchPoint in _touchPoints)
		        {
		            var p = this.GetMidOfTouchPoint(touchPoint);
                    canvas.DrawCircle((float)p.X, (float)p.Y, 10, skPaintTouchPoint);
		        }

                if (_fixedPoints.Count == 0)
                {
                    return;
                }

                // Paint the fixed points.
                canvas.DrawCircle((float)_fixedPoints.First().X, (float)_fixedPoints.First().Y, 50, skPaintTouchPointTouched);
                for (int i = 1; i < _fixedPoints.Count; i++)
		        {
                    float xStart = (float)_fixedPoints[i-1].X;
                    float yStart = (float)_fixedPoints[i - 1].Y;
                    float xEnd = (float)_fixedPoints[i].X;
                    float yEnd = (float)_fixedPoints[i].Y;
                    canvas.DrawLine(xStart, yStart, xEnd, yEnd, skPaint);
                    canvas.DrawCircle((float)_fixedPoints[i].X, (float)_fixedPoints[i].Y, 50, skPaintTouchPointTouched);
                }
                // Paint the pending point.
                if (_pendingPoint != Point.Zero)
		        {
		            skPaint.Color = SKColors.Red;
                    float xStart = (float)_fixedPoints.Last().X;
                    float yStart = (float)_fixedPoints.Last().Y;
                    float xEnd = (float)_pendingPoint.X;
                    float yEnd = (float)_pendingPoint.Y;
                    canvas.DrawLine(xStart, yStart, xEnd, yEnd, skPaint);
                }
            };
            _canvas.SetValue(Grid.RowProperty, 0);
            _canvas.SetValue(Grid.RowSpanProperty, grid.RowDefinitions.Count);
            _canvas.SetValue(Grid.ColumnProperty, 0);
            _canvas.SetValue(Grid.ColumnSpanProperty, grid.ColumnDefinitions.Count);
            
            grid.Children.Add(_canvas);

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

        private List<Point> _fixedPoints = new List<Point>();
	    private Point _pendingPoint = Point.Zero;

        private void OnPanning(object sender, PanEventArgs e)
		{
			Debug.WriteLine($"Panning: {e.Touches.FirstOrDefault()} {e.DeltaDistance} {e.TotalDistance} {e.NumberOfTouches} {e.Center} {e.Sender}");
			var location = e.Touches.First();

		    bool hasPendingPoint = true;
		    foreach (var gestureTouchPoint in _touchPoints)
			{
				if (TouchedTouchPoint(location, gestureTouchPoint) && gestureTouchPoint.Value != _lastTouchPointValue)
				{
                    gestureTouchPoint.Touch();
					_gestureValueBuilder.Append(gestureTouchPoint.Value);
                    _lastTouchPointValue = gestureTouchPoint.Value;
                    _fixedPoints.Add(this.GetMidOfTouchPoint(gestureTouchPoint));
                    hasPendingPoint = false;
					break;
				}
			}
		    if (hasPendingPoint)
		    {
		        _pendingPoint.X = location.X;
		        _pendingPoint.Y = location.Y;
		    }
		    else
		    {
		        _pendingPoint = Point.Zero;
		    }
            _canvas?.InvalidateSurface();
        }

	    private void OnPanned(object sender, PanEventArgs e)
		{
		    Debug.WriteLine($"Panned: {e.Touches.FirstOrDefault()} {e.DeltaDistance} {e.TotalDistance} {e.NumberOfTouches} {e.Center} {e.Sender}");
			Debug.WriteLine($"PATTERN VALUE = {_gestureValueBuilder}");

			// Gesture pattern completed.
			this.OnGesturePatternCompleted(_gestureValueBuilder.ToString());

			// Reset the touchpoints.
			foreach (var gestureTouchPoint in _touchPoints)
			{
				gestureTouchPoint.Reset();
			}
			// Clear the recognized gesture values.
			_gestureValueBuilder.Clear();
			_lastTouchPointValue = null;

            _pendingPoint = Point.Zero;
            _canvas?.InvalidateSurface();
        }

        private Point GetMidOfTouchPoint(GestureTouchPoint touchPoint)
        {
            double midX = touchPoint.X + (touchPoint.Width / 2);
            double midY = touchPoint.Y + (touchPoint.Height / 2);

            return new Point(midX, midY);
        }

        #endregion
    }


	public class GesturePatternCompletedEventArgs : EventArgs
	{
		public string GesturePatternValue { get; set; }
	}
}
