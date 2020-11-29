using System.ComponentModel;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        private bool _uiEnabled = false;
        public bool UiEnabled
        {
            get => _uiEnabled;
            set
            {
                if( _uiEnabled != value )
                {
                    _uiEnabled = value;
                    this.RaisePropertyChanged( nameof( UiEnabled ) );
                }
            }
        }
    }
}
