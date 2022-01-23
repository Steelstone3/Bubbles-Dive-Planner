using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BubblesDivePlanner.GasManagement
{
    public class GasManagementView : UserControl
    {
        public GasManagementView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}