using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DiveHeader
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
