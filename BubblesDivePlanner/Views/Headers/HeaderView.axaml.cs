using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BubblesDivePlanner.Views.Headers
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