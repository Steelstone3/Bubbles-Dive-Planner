using BubblesDivePlanner.Contracts.Models.Results;
using BubblesDivePlanner.Contracts.ViewModels.Results;
using BubblesDivePlanner.ViewModels.Result;

namespace BubblesDivePlanner.Controllers.Converters
{
    public class DiveParametersResultModelConverter
    {
        public IDiveParametersResultViewModel ConvertToViewModel(IDiveParametersResultModel model)
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