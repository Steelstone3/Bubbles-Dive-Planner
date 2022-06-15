using BubblesDivePlanner.Cylinders.CylinderSelector;
using BubblesDivePlanner.DiveModels.Selector;

namespace BubblesDivePlanner.DiveCalculationParameters
{
    public interface IDiveParameterSelectorModel
    {
        IDiveModelSelectorModel DiveModelSelector { get; set; }
        ICylinderSelectorModel CylinderSelector { get; set; }
    }
}