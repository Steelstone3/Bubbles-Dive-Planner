using Xunit;
using BubblesDivePlanner.Visibility;
using BubblesDivePlanner.ApplicationEntry;
using BubblesDivePlannerTests.TestFixtures;

namespace BubblesDivePlannerTests.Visibility
{
    public class VisibilityControllerShould
    {
        DivePlannerApplicationTestFixture _diveStagesTextFixture = new();

        [Fact]
        public void HideViews()
        {
            //Arrange
            IMainWindowModel mainWindowViewModel = new MainWindowViewModel();
            mainWindowViewModel.DiveModelSelector.SelectedDiveModel = _diveStagesTextFixture.GetDiveModel;
            mainWindowViewModel.CylinderSelector.Cylinders.Add(_diveStagesTextFixture.GetSelectedCylinder);
            mainWindowViewModel.CylinderSelector.Cylinders.Add(_diveStagesTextFixture.GetSelectedCylinder);

            var visibilityController = new VisibilityController();

            //Act
            visibilityController.UpdateVisibilty(mainWindowViewModel);

            //Assert
            foreach (var cylinder in mainWindowViewModel.CylinderSelector.Cylinders)
            {
                Assert.True(cylinder.GasUsage.IsVisible);
                Assert.False(cylinder.GasMixture.IsVisible);
                Assert.False(cylinder.IsVisible);
            }

            Assert.False(mainWindowViewModel.DiveModelSelector.IsVisible);            
            Assert.True(mainWindowViewModel.DecompressionProfile.IsVisible);            
        }
    }
}