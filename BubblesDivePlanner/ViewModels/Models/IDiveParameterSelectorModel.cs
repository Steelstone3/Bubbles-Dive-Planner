using BubblesDivePlanner.ViewModels.Models;

namespace BubblesDivePlanner.DiveCalculationParameters
{
    public interface IDivePlanSelectorModel
    {
        IDiveModelSelectorModel DiveModelSelector { get; set; }
        ICylinderSelectorModel CylinderSelector { get; set; }
    }
}