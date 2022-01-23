using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BubblesDivePlanner.GasManagement.GasMixture
{
    public class GasMixtureView : UserControl
    {
        public GasMixtureView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}