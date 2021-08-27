using BubblesDivePlanner.Contracts.ViewModels.DiveApplication;
using BubblesDivePlanner.Contracts.ViewModels.Header;
using BubblesDivePlanner.Services;
using BubblesDivePlanner.ViewModels.DiveApplication;
using BubblesDivePlanner.ViewModels.Header;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        //TODO AH Test drive and Move NewCommand, SaveCommand, OpenCommand to there own classes under the header folder

        public MainWindowViewModel()
        {
            DiveApplication = new DiveApplicationViewModel(new DiveProfileService());
            _diveHeader.File = new FileViewModel(this);
        }

        private IDiveApplicationViewModel _diveApplication;
        public IDiveApplicationViewModel DiveApplication
        {
            get => _diveApplication;
            set => this.RaiseAndSetIfChanged(ref _diveApplication, value);
        }

        private IDiveHeaderViewModel _diveHeader = new DiveHeaderViewModel();
        public IDiveHeaderViewModel DiveHeader
        {
            get => _diveHeader;
            set => this.RaiseAndSetIfChanged(ref _diveHeader, value);
        }
    }
}
