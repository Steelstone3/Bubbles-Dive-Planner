using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BubblesDivePlanner.DiveModels.Selector
{
    public class DiveModelSelectorView : UserControl
    {
        public DiveModelSelectorView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}