using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace XamarinFormsSamples.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			// TODO:Replace this licence key by your own MR GESTURES licence key.
			// This licence key will only work, if your app is called "GestureSample".
			// We use the Test-LicenceKey from the MR GESTURES SAMPLE APP (https://github.com/MichaelRumpler/GestureSample).
			// Details, see: http://www.mrgestures.com/
			MR.Gestures.iOS.Settings.LicenseKey = "ALZ9-BPVU-XQ35-CEBG-5ZRR-URJQ-ED5U-TSY8-6THP-3GVU-JW8Z-RZGE-CQW6";

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
