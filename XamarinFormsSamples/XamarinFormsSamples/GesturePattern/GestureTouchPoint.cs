using Xamarin.Forms;

namespace XamarinFormsSamples.GesturePattern
{
	public class GestureTouchPoint : Label
	{
		private bool _setDefaults = true;


		#region properties

		public bool IsTouched { get; set; }

		public string Value { get; set; }

		public Color DefaultTextColor { get; set; }

		public Color HighlightTextColor { get; set; }

		public string DefaultText { get; set; }

		public string HighlightText { get; set; }

		#endregion


		#region public methods

		public void Touch()
		{
			if (_setDefaults)
			{
				this.DefaultTextColor = this.TextColor;
				this.DefaultText = this.Text;
				_setDefaults = false;
			}
			this.TextColor = this.HighlightTextColor;
			this.Text = this.HighlightText;
			this.IsTouched = true;
		}

		public void Reset()
		{
			if (this.IsTouched)
			{
				this.TextColor = this.DefaultTextColor;
				this.Text = this.DefaultText;
				this.IsTouched = false;
			}
		}

		#endregion
	}
}
