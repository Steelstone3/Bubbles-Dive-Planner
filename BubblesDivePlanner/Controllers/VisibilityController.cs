using BubblesDivePlanner.ApplicationEntry;

namespace BubblesDivePlanner.Visibility
{
    public static class VisibilityController
    {
        public static void UpdateVisibilty(IMainWindowModel mainWindowViewModel)
        {
            mainWindowViewModel.DivePlanner.DiveModelSelector.IsVisible = false;

            UpdateCylinderVisibilty(mainWindowViewModel);
            mainWindowViewModel.ResultsOverview.IsVisible = true;
        }

        private static void UpdateCylinderVisibilty(IMainWindowModel mainWindowViewModel)
        {
            foreach (var cylinder in mainWindowViewModel.DivePlanner.CylinderSelector.Cylinders)
            {
                cylinder.GasMixture.IsVisible = false;
                cylinder.IsVisible = false;
                cylinder.GasUsage.IsVisible = true;
            }
        }
    }
}