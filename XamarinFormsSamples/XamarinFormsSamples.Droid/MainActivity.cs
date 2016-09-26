using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace XamarinFormsSamples.Droid
{
	[Activity(Label = "GestureSample", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			// TODO:Replace this licence key by your own MR GESTURES licence key.
			// This licence key will only work, if your app is called "GestureSample".
			// We use the Test-LicenceKey from the MR GESTURES SAMPLE APP (https://github.com/MichaelRumpler/GestureSample).
			// Details, see: http://www.mrgestures.com/
			MR.Gestures.Android.Settings.LicenseKey = "ALZ9-BPVU-XQ35-CEBG-5ZRR-URJQ-ED5U-TSY8-6THP-3GVU-JW8Z-RZGE-CQW6";

			LoadApplication(new App());
		}
	}
}

