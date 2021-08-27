using BubblesDivePlanner.Contracts.Models.Plan;
using BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Plan;
using BubblesDivePlanner.Models.Plan;

namespace BubblesDivePlanner.Controllers.Converters
{
    public class DiveStepModelConverter
    {
        public IDiveStepModel ConvertToModel(IDiveStepViewModel viewModel)
        {
            return new DiveStepModel()
            {
                Depth = viewModel.Depth,
                Time = viewModel.Time,
            };
        }
    }
}