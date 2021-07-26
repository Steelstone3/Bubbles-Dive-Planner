using ReactiveUI;

namespace DivePlannerMk3.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        private bool _isUiVisible = false;
        public bool IsUiVisible
        {
            get => _isUiVisible;
            set => this.RaiseAndSetIfChanged(ref _isUiVisible, value);
        }

        private bool _isUiEnabled = true;
        public bool IsUiEnabled
        {
            get => _isUiEnabled;
            set => this.RaiseAndSetIfChanged(ref _isUiEnabled, value);
        }
    }
}
