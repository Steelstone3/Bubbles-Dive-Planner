using BubblesDivePlanner.ApplicationEntry;
using Xunit;
using Moq;
using BubblesDivePlanner.Controllers;

namespace BubblesDivePlannerTests.Controllers
{
    public class NewApplicationStateControllerShould
    {
        [Fact]
        public void CreateNewDivePlannerInstance()
        {
            //Arrange
            var divePlannerInstance = new MainWindowViewModel();
            var divePlanner = divePlannerInstance.DivePlanner;
            var diveInformation = divePlannerInstance.DiveInformation;
            var resultsOverview = divePlannerInstance.ResultsOverview;

            //Act
            NewApplicationStateController.CreateNewApplicationInstance(divePlannerInstance);

            //Assert
            Assert.NotSame(divePlanner, divePlannerInstance.DivePlanner);
            Assert.NotSame(diveInformation, divePlannerInstance.DiveInformation);
            Assert.NotSame(resultsOverview, divePlannerInstance.ResultsOverview);
        }

        [Fact]
        public void SubscribeEvents()
        {
            //Arrange
            var divePlannerInstanceMock = new Mock<IMainWindowModel>();
            divePlannerInstanceMock.Setup(vm => vm.SubscribeEvents());

            //Act
            NewApplicationStateController.CreateNewApplicationInstance(divePlannerInstanceMock.Object);

            //Assert
            divePlannerInstanceMock.Verify(vm => vm.SubscribeEvents());
        }
    }
}