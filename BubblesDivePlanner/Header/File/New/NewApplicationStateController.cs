using BubblesDivePlanner.ApplicationEntry;
using BubblesDivePlanner.DiveInformation;
using BubblesDivePlanner.DivePlanner;
using BubblesDivePlanner.Results;

namespace BubblesDivePlanner.Header.File.New
{
    public static class NewApplicationStateController
    {
        public static void CreateNewApplicationInstance(IMainWindowModel mainWindowModel)
        {
            mainWindowModel.DivePlanner = new DivePlannerViewModel();
            mainWindowModel.DiveInformation = new DiveInformationViewModel();
            mainWindowModel.ResultsOverview = new ResultsOverviewViewModel();
            mainWindowModel.SubscribeEvents();
        }
    }
}