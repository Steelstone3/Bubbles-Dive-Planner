using ReactiveUI;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.GasManagement;

namespace BubblesDivePlanner.ApplicationEntry
{
    public class MainWindowViewModel : ReactiveObject
    {
        public MainWindowViewModel()
        {
            DiveStep = new DiveStepViewModel();
            GasManagement = new GasManagementViewModel();
        }

        private IDiveStepModel _diveStep;
        public IDiveStepModel DiveStep
        {
            get => _diveStep;
            set => this.RaiseAndSetIfChanged(ref _diveStep, value);
        }

        private IGasManagementModel _gasManagement;
        public IGasManagementModel GasManagement
        {
            get => _gasManagement;
            set => this.RaiseAndSetIfChanged(ref _gasManagement, value);
        }
    }
}
