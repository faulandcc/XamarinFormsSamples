using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XamarinFormsSamples.Common
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private bool _isBusy;
        private string _isBusyText;


        #region properties

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string IsBusyText
        {
            get { return _isBusyText; }
            set
            {
                if (_isBusyText != value)
                {
                    _isBusyText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        #endregion

        
        #region events

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        #region ctor

        protected ViewModelBase()
        {
        }

        #endregion
    }
}
