using System.Collections.Generic;
using DivePlannerMk3.Contracts.DataAccessContracts;
using DivePlannerMk3.DataAccessLayer.Converters;
using DivePlannerMk3.ViewModels;

namespace DivePlannerMk3.DataAccessLayer.EntityModels
{
    public class ApplicationEntityModelConverter
    {
        public IEnumerable<IEntityModel> GenerateEntityModels(MainWindowViewModel mainViewModel)
        {
          return new List<IEntityModel>()
          {
            new DivePlanEntityModelConverter().ModelToEntity(mainViewModel.DivePlan),
            new DiveInfoEntityModelConverter().ModelToEntity(mainViewModel.DiveInfo),
            new DiveResultsEntityModelConverter().ModelToEntity(mainViewModel.DiveResults),
            new DiveHeaderEntityModelConverter().ModelToEntity(mainViewModel.DiveHeader),
          };
        }
    }

}
