using ReactiveUI;

namespace BubblesDivePlanner.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        bool IsVisible { get; } = true;
    }
}