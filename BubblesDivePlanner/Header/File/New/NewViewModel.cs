using System.Reactive;
using BubblesDivePlanner.ApplicationEntry;
using ReactiveUI;

namespace BubblesDivePlanner.Header.File.New
{
    public class NewViewModel : INewModel
    {
        private IMainWindowModel _mainWindowModel;

        public NewViewModel(IMainWindowModel mainWindowModel)
        {
            CreateNewDivePlannerInstanceCommand = ReactiveCommand.Create(CreateNewDivePlannerInstance);
            _mainWindowModel = mainWindowModel;
        }

        public ReactiveCommand<Unit, Unit> CreateNewDivePlannerInstanceCommand { get; }

        private void CreateNewDivePlannerInstance()
        {
            _mainWindowModel = new NewApplicationStateController().CreateNewApplicationInstance();
        }
    }
}