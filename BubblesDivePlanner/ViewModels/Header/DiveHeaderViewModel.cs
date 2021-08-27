using BubblesDivePlanner.Contracts.ViewModels.Header;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Header
{
    public class DiveHeaderViewModel : ViewModelBase, IDiveHeaderViewModel
    {
        private IFileViewModel _file;
        public IFileViewModel File
        {
            get => _file;
            set => this.RaiseAndSetIfChanged(ref _file, value);
        }
    }
}
