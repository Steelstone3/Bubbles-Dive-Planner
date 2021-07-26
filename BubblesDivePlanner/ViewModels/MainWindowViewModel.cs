using DivePlannerMk3.Controllers;
using DivePlannerMk3.ViewModels.DiveApplication;
using DivePlannerMk3.ViewModels.DiveHeader;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        //TODO AH Test drive and Move NewCommand, SaveCommand, OpenCommand to there own classes under the header folder

        public MainWindowViewModel()
        {
            DiveApplication = new DiveApplicationViewModel(new DiveProfileService());
            _diveHeader.File = new FileViewModel(this);
        }

        private DiveApplicationViewModel _diveApplication;
        public DiveApplicationViewModel DiveApplication
        {
            get => _diveApplication;
            set => this.RaiseAndSetIfChanged(ref _diveApplication, value);
        }

        private DiveHeaderViewModel _diveHeader = new DiveHeaderViewModel();
        public DiveHeaderViewModel DiveHeader
        {
            get => _diveHeader;
            set => this.RaiseAndSetIfChanged(ref _diveHeader, value);
        }
    }
}
