using System.Reactive;
using BubblesDivePlanner.Controllers.Header;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Header
{
    public class FileViewModel : ViewModelBase
    {
        private MainWindowViewModel _mainWindowViewModel;

        public FileViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            NewCommand = ReactiveCommand.Create(CreateNewDiveSession);
            SaveCommand = ReactiveCommand.Create(SaveDivePlannerState);
            OpenCommand = ReactiveCommand.Create(LoadDivePlannerState);
        }

        public ReactiveCommand<Unit, Unit> NewCommand
        {
            get;
        }

        public ReactiveCommand<Unit, Unit> SaveCommand
        {
            get;
        }

        public ReactiveCommand<Unit, Unit> OpenCommand
        {
            get;
        }

        private void CreateNewDiveSession()
        {
            var newApplicationState = new NewApplicationStateController();
            _mainWindowViewModel = newApplicationState.NewApplication(_mainWindowViewModel);
        }

        //TODO AH this area needs a changable save name investigate Directory property of the SaveDialog
        private void SaveDivePlannerState()
        {
            var saveApplicationState = new SaveApplicationStateController();
            saveApplicationState.SaveApplication(_mainWindowViewModel);
        }

        //TODO AH this area needs work
        private void LoadDivePlannerState()
        {
            var loadApplicationState = new LoadApplicationStateController();
            loadApplicationState.LoadApplication(_mainWindowViewModel);
        }

    }
}