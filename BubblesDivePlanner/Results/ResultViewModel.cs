using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.DiveModels.DiveProfile;
using BubblesDivePlanner.DiveStep;
using ReactiveUI;

namespace BubblesDivePlanner.Results
{
    public class ResultViewModel : ReactiveObject, IResultModel
    {
        private IDiveStepModel _diveStepModel;
        public IDiveStepModel DiveStep
        {
            get => _diveStepModel;
            set => this.RaiseAndSetIfChanged(ref _diveStepModel, value);
        }

        private IDiveProfileModel _diveProfileModel;
        public IDiveProfileModel DiveProfile
        {
            get => _diveProfileModel;
            set => this.RaiseAndSetIfChanged(ref _diveProfileModel, value);
        }

        private ICylinderSetupModel _cylinderSetupModel;
        public ICylinderSetupModel SelectedCylinder 
        {
            get=> _cylinderSetupModel;
            set=> this.RaiseAndSetIfChanged(ref _cylinderSetupModel, value);
        }
    }
}