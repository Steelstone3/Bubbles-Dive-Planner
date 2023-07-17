using ReactiveUI;

namespace BubblesDivePlanner.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        public bool IsVisible { get; } = true;
    }
}