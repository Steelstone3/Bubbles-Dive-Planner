using BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Information;
using BubblesDivePlanner.Entities;
using BubblesDivePlanner.ViewModels.DiveApplication.Information;

namespace BubblesDivePlanner.Controllers.DataAccess.DataMappers
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

        public DiveInfoEntityModel ModelToEntity(IDiveInformationViewModel diveInfoViewModel)
        {
            CnsToxicityDataMappingToEntity(diveInfoViewModel);
            DecompressionProfileDataMappingToEntity(diveInfoViewModel);

            return _diveInfoEntityModel;
        }

        #region ModelToEntity

        private void CnsToxicityDataMappingToEntity(IDiveInformationViewModel diveInfoViewModel)
        {
            _diveInfoEntityModel.MaximumSingleDiveDuration = diveInfoViewModel.CnsToxicity.CnsToxicity.MaximumSingleDiveDuration;
            _diveInfoEntityModel.OxygenPartialPressureConstant = diveInfoViewModel.CnsToxicity.CnsToxicity.OxygenPartialPressureConstant;
            _diveInfoEntityModel.Total24HourDuration = diveInfoViewModel.CnsToxicity.CnsToxicity.Total24HourDuration;
        }

        //TODO AH This when decompression is implemented in version 2
        private void DecompressionProfileDataMappingToEntity(IDiveInformationViewModel diveInfoViewModel)
        {
            //diveInfoViewModel.DecompressionProfile
        }
       
        #endregion

        #region EntityToModel

        #endregion
    }
}