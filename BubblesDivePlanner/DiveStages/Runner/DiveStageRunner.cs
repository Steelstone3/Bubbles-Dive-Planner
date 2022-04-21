using BubblesDivePlanner.Results;

namespace BubblesDivePlanner.DiveStages.Runner
{
    public class DiveStageRunner : IDiveStageRunner
    {
        private IResultModel _resultModel;
        private DiveStageCommandFactory _diveStageCommandFactory;

        public DiveStageRunner(IResultModel resultModel, DiveStageCommandFactory diveStageCommandFactory)
        {
            _resultModel = resultModel;
            _diveStageCommandFactory = diveStageCommandFactory;
        }

        public IResultModel RunDiveStages()
        {
            var stages = _diveStageCommandFactory.CreateDiveStages();

            foreach(var stage in stages) {
                stage.RunDiveStage();
            }

            return _resultModel;
        }
    }
}