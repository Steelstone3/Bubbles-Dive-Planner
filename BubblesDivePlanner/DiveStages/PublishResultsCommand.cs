using System;
using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveModels.DiveProfile;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.Results;

namespace BubblesDivePlanner.DiveStages
{
    public class PublishResultsCommand : IDiveStageCommand
    {
        private IDiveProfileModel _diveProfile;
        private IDiveStepModel _diveStepModel;
        private ICylinderSetupModel _selectedCylinder;
        private IResultModel _resultModel;

        public PublishResultsCommand(IDiveProfileModel diveProfile, IDiveStepModel diveStepModel, ICylinderSetupModel selectedCylinder, IResultModel resultModel)
        {
            _diveProfile = diveProfile;
            _diveStepModel = diveStepModel;
            _selectedCylinder = selectedCylinder;
            _resultModel = resultModel;
        }

        public void RunDiveStage()
        {
            AssignDiveStep();
            AssignSelectedCylinder();
            AssignDiveProfile();
        }

        private void AssignDiveStep() => _resultModel.DiveStepModel = _diveStepModel.DeepClone();
        private void AssignSelectedCylinder()=> _resultModel.CylinderSetupModel = new CylinderPrototype().Clone(_selectedCylinder);
        private void AssignDiveProfile() => _resultModel.DiveProfileModel = _diveProfile.DeepClone();
    }
}