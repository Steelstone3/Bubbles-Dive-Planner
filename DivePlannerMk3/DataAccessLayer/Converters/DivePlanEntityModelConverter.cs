using DivePlannerMk3.DataAccessLayer.EntityModels;
using DivePlannerMk3.ViewModels.DivePlan;

namespace DivePlannerMk3.DataAccessLayer.Converters
{
    public class DivePlanEntityModelConverter
    {
        //UI Visibility later
        private DivePlanEntityModel _divePlanEntityModel = new DivePlanEntityModel();

        public void EntityToModel()
        {
            throw new System.NotImplementedException();
        }

        public DivePlanEntityModel ModelToEntity(DivePlanViewModel divePlanViewModel)
        {
            DiveModelSelectorDataMappingToEntity(divePlanViewModel);
            DiveStepDataMappingToEntity(divePlanViewModel);
            GasManagementDataMappingToEntity(divePlanViewModel);
            GasMixtureDataMappingToEntity(divePlanViewModel);
            
            return _divePlanEntityModel;
        }

        #region ModelToEntity

        private void DiveModelSelectorDataMappingToEntity(DivePlanViewModel divePlanViewModel)
        {
            _divePlanEntityModel.SelectedDiveModel = divePlanViewModel.DiveModelSelector.SelectedDiveModel;
        }

        private void DiveStepDataMappingToEntity(DivePlanViewModel divePlanViewModel)
        {
            _divePlanEntityModel.Depth = divePlanViewModel.DiveStep.Depth;
            _divePlanEntityModel.Time = divePlanViewModel.DiveStep.Time;
        }

        private void GasManagementDataMappingToEntity(DivePlanViewModel divePlanViewModel)
        {
             _divePlanEntityModel.CylinderPressure = divePlanViewModel.GasManagement.CylinderPressure;
             _divePlanEntityModel.CylinderVolume = divePlanViewModel.GasManagement.CylinderVolume;
             _divePlanEntityModel.GasRemaining = divePlanViewModel.GasManagement.GasRemaining;
             _divePlanEntityModel.GasUsedForStep = divePlanViewModel.GasManagement.GasUsedForStep;
             _divePlanEntityModel.InitialCylinderTotalVolume = divePlanViewModel.GasManagement.InitialCylinderTotalVolume;
             _divePlanEntityModel.SacRate = divePlanViewModel.GasManagement.SacRate;
        }

        private void GasMixtureDataMappingToEntity(DivePlanViewModel divePlanViewModel)
        {
            _divePlanEntityModel.MaximumOperatingDepth = divePlanViewModel.GasMixture.MaximumOperatingDepth;

            _divePlanEntityModel.NewGasMixtureGasName = divePlanViewModel.GasMixture.NewGasMixture.GasName;
            _divePlanEntityModel.NewGasMixtureHelium = divePlanViewModel.GasMixture.NewGasMixture.Helium;
            _divePlanEntityModel.NewGasMixtureNitrogen = divePlanViewModel.GasMixture.NewGasMixture.Nitrogen;
            _divePlanEntityModel.NewGasMixtureOxygen = divePlanViewModel.GasMixture.NewGasMixture.Oxygen;

            _divePlanEntityModel.SelectedGasMixtureGasName = divePlanViewModel.GasMixture.SelectedGasMixture.GasName;
            _divePlanEntityModel.SelectedGasMixtureHelium = divePlanViewModel.GasMixture.SelectedGasMixture.Helium;
            _divePlanEntityModel.SelectedGasMixtureNitrogen = divePlanViewModel.GasMixture.SelectedGasMixture.Nitrogen;
            _divePlanEntityModel.SelectedGasMixtureOxygen = divePlanViewModel.GasMixture.SelectedGasMixture.Oxygen;
            
            //TODO AH WORK OUT GAS MIXTURES
            //_divePlanEntityModel.GasMixtures.Add(divePlanViewModel.GasMixture.GasMixtures);
        }

        #endregion

        #region EntityToModel

        #endregion

    }
}