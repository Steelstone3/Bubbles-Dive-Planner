using System.Reactive;
using BubblesDivePlanner.ApplicationEntry;
using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.ViewModels.Models;
using ReactiveUI;

namespace BubblesDivePlanner.Header.File.New
{
    public class NewViewModel : INewModel
    {
        private readonly IMainWindowModel _mainWindowModel;

        public NewViewModel(IMainWindowModel mainWindowModel)
        {
            CreateNewDivePlannerInstanceCommand = ReactiveCommand.Create(CreateNewDivePlannerInstance);
            _mainWindowModel = mainWindowModel;
        }

        public ReactiveCommand<Unit, Unit> CreateNewDivePlannerInstanceCommand { get; }

        private void CreateNewDivePlannerInstance()
        {
            NewApplicationStateController.CreateNewApplicationInstance(_mainWindowModel);
        }
    }
}