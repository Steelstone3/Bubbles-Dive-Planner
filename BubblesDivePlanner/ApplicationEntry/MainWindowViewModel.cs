using ReactiveUI;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.GasManagement;
using BubblesDivePlanner.GasManagement.GasMixture;
using BubblesDivePlanner.DiveModels.Selector;

namespace BubblesDivePlanner.ApplicationEntry
{
    public class MainWindowViewModel : ReactiveObject
    {
        private IDiveModelSelectorModel _diveModelSelector = new DiveModelSelectorViewModel();
        public IDiveModelSelectorModel DiveModelSelector
        {
            get => _diveModelSelector;
            set => this.RaiseAndSetIfChanged(ref _diveModelSelector, value);
        }

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
