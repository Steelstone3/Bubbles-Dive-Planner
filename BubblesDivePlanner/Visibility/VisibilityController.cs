using System;
using BubblesDivePlanner.ApplicationEntry;

namespace BubblesDivePlanner.Visibility
{
    public class VisibilityController
    {
        public void Hide(IMainWindowModel mainWindowViewModel)
        {
            mainWindowViewModel.CylinderSelector.SelectedCylinder.GasUsage.IsVisible = true;

            mainWindowViewModel.DiveModelSelector.IsVisible = false;
            mainWindowViewModel.CylinderSelector.SelectedCylinder.IsVisible = false;
            mainWindowViewModel.CylinderSelector.SelectedCylinder.GasMixture.IsVisible = false;
        }
    }
}