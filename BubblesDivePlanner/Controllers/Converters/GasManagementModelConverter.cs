using BubblesDivePlanner.Contracts.Models.Plan;
using BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Plan;
using BubblesDivePlanner.Models.Plan;

namespace BubblesDivePlanner.Controllers.Converters
{
    public class GasManagementModelConverter
    {
        public IGasManagementModel ConvertToModel(IGasManagementViewModel viewModel)
        {
            return new GasManagementModel()
            {
                CylinderVolume = viewModel.CylinderVolume,
                CylinderPressure = viewModel.CylinderPressure,
                SacRate = viewModel.SacRate,
                InitialCylinderTotalVolume= viewModel.InitialCylinderTotalVolume,
                GasUsedForStep = viewModel.GasUsedForStep,
                GasRemaining = viewModel.GasRemaining,
            };
        }
    }
}