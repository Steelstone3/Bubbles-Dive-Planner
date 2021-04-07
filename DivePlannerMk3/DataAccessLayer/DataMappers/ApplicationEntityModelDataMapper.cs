using System.Collections.Generic;
using DivePlannerMk3.Contracts.DataAccessContracts;
using DivePlannerMk3.DataAccessLayer.DataMappers;
using DivePlannerMk3.ViewModels;

namespace DivePlannerMk3.DataAccessLayer.DataMappers
{
    public class ApplicationEntityModelDataMapper
    {
        public IEnumerable<IEntityModel> ConvertModelsToEntities(MainWindowViewModel mainViewModel)
        {
          return new List<IEntityModel>()
          {
            new DivePlanEntityModelDataMapper().ModelToEntity(mainViewModel.DivePlan),
            new DiveInfoEntityModelDataMapper().ModelToEntity(mainViewModel.DiveInfo),
            new DiveResultsEntityModelDataMapper().ModelToEntity(mainViewModel.DiveResults),
            //new DiveHeaderEntityModelDataMapper().ModelToEntity(mainViewModel.DiveHeader),
          };
        }

        public IEnumerable<IEntityModel> ConvertEntitiesToModels(IEnumerable<IEntityModel> entityModels)
        {
          return new List<IEntityModel>()
          {
            /*new DivePlanEntityModelDataMapper().EntityToModel(mainViewModel.DivePlan),
            new DiveInfoEntityModelDataMapper().EntityToModel(mainViewModel.DiveInfo),
            new DiveResultsEntityModelDataMapper().EntityToModel(mainViewModel.DiveResults),
            //new DiveHeaderEntityModelDataMapper().EntityToModel(mainViewModel.DiveHeader),*/
          };
        }
    }

}
