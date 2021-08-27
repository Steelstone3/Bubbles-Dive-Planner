using System.Collections.Generic;
using BubblesDivePlanner.Contracts.Models.Results;
using BubblesDivePlanner.Contracts.ViewModels.Results;
using BubblesDivePlanner.Entities;
using BubblesDivePlanner.Models.Results;
using BubblesDivePlanner.ViewModels.Result;

namespace BubblesDivePlanner.Controllers.DataAccess.DataMappers
{
    public class DiveResultsEntityModelDataMapper
    {
        //TODO AH Change these to models as it should be view models to models and stuff happens then convert back
        //TODO AH Finish off
        //UI Visibility later
        private DiveResultsEntityModel _diveResultsEntityModel = new DiveResultsEntityModel();

        public void EntityToModel()
        {
            throw new System.NotImplementedException();
        }

        public DiveResultsEntityModel ModelToEntity(IDiveResultsViewModel diveResultsViewModel)
        {
            DiveParametersResultDataMappingToEntity(diveResultsViewModel);
            DiveProfileResultsDataMappingToEntity(diveResultsViewModel);

            return _diveResultsEntityModel;
        }

        #region ModelToEntity

        private void DiveParametersResultDataMappingToEntity(IDiveResultsViewModel diveResultsViewModel)
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
        
        private void DiveProfileResultsDataMappingToEntity(IDiveResultsViewModel diveResultsViewModel)
        {
            var diveResults = new List<IDiveResultsStepOutputModel>( diveResultsViewModel.DiveProfileResults );
            _diveResultsEntityModel.DiveResults.AddRange(diveResults);
        }

        #endregion
    }
}