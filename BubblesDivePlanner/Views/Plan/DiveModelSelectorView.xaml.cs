using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BubblesDivePlanner.Views.Plan
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