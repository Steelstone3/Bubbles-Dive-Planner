using System.Reactive;
using BubblesDivePlanner.Cylinders.CylinderSelector;
using BubblesDivePlanner.DiveModels.Selector;
using BubblesDivePlanner.DiveStep;
using ReactiveUI;

namespace BubblesDivePlanner.ApplicationEntry
{
    public interface IMainWindowModel
    {
        IDiveModelSelectorModel DiveModelSelector { get; set; }
        IDiveStepModel DiveStep { get; set; }
        ICylinderSelectorModel CylinderSelector { get; set; }
        ReactiveCommand<Unit, Unit> CalculateDiveStepCommand { get; }
    }
}