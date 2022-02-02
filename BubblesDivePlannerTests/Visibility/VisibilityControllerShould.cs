using Xunit;
using BubblesDivePlanner.Visibility;
using BubblesDivePlanner.ApplicationEntry;

namespace BubblesDivePlannerTests.Visibility
{
    public class VisibilityControllerShould
    {
        [Fact]
        public void HideViews()
        {
            //Arrange
            IMainWindowModel mainWindowViewModel = new MainWindowViewModel();
            var visibilityController = new VisibilityController();

            //Act
            visibilityController.Hide(mainWindowViewModel);

            //Assert
            Assert.True(mainWindowViewModel.CylinderSelector.SelectedCylinder.GasUsage.IsVisible);
            
            Assert.False(mainWindowViewModel.DiveModelSelector.IsVisible);
            Assert.False(mainWindowViewModel.CylinderSelector.SelectedCylinder.IsVisible);
            Assert.False(mainWindowViewModel.CylinderSelector.SelectedCylinder.GasMixture.IsVisible);
        }
    }
}