using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DivePlannerMk3.Views.UserControls.DiveInfo
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