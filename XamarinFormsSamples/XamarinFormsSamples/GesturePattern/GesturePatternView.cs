using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using Xamarin.Forms;
using XamarinFormsSamples.Services;

namespace XamarinFormsSamples.GesturePattern
{
	/// <summary>
	/// https://developer.xamarin.com/guides/xamarin-forms/user-interface/gestures/pan/
	/// http://stackoverflow.com/questions/37516830/how-to-create-pin-pattern-password-using-xamarin-form
	/// </summary>
	public class GesturePatternView : ContentView
	{
		double x, y;
		private double viewWidth, viewHeight;
		private IDeviceService _deviceService;


		public GesturePatternView()
		{
			var panGesture = new PanGestureRecognizer();
			panGesture.PanUpdated += OnPanUpdated;
			GestureRecognizers.Add(panGesture);

			_deviceService = SimpleIoc.Default.GetInstance<IDeviceService>();
		}

		private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
		{
			Debug.WriteLine($"OnPanUpdated [{e.StatusType}] GestureId [{e.GestureId}] TotalX [{e.TotalX}] TotalY [{e.TotalY}]");
			switch (e.StatusType)
			{
				case GestureStatus.Started:
					viewWidth = this.Width;
					viewHeight = this.Height;
					break;
				case GestureStatus.Running:
					//Content.TranslationX = Math.Max(Math.Min(0, x + e.TotalX), -Math.Abs(Content.Width - viewWidth)); 
					//Content.TranslationY = Math.Max(Math.Min(0, y + e.TotalY), -Math.Abs(Content.Height - viewHeight));
					Content.TranslationX = x + e.TotalX;
					Content.TranslationY = y + e.TotalY;
					Debug.WriteLine($"Content X [{Content.X}] Y [{Content.Y}]");
					break;
				case GestureStatus.Completed:
					x = Content.TranslationX;
					y = Content.TranslationY;
					break;
				case GestureStatus.Canceled:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private Page GetParentPage(Element child)
		{
			if (child.Parent is Page)
			{
				return (Page)child.Parent;
			}
			return this.GetParentPage(child.Parent);
		}
	}
}
