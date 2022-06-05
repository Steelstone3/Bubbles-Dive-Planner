using ReactiveUI;

namespace BubblesDivePlanner.DecompressionProfile
{
    public class DecompressionProfileViewModel : ReactiveObject, IDecompressionProfileModel
    {
        private bool _isVisible = false;
        public bool IsVisible
        {
            get => _isVisible;
            set => this.RaiseAndSetIfChanged(ref _isVisible, value);
        }
    }
}