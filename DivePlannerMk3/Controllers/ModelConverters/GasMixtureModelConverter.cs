using DivePlannerMk3.Models;
using DivePlannerMk3.ViewModels.DivePlan;

namespace DivePlannerMk3.Controllers.ModelConverters
{
    public class GasMixtureModelConverter
    {
        public GasMixtureModel ConvertToModel(GasMixtureViewModel viewModel)
        {
            return new GasMixtureModel()
            {
                GasName=viewModel.GasName,
                Oxygen=viewModel.Oxygen,
                Helium=viewModel.Helium,
                Nitrogen=viewModel.Nitrogen,
            };
        }

        public GasMixtureViewModel ConvertToViewModel(GasMixtureModel model)
        {
            return new GasMixtureViewModel()
            {
                GasName=model.GasName,
                Oxygen=model.Oxygen,
                Helium=model.Helium,
            };
        }      
    }
}