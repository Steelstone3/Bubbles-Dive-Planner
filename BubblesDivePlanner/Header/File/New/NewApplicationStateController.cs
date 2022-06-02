using BubblesDivePlanner.ApplicationEntry;
using BubblesDivePlanner.Cylinders.CylinderSelector;
using BubblesDivePlanner.DiveModels.Selector;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.Results;

namespace BubblesDivePlanner.Header.File.New
{
    public class NewApplicationStateController
    {
        public void CreateNewApplicationInstance(IMainWindowModel mainWindowModel)
        {
            mainWindowModel.DiveModelSelector = new DiveModelSelectorViewModel();
            mainWindowModel.DiveStep = new DiveStepViewModel();
            mainWindowModel.CylinderSelector = new CylinderSelectorViewModel();
            mainWindowModel.ResultsOverviewModel = new ResultsOverviewViewModel();
        }
    }
}