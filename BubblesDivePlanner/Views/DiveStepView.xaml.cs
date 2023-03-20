using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BubblesDivePlanner.DiveStep
{
    public class DiveStepView : UserControl
    {
        public DiveStepView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}