using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Header
{
    public class DiveHeaderViewModel : ViewModelBase
    {
        private FileViewModel _file;
        public FileViewModel File
        {
            get => _file;
            set => this.RaiseAndSetIfChanged(ref _file, value);
        }
    }
}
