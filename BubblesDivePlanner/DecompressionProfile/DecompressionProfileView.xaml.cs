using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BubblesDivePlanner.DecompressionProfile
{
    public class DecompressionProfileView : UserControl
    {
        public DecompressionProfileView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}