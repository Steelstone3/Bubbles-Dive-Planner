using BubblesDivePlannerTests.Asserters;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.Acceptance
{
    public class DivePlannerShould
    {
        private DivePlannerApplicationTestFixture diveStagesTextFixture = new DivePlannerApplicationTestFixture();
        private DiveParameterAsserter diveParameterAsserter = new DiveParameterAsserter();


        [Fact(Skip = "Unexplained failing test")]
        public void AddCylinder()
        {
            //Arrange
            var selectedCylinder = diveStagesTextFixture.GetSelectedCylinder;

            //Act
            // _cylinderSelectorViewModel.AddCylinderCommand.Execute();

            //Assert
            // Assert.NotEmpty(_cylinderSelectorViewModel.Cylinders);
        }

        [Fact(Skip = "Need to work out how to get this type of test working")]
        public void CanAddCylinder()
        {
            //Arrange
            //Stubs of requirements

            //Act
            // _mainWindowViewModel.CalculateDiveStepCommand.Execute();

            //Assert
            //TODO AH something results populated
        }

        [Fact(Skip = "Need to work out how to get this type of test working")]
        public void CalculateDiveStep()
        {
            //Arrange
            //Stubs of requirements

            //Act
            // _mainWindowViewModel.CalculateDiveStepCommand.Execute();

            //Assert
            //TODO AH something results populated
        }

        [Fact(Skip = "Need to work out how to get this type of test working")]
        public void CanCalculateDiveStep()
        {
            //Arrange
            //Stubs of requirements

            //Act
            // _mainWindowViewModel.CalculateDiveStepCommand.Execute();

            //Assert
            //TODO AH something results populated
        }

        [Fact(Skip = "Need to work out how to get this type of test working")]
        public void CalculateDecompressionProfile()
        {
            //Arrange
            //Stubs of requirements

            //Act
            //newViewModel.CreateNewDivePlannerInstanceCommand.Execute();

            //Assert
            //TODO AH something results populated
        }

        [Fact(Skip = "Need to work out how to get this type of test working")]
        public void CanCalculateDecompressionProfile()
        {
            //Arrange
            //Stubs of requirements

            //Act
            //newViewModel.CreateNewDivePlannerInstanceCommand.Execute();

            //Assert
            //TODO AH something results populated
        }

        [Fact(Skip = "Need to work out how to get this type of test working")]
        public void CreateNewDivePlannerInstance()
        {
            //Arrange
            //Stubs of requirements

            //Act
            //newViewModel.CreateNewDivePlannerInstanceCommand.Execute();

            //Assert
            //TODO AH something results populated
        }
    }
}