
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BubblesDivePlanner.Results
{
    public class ResultsView : UserControl
    {
        public ResultsView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}