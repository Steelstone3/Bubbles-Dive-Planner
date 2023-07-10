using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BubblesDivePlanner.Views.Header
{
    public partial class HeaderView : UserControl
    {
        public HeaderView()
        {
            InitializeComponent();
        }

        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
    }
}