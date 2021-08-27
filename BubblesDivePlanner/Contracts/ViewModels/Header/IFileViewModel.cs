using System.Reactive;
using ReactiveUI;

namespace BubblesDivePlanner.Contracts.ViewModels.Header
{
    public interface IFileViewModel
    {
        ReactiveCommand<Unit, Unit> NewCommand { get; }

        ReactiveCommand<Unit, Unit> SaveCommand { get; }

        ReactiveCommand<Unit, Unit> OpenCommand { get; }
    }
}