using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DiveInfo
{
    public class InfoDiveModelSelectedReadOnlyViewModel : ViewModelBase
    {
        //public string DiveModelName
        //{

        //}

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