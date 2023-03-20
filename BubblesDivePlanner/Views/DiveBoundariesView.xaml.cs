using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BubblesDivePlanner.DiveBoundaries
{
    public class DiveBoundariesView : UserControl
    {
        public DiveBoundariesView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}