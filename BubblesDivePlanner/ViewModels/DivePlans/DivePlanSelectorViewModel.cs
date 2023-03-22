using BubblesDivePlanner.DiveCalculationParameters;
using BubblesDivePlanner.ViewModels.Cylinders;
using BubblesDivePlanner.ViewModels.Models;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.DivePlans
{
    public class DivePlanSelectorViewModel : ReactiveObject, IDivePlanSelectorModel
    {
        private IDiveModelSelectorModel _diveModelSelector = new DiveModelSelectorViewModel();
        public IDiveModelSelectorModel DiveModelSelector
        {
            get => _diveModelSelector;
            set => this.RaiseAndSetIfChanged(ref _diveModelSelector, value);
        }

        private ICylinderSelectorModel _cylinderSelector = new CylinderSelectorViewModel();
        public ICylinderSelectorModel CylinderSelector
        {
            get => _cylinderSelector;
            set => this.RaiseAndSetIfChanged(ref _cylinderSelector, value);
        }
    }
}