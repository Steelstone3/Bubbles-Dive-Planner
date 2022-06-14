using BubblesDivePlanner.Header.File.New;
using BubblesDivePlanner.ApplicationEntry;
using Xunit;
using BubblesDivePlannerTests.TestFixtures;
using Moq;

namespace BubblesDivePlannerTests.Header.File.New
{
    public class NewApplicationStateControllerShould
    {
        private DivePlannerApplicationTestFixture _diveStagesTextFixture = new();
        private NewApplicationStateController _newController = new();

        [Fact]
        public void CreateNewDivePlannerInstance()
        {
            //Arrange
            var divePlannerInstance = new MainWindowViewModel();
            var diveModelSelector = divePlannerInstance.DiveModelSelector;
            var diveStep = divePlannerInstance.DiveStep;
            var cylinderSelector = divePlannerInstance.CylinderSelector;
            var diveInformation = divePlannerInstance.DiveInformation;
            var resultsOverview = divePlannerInstance.ResultsOverviewModel;

            //Act
            _newController.CreateNewApplicationInstance(divePlannerInstance);

            //Assert
            Assert.NotSame(diveModelSelector, divePlannerInstance.DiveModelSelector);
            Assert.NotSame(diveStep, divePlannerInstance.DiveStep);
            Assert.NotSame(cylinderSelector, divePlannerInstance.CylinderSelector);
            Assert.NotSame(diveInformation, divePlannerInstance.DiveInformation);
            Assert.NotSame(resultsOverview, divePlannerInstance.ResultsOverviewModel);
        }

        [Fact]
        public void SubscribeEvents()
        {
            //Arrange
            var divePlannerInstanceMock = new Mock<IMainWindowModel>();
            divePlannerInstanceMock.Setup(vm => vm.SubscribeEvents());

            //Act
            _newController.CreateNewApplicationInstance(divePlannerInstanceMock.Object);
            
            //Assert
            divePlannerInstanceMock.Verify(vm => vm.SubscribeEvents());
        }
    }
}