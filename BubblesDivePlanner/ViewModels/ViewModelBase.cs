using BubblesDivePlanner.Contracts.ViewModels;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels
{
    public class ViewModelBase : ReactiveObject, IVisibility
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

        public bool IsVisible { get; set; }
    }
}
