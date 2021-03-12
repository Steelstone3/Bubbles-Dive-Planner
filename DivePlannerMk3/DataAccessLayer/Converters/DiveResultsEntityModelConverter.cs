using DivePlannerMk3.DataAccessLayer.EntityModels;
using DivePlannerMk3.ViewModels.DiveResult;

namespace DivePlannerMk3.DataAccessLayer.Converters
{
    public class DiveResultsEntityModelConverter
    {
        //TODO AH Finish off
         //UI Visibility later
        private DiveResultsEntityModel _diveResultsEntityModel = new DiveResultsEntityModel();

        public void EntityToModel()
        {
            throw new System.NotImplementedException();
        }

        public DiveResultsEntityModel ModelToEntity(DiveResultsViewModel diveResultsViewModel)
        {
            DataMappingToEntity(diveResultsViewModel);
            
            return _diveResultsEntityModel;
        }

        #region ModelToEntity

        private void DataMappingToEntity(DiveResultsViewModel diveResultsViewModel)
        {
            //_diveResultsEntityModel = diveResultsViewModel;
        }

        #endregion
    }
}