using BubblesDivePlanner.Contracts.Services;
using BubblesDivePlanner.Controllers.Header;
using BubblesDivePlanner.ViewModels;
using BubblesDivePlanner.ViewModels.DiveApplication;
using BubblesDivePlanner.ViewModels.DiveApplication.Plan;
using BubblesDivePlanner.ViewModels.Header;
using BubblesDivePlanner.ViewModels.Result;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.BusinessLayerTests.Controllers.Header
{
    public class NewDiveControllerShould
    {
        [Fact]
        public void ResetAllDiveApplicationModels()
        {
            var mainWindowViewModelOriginal = new MainWindowViewModel()
            {
                DiveHeader = new DiveHeaderViewModel(),
                DiveApplication = new DiveApplicationViewModel(new Mock<IDiveProfileService>().Object)
                {
                    DivePlanSetup = new DivePlanSetupViewModel(new Mock<IDiveProfileService>().Object)
                    {
                        DiveStep = new DiveStepViewModel()
                        {
                            Depth = 50,
                            Time = 10,
                        },
                    },
                    DiveResults = new DiveResultsViewModel()
                    {
                        DiveParametersResult = new DiveParametersResultViewModel()
                        {
                            Depth = 50,
                            Time = 10,
                            DiveModelUsed = "Bob",
                            DiveProfileStepHeader = "Dive Step",
                        }
                    },
                },

            };

            var newApplicationState = new NewApplicationStateController();
            var mainWindowViewModel = newApplicationState.NewApplication(mainWindowViewModelOriginal);

            Assert.Equal(mainWindowViewModelOriginal.DiveHeader, mainWindowViewModel.DiveHeader);

            Assert.NotEqual(50, mainWindowViewModelOriginal.DiveApplication.DivePlanSetup.DiveStep.Depth);
            Assert.NotEqual(10, mainWindowViewModelOriginal.DiveApplication.DivePlanSetup.DiveStep.Time);

            Assert.NotEqual(50, mainWindowViewModelOriginal.DiveApplication.DiveResults.DiveParametersResult.Depth);
            Assert.NotEqual(10, mainWindowViewModelOriginal.DiveApplication.DiveResults.DiveParametersResult.Time);
            Assert.NotEqual("Bob", mainWindowViewModelOriginal.DiveApplication.DiveResults.DiveParametersResult.DiveModelUsed);
            Assert.NotEqual("Dive Step", mainWindowViewModelOriginal.DiveApplication.DiveResults.DiveParametersResult.DiveProfileStepHeader);
        }
    }
}