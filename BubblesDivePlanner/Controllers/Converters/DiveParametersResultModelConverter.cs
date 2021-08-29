using BubblesDivePlanner.Contracts.Models.Results;
using BubblesDivePlanner.Contracts.ViewModels.Results;
using BubblesDivePlanner.ViewModels.Result;

namespace BubblesDivePlanner.Controllers.Converters
{
    public class DiveParametersResultModelConverter
    {
        public IDiveParametersResultViewModel ConvertModelToViewModel(
            IDiveParametersResultModel diveParametersResultModel)
        {
            return new DiveParametersResultViewModel()
            {
                DiveProfileStepHeader = diveParametersResultModel.DiveProfileStepHeader,
                DiveModelUsed = diveParametersResultModel.DiveModelUsed,
                Depth = diveParametersResultModel.Depth,
                Time = diveParametersResultModel.Time,
                GasName = diveParametersResultModel.GasName,
                Oxygen = diveParametersResultModel.Oxygen,
                Helium = diveParametersResultModel.Helium,
                Nitrogen = diveParametersResultModel.Nitrogen,
                DiveCeiling = diveParametersResultModel.DiveCeiling,
            };
        }
    }
}