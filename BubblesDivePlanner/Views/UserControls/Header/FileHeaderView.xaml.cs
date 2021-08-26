using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BubblesDivePlanner.Views.UserControls.Header
{
    public class DiveHeaderFileHeaderView : UserControl
    {
        public DiveHeaderFileHeaderView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}