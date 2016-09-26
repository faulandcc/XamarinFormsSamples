using System.ComponentModel;
using System.Runtime.CompilerServices;
using XamarinFormsSamples.Annotations;

namespace XamarinFormsSamples.GesturePattern
{
	public class GestureSampleViewModel : INotifyPropertyChanged
	{
		private string _gestureValue;


		#region properties

		public string GestureValue
		{
			get { return _gestureValue; }
			set
			{
				if (_gestureValue != value)
				{
					_gestureValue = value;
					this.OnPropertyChanged();
				}
			}
		}

		#endregion


		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}
