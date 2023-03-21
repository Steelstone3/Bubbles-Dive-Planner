using BubblesDivePlanner.ViewModels.Models;

namespace BubblesDivePlanner.DiveCalculationParameters
{
    public interface IDiveParameterSelectorModel
    {
        IDiveModelSelectorModel DiveModelSelector { get; set; }
        ICylinderSelectorModel CylinderSelector { get; set; }
    }
}