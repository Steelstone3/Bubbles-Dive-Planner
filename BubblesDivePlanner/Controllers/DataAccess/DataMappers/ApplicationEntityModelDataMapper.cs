using System.Collections.Generic;
using BubblesDivePlanner.Contracts.Entities;
using BubblesDivePlanner.Entities;
using BubblesDivePlanner.ViewModels;

namespace BubblesDivePlanner.Controllers.DataAccess.DataMappers
{
    public class ApplicationEntityModelDataMapper
    {
        public IEnumerable<IEntityModel> ConvertModelsToEntities(MainWindowViewModel mainViewModel)
        {
            return new List<IEntityModel>()
          {
            new DivePlanEntityModelDataMapper().ModelToEntity(mainViewModel.DiveApplication.DivePlanSetup),
            new DiveInfoEntityModelDataMapper().ModelToEntity(mainViewModel.DiveApplication.DiveInformation),
            new DiveResultsEntityModelDataMapper().ModelToEntity(mainViewModel.DiveApplication.DiveResults),
            //new DiveHeaderEntityModelDataMapper().ModelToEntity(mainViewModel.DiveHeader),
          };
        }

        public void ConvertEntitiesToModels(List<IEntityModel> entityModels, MainWindowViewModel mainWindowViewModel)
        {
            //TODO AH each to take specific IEntityModel
            mainWindowViewModel.DiveApplication.DivePlanSetup = new DivePlanEntityModelDataMapper().EntityToModel((DivePlanEntityModel)entityModels[0]);
            //new DiveInfoEntityModelDataMapper().EntityToModel((DiveInfoEntityModel)entityModels[1]),
            //new DiveResultsEntityModelDataMapper().EntityToModel((DiveResultsEntityModel)entityModels[2]),
            //new DiveHeaderEntityModelDataMapper().EntityToModel(entityModels.DiveHeader),

        }
    }

}
