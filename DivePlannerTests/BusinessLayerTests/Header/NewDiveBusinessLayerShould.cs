using DivePlannerMk3.Contracts;
using DivePlannerMk3.ViewModels;
using DivePlannerMk3.ViewModels.DiveApplication;
using DivePlannerMk3.ViewModels.DiveHeader;
using DivePlannerMk3.ViewModels.DiveInformation;
using DivePlannerMk3.ViewModels.DivePlan;
using DivePlannerMk3.ViewModels.DiveResult;
using Moq;
using Xunit;

namespace DivePlannerTests
{
    public class NewDiveBusinessLayerShould
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
                    DiveInformation = new DiveInformationViewModel()
                    {
                    
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