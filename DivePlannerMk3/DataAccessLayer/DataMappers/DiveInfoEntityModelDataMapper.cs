using DivePlannerMk3.DataAccessLayer.EntityModels;
using DivePlannerMk3.ViewModels.DiveInfo;

namespace DivePlannerMk3.DataAccessLayer.DataMappers
{
    public class DiveInfoEntityModelDataMapper
    {

        //TODO AH Finish off later
        //UI Visibility later
        private DiveInfoEntityModel _diveInfoEntityModel = new DiveInfoEntityModel();

        public void EntityToModel()
        {
            throw new System.NotImplementedException();
        }

        public DiveInfoEntityModel ModelToEntity(DiveInfoViewModel diveInfoViewModel)
        {
            CnsToxicityDataMappingToEntity(diveInfoViewModel);
            DecompressionProfileDataMappingToEntity(diveInfoViewModel);
            DiveCeilingViewModelDataMappingToEntity(diveInfoViewModel);

            return _diveInfoEntityModel;
        }

        #region ModelToEntity

        private void CnsToxicityDataMappingToEntity(DiveInfoViewModel diveInfoViewModel)
        {
            _diveInfoEntityModel.MaximumSingleDiveDuration = diveInfoViewModel.CnsToxicityViewModel.CnsToxicity.MaximumSingleDiveDuration;
            _diveInfoEntityModel.OxygenPartialPressureConstant = diveInfoViewModel.CnsToxicityViewModel.CnsToxicity.OxygenPartialPressureConstant;
            _diveInfoEntityModel.Total24HourDuration = diveInfoViewModel.CnsToxicityViewModel.CnsToxicity.Total24HourDuration;
        }

        //TODO AH This when decompression is implemented in version 2
        private void DecompressionProfileDataMappingToEntity(DiveInfoViewModel diveInfoViewModel)
        {
            //diveInfoViewModel.DecompressionProfile
        }

        private void DiveCeilingViewModelDataMappingToEntity(DiveInfoViewModel diveInfoViewModel)
        {
            _diveInfoEntityModel.DiveCeiling = diveInfoViewModel.DiveCeilingViewModel.DiveCeiling;
        }

        #endregion

        #region EntityToModel

        #endregion
    }
}