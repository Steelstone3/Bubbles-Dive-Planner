using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.DiveModels.DiveProfile;
using BubblesDivePlanner.DiveStep;
using ReactiveUI;

namespace BubblesDivePlanner.Results
{
    public class ResultViewModel : ReactiveObject, IResultModel
    {
        private IDiveStepModel _diveStep;
        public IDiveStepModel DiveStep
        {
            get => _diveStep;
            set => this.RaiseAndSetIfChanged(ref _diveStep, value);
        }

        private IDiveProfileModel _diveProfile;
        public IDiveProfileModel DiveProfile
        {
            get => _diveProfile;
            set => this.RaiseAndSetIfChanged(ref _diveProfile, value);
        }

        private ICylinderSetupModel _selectedCylinder;
        public ICylinderSetupModel SelectedCylinder
        {
            get => _selectedCylinder;
            set => this.RaiseAndSetIfChanged(ref _selectedCylinder, value);
        }
    }
}