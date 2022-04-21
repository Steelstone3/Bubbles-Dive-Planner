using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.Results;

namespace BubblesDivePlanner.DiveStages
{
    public class PublishResultsCommand : IDiveStageCommand
    {
        private IDiveModel _diveModel;
        private IDiveStepModel _diveStepModel;
        private IResultModel _resultModel;

        public PublishResultsCommand(IDiveModel diveModel, IDiveStepModel diveStepModel, IResultModel resultModel)
        {
            _diveModel = diveModel;
            _diveStepModel = diveStepModel;
            _resultModel = resultModel;
        }

        public void RunDiveStage()
        {
            AssignDiveStep();
            AssignDiveProfile();
        }

        private void AssignDiveStep() => _resultModel.DiveStepModel = _diveStepModel.DeepClone();
        private void AssignDiveProfile() => _resultModel.DiveProfileModel = _diveModel.DiveProfile.DeepClone();
    }
}