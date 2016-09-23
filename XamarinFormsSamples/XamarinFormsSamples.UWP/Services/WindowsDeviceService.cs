using XamarinFormsSamples.Services;

namespace XamarinFormsSamples.UWP.Services
{
	public class WindowsDeviceService : IDeviceService
	{
		#region Implementation of IDeviceService

		public double DeviceWidth { get; set; }
		public double DeviceHeight { get; set; }

		#endregion
	}
}
