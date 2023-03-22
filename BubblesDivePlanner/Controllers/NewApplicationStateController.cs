using BubblesDivePlanner.ApplicationEntry;
using BubblesDivePlanner.ViewModels.DiveInformation;
using BubblesDivePlanner.ViewModels.DivePlans;

namespace BubblesDivePlanner.Controllers
{
    public static class NewApplicationStateController
    {
        public static void CreateNewApplicationInstance(IMainWindowModel mainWindowModel)
        {
            mainWindowModel.DivePlanner = new DiveCalculationParametersViewModel();
            mainWindowModel.DiveInformation = new DiveInformationViewModel();
            mainWindowModel.ResultsOverview = new ResultsOverviewViewModel();
            mainWindowModel.SubscribeEvents();
        }
    }
}