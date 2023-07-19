using ReactiveUI;

namespace BubblesDivePlanner.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        private bool isVisible = true;
        public bool IsVisible
        {
            get => isVisible;
            set => this.RaiseAndSetIfChanged(ref isVisible, value);
        }
    }
}