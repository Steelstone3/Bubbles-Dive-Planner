using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BubblesDivePlanner.GasManagement.Cylinder
{
    public class CylinderView : UserControl
    {
        public CylinderView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}