using BubblesDivePlanner.Commands;
using BubblesDivePlanner.Commands.Interfaces;
using BubblesDivePlanner.Controllers.Interfaces;
using BubblesDivePlanner.ViewModels.Models;

namespace BubblesDivePlanner.Controllers.DiveStages
{
    public class DiveStageCommandFactory : IDiveStageCommandFactory
    {
        private readonly IDiveModel _diveModel;
        private readonly IDiveStepModel _diveStepModel;
        private readonly ICylinderSetupModel _selectedCylinderModel;

        public DiveStageCommandFactory(IDiveModel diveModel, IDiveStepModel diveStepModel, ICylinderSetupModel selectedCylinder)
        {
            _diveModel = diveModel;
            _diveStepModel = diveStepModel;
            _selectedCylinderModel = selectedCylinder;
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
            };
        }
    }
}