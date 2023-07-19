using ReactiveUI;

namespace BubblesDivePlanner.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        private bool _isVisible = true;
        public bool IsVisible
        {
            get => _isVisible;
            set => this.RaiseAndSetIfChanged(ref _isVisible, value);
        }
    }
}