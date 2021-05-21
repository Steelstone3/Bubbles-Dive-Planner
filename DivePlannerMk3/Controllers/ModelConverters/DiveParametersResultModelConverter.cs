using DivePlannerMk3.ViewModels.DiveResult;
using DivePlannerMk3.Contracts;

namespace DivePlannerMk3.Controllers.ModelConverters
{
    public class DiveParametersResultModelConverter
    {
        public DiveParametersResultViewModel ConvertToViewModel(IDiveParametersResultModel model)
        {
            return new DiveParametersResultViewModel
            {
                DiveProfileStepHeader = model.DiveProfileStepHeader,
                DiveModelUsed = model.DiveModelUsed,
                Depth = model.Depth,
                Time = model.Time,
                GasName = model.GasName,
                Oxygen = model.Oxygen,
                Helium = model.Helium,
                Nitrogen = model.Nitrogen,
                DiveCeiling = model.DiveCeiling,
            };
        }
    }
}