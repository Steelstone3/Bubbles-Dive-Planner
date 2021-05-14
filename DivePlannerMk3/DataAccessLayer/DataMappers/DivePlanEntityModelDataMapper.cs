using DivePlannerMk3.Controllers;
using DivePlannerMk3.DataAccessLayer.EntityModels;
using DivePlannerMk3.ViewModels.DivePlan;

namespace DivePlannerMk3.DataAccessLayer.DataMappers
{
    public class DivePlanEntityModelDataMapper
    {
        //TODO AH potentially could get fancy here and use a strategy pattern
        //TODO AH Pros pattern driven and clean cons more files may be a bit overkill exposed methods
        //TODO AH Step 1 IModelToEntityStrategy ModelToEntity()
        //TODO AH Step 2 Split view models down further where each private method currently would be its own ModelToEntityConversion
        //TODO AH Step 3 Combine the strategy pattern with the command pattern to call each model to entity conversion in this class' public EntityToModel()

        //TODO AH UI Visibility later
        private DivePlanViewModel _divePlanViewModel = new DivePlanViewModel(new DiveProfileService());
        private DivePlanEntityModel _divePlanEntityModel = new DivePlanEntityModel();

        public DivePlanViewModel EntityToModel(DivePlanEntityModel divePlanEntityModel)
        {
            DiveModelSelectorDataMappingToModel(divePlanEntityModel);
            DiveStepDataMappingToModel(divePlanEntityModel);
            GasManagementDataMappingToModel(divePlanEntityModel);
            GasMixtureDataMappingToModel(divePlanEntityModel);

            return _divePlanViewModel;
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

            //TODO AH may have to do a linq statement to get each gas mix index based on each name or redesign this!
            foreach (var gasMixture in divePlanViewModel.GasMixture.GasMixtures)
            {
                _divePlanEntityModel.GasName.Add(gasMixture.GasName);
                _divePlanEntityModel.Helium.Add(gasMixture.Helium);
                _divePlanEntityModel.Nitrogen.Add(gasMixture.Nitrogen);
                _divePlanEntityModel.Oxygen.Add(gasMixture.Oxygen);
            }
        }

        #endregion

        #region EntityToModel

        private void GasMixtureDataMappingToModel(DivePlanEntityModel divePlanEntityModel)
        {
            _divePlanViewModel.GasMixture.NewGasMixture.GasName = divePlanEntityModel.NewGasMixtureGasName;
            _divePlanViewModel.GasMixture.NewGasMixture.Helium = divePlanEntityModel.NewGasMixtureHelium;
            _divePlanViewModel.GasMixture.NewGasMixture.Oxygen = divePlanEntityModel.NewGasMixtureOxygen;

            _divePlanViewModel.GasMixture.SelectedGasMixture.GasName = divePlanEntityModel.SelectedGasMixtureGasName;
            _divePlanViewModel.GasMixture.SelectedGasMixture.Oxygen = divePlanEntityModel.SelectedGasMixtureOxygen;
            _divePlanViewModel.GasMixture.SelectedGasMixture.Helium = divePlanEntityModel.SelectedGasMixtureHelium;

            for (int i = 0; i < divePlanEntityModel.GasName.Count - 1; i++)
            {
                _divePlanViewModel.GasMixture.GasMixtures[i].GasName = divePlanEntityModel.GasName[i];
                _divePlanViewModel.GasMixture.GasMixtures[i].Oxygen = divePlanEntityModel.Oxygen[i];
                _divePlanViewModel.GasMixture.GasMixtures[i].Helium = divePlanEntityModel.Helium[i];
            }

            _divePlanViewModel.GasMixture.MaximumOperatingDepth = divePlanEntityModel.MaximumOperatingDepth;
        }

        private void GasManagementDataMappingToModel(DivePlanEntityModel divePlanEntityModel)
        {
            _divePlanViewModel.GasManagement.GasRemaining = divePlanEntityModel.GasRemaining;
            _divePlanViewModel.GasManagement.GasUsedForStep = divePlanEntityModel.GasUsedForStep;
            _divePlanViewModel.GasManagement.InitialCylinderTotalVolume = divePlanEntityModel.InitialCylinderTotalVolume;
            _divePlanViewModel.GasManagement.SacRate = divePlanEntityModel.SacRate;
        }

        private void DiveStepDataMappingToModel(DivePlanEntityModel divePlanEntityModel)
        {
            _divePlanViewModel.DiveStep.Depth = divePlanEntityModel.Depth;
            _divePlanViewModel.DiveStep.Time = divePlanEntityModel.Time;
        }

        private void DiveModelSelectorDataMappingToModel(DivePlanEntityModel divePlanEntityModel)
        {
            _divePlanViewModel.DiveModelSelector.SelectedDiveModel = divePlanEntityModel.SelectedDiveModel;
        }

        #endregion

    }
}