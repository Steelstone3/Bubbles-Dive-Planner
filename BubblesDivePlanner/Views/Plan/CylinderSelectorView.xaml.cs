using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BubblesDivePlanner.Views.Plan
{
    public class CylinderSelectorView : UserControl
    {
        public CylinderSelectorView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}