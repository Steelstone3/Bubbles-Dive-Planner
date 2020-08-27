using DivePlannerMk3.ViewModels.DiveResult;
using DivePlannerMk3.Models;

namespace DivePlannerMk3.Controllers.ModelConverters
{
    public class DiveParametersResultModelConverter
    {
        public DiveParametersOutputModel ConvertToModel(DiveParametersOutputModel viewModel)
        {
            return new DiveParametersOutputModel()
            {
                DiveProfileStepHeader = viewModel.DiveProfileStepHeader,
                DiveModelUsed = viewModel.DiveModelUsed,
                Depth = viewModel.Depth,
                Time = viewModel.Time,
                GasName = viewModel.GasName,
                Oxygen = viewModel.Oxygen,
                Helium = viewModel.Helium,
                Nitrogen = viewModel.Nitrogen,
            };
        }

        //TODO AH Could use an interface
        public DiveParametersResultViewModel ConvertToViewModel(DiveParametersOutputModel model)
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
            };
        }
    }
}