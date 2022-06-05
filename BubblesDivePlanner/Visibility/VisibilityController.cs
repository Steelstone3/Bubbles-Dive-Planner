using BubblesDivePlanner.ApplicationEntry;

namespace BubblesDivePlanner.Visibility
{
    public class VisibilityController
    {
        public void UpdateVisibilty(IMainWindowModel mainWindowViewModel)
        {
            mainWindowViewModel.DiveModelSelector.IsVisible = false;

            UpdateCylinderVisibilty(mainWindowViewModel);
            mainWindowViewModel.ResultsOverviewModel.IsVisible = true;
            mainWindowViewModel.DecompressionProfile.IsVisible = true;
        }

        private void UpdateCylinderVisibilty(IMainWindowModel mainWindowViewModel) {
            foreach(var cylinder in mainWindowViewModel.CylinderSelector.Cylinders)
            {
                cylinder.GasMixture.IsVisible = false;
                cylinder.IsVisible = false;
                cylinder.GasUsage.IsVisible = true;
            }
        }
    }
}