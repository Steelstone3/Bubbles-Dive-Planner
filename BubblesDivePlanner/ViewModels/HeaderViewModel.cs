using BubblesDivePlanner.ApplicationEntry;
using BubblesDivePlanner.Header.File;
using BubblesDivePlanner.ViewModels.Models;

namespace BubblesDivePlanner.Header
{
    public class HeaderViewModel : IHeaderModel
    {
        private readonly IMainWindowModel _mainWindowModel;

        public HeaderViewModel(IMainWindowModel mainWindowModel)
        {
            _mainWindowModel = mainWindowModel;
            FileModel = new FileViewModel(_mainWindowModel);
        }

        public IFileModel FileModel { get; }
    }
}