using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.DiveModels.DiveProfile;
using BubblesDivePlanner.DiveStep;
using ReactiveUI;

namespace BubblesDivePlanner.Results
{
    public class ResultViewModel : ReactiveObject, IResultModel
    {
        private IDiveStepModel _diveStepModel;
        public IDiveStepModel DiveStepModel
        {
            get => _diveStepModel;
            set => this.RaiseAndSetIfChanged(ref _diveStepModel, value);
        }

        private IDiveProfileModel _diveProfileModel;
        public IDiveProfileModel DiveProfileModel
        {
            get => _diveProfileModel;
            set => this.RaiseAndSetIfChanged(ref _diveProfileModel, value);
        }

        private ICylinderSetupModel _cylinderSetupModel;
        public ICylinderSetupModel CylinderSetupModel 
        {
            get=> _cylinderSetupModel;
            set=> this.RaiseAndSetIfChanged(ref _cylinderSetupModel, value);
        }
    }
}