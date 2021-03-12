using DivePlannerMk3.DataAccessLayer.EntityModels;
using DivePlannerMk3.ViewModels.DiveInfo;

namespace DivePlannerMk3.DataAccessLayer.Converters
{
    public class DiveInfoEntityModelConverter
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
            DataMappingToEntity(diveInfoViewModel);
            return _diveInfoEntityModel;
        }

        #region ModelToEntity

        private void DataMappingToEntity(DiveInfoViewModel diveInfoViewModel)
        {

        }

        #endregion

        #region EntityToModel

        #endregion
    }
}