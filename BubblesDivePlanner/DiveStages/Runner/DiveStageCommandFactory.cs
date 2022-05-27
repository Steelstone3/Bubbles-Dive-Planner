using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.Results;

namespace BubblesDivePlanner.DiveStages.Runner
{
    public class DiveStageCommandFactory : IDiveStageCommandFactory
    {
        private IDiveModel _diveModel;
        private IDiveStepModel _diveStepModel;
        private ICylinderSetupModel _selectedCylinderModel;
        private IResultModel _resultModel;

        public DiveStageCommandFactory(IDiveModel diveModel, IDiveStepModel diveStepModel, ICylinderSetupModel selectedCylinder, IResultModel resultModel)
        {
            _diveModel = diveModel;
            _diveStepModel = diveStepModel;
            _selectedCylinderModel = selectedCylinder;
            _resultModel = resultModel;
        }

        public IDiveStageCommand[] CreateDiveStages()
        {
            return new IDiveStageCommand[]
            {
                new AmbientPressureCommand(_diveModel.DiveProfile, _selectedCylinderModel.GasMixture, _diveStepModel),
                new TissuePressureCommand(_diveModel, _diveStepModel),
                new AbValuesCommand(_diveModel),
                new ToleratedAmbientPressureCommand(_diveModel),
                new MaximumSurfacePressureCommand(_diveModel),
                new CompartmentLoadCommand(_diveModel),
                new PublishResultsCommand(_diveModel.DiveProfile, _diveStepModel, _selectedCylinderModel, _resultModel)
            };
        }
    }
}