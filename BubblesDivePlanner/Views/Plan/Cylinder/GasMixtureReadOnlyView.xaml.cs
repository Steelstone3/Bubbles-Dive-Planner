using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BubblesDivePlanner.Views.Plan.Cylinder
{
    public class GasMixtureReadOnlyView : UserControl
    {
        public GasMixtureReadOnlyView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}