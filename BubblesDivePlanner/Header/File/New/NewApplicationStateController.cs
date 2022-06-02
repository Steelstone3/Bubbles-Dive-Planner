using BubblesDivePlanner.ApplicationEntry;
using BubblesDivePlanner.Cylinders.CylinderSelector;
using BubblesDivePlanner.DiveModels.Selector;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.Results;

namespace BubblesDivePlanner.Header.File.New
{
    public class NewApplicationStateController
    {
        public IMainWindowModel CreateNewApplicationInstance()
        {
            return new MainWindowViewModel();
            // {
            //     DiveModelSelector = new DiveModelSelectorViewModel(),
            //     DiveStep = new DiveStepViewModel(),
            //     CylinderSelector = new CylinderSelectorViewModel(),
            //     ResultsOverviewModel = new ResultsOverviewViewModel(),
            // };
        }
    }
}