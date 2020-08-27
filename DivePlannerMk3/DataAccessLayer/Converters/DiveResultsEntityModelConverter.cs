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
            DiveParametersResultDataMappingToEntity(diveResultsViewModel);
            DiveProfileResultsDataMappingToEntity(diveResultsViewModel);

            return _diveResultsEntityModel;
        }

        #region ModelToEntity

        private void DiveParametersResultDataMappingToEntity(DiveResultsViewModel diveResultsViewModel)
        {
            _diveResultsEntityModel.DiveProfileStepHeader = diveResultsViewModel.DiveParametersResult.DiveProfileStepHeader;
            _diveResultsEntityModel.DiveModelUsed = diveResultsViewModel.DiveParametersResult.DiveModelUsed;
            _diveResultsEntityModel.Depth = diveResultsViewModel.DiveParametersResult.Depth;
            _diveResultsEntityModel.Time = diveResultsViewModel.DiveParametersResult.Time;
            _diveResultsEntityModel.GasName = diveResultsViewModel.DiveParametersResult.GasName;
            _diveResultsEntityModel.Oxygen = diveResultsViewModel.DiveParametersResult.Oxygen;
            _diveResultsEntityModel.Helium = diveResultsViewModel.DiveParametersResult.Helium;
            _diveResultsEntityModel.Nitrogen = diveResultsViewModel.DiveParametersResult.Nitrogen;
        }

        private void DiveProfileResultsDataMappingToEntity(DiveResultsViewModel diveResultsViewModel)
        {
            //TODO AH need to think about how you are handling a list inside of a list
            //_diveResultsEntityModel.DiveProfileResults = diveResultsViewModel.DiveProfileResults;
        }

        #endregion
    }
}