using DivePlannerMk3.DataAccessLayer.EntityModels;
using DivePlannerMk3.ViewModels.DiveHeader;

namespace DivePlannerMk3.DataAccessLayer.Converters
{
    public class DiveHeaderEntityModelConverter
    {
        //TODO AH Finish off
         //UI Visibility later
        private DiveHeaderEntityModel _diveHeaderEntityModel = new DiveHeaderEntityModel();

        public void EntityToModel()
        {
            throw new System.NotImplementedException();
        }

        public DiveHeaderEntityModel ModelToEntity(DiveHeaderViewModel diveHeaderViewModel)
        {
            DataMappingToEntity(diveHeaderViewModel);
            
            return _diveHeaderEntityModel;
        }

        #region ModelToEntity

        private void DataMappingToEntity(DiveHeaderViewModel diveHeaderViewModel)
        {
            //_diveResultsEntityModel = diveResultsViewModel;
        }

        #endregion
    }
}