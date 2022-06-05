using BubblesDivePlanner.Header.File.New;
using BubblesDivePlanner.ApplicationEntry;
using Xunit;
using BubblesDivePlannerTests.TestFixtures;

namespace BubblesDivePlannerTests.Header.File.New
{
    public class NewApplicationStateControllerShould
    {
        private DivePlannerApplicationTestFixture _diveStagesTextFixture = new DivePlannerApplicationTestFixture();

        [Fact]
        public void CreateNewDivePlannerInstance()
        {
            //Arrange
            NewApplicationStateController newController = new NewApplicationStateController();
            var divePlannerInstance = new MainWindowViewModel();
            var diveModelSelector = divePlannerInstance.DiveModelSelector;
            var diveStep = divePlannerInstance.DiveStep;
            var cylinderSelector = divePlannerInstance.CylinderSelector;
            var resultsOverview = divePlannerInstance.ResultsOverviewModel;

            //Act
            newController.CreateNewApplicationInstance(divePlannerInstance);

            //Assert
            Assert.NotSame(diveModelSelector, divePlannerInstance.DiveModelSelector);
            Assert.NotSame(diveStep, divePlannerInstance.DiveStep);
            Assert.NotSame(cylinderSelector, divePlannerInstance.CylinderSelector);
            Assert.NotSame(resultsOverview, divePlannerInstance.ResultsOverviewModel);
        }
    }
}