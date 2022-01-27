using ReactiveUI;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.GasManagement;
using BubblesDivePlanner.GasManagement.GasMixture;

namespace BubblesDivePlanner.ApplicationEntry
{
    public class MainWindowViewModel : ReactiveObject
    {
        private IDiveStepModel _diveStep = new DiveStepViewModel();
        public IDiveStepModel DiveStep
        {
            get => _diveStep;
            set => this.RaiseAndSetIfChanged(ref _diveStep, value);
        }

        private IGasManagementModel _gasManagement = new GasManagementViewModel();
        public IGasManagementModel GasManagement
        {
            get => _gasManagement;
            set => this.RaiseAndSetIfChanged(ref _gasManagement, value);
        }
    }
}
