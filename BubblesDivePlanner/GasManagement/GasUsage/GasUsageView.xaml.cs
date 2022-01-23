using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BubblesDivePlanner.GasManagement.GasUsage
{
    public class GasUsageView : UserControl
    {
        public GasUsageView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}