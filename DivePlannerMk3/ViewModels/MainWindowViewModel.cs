using System;
using System.Linq;
using System.Reactive;
using DivePlannerMk3.Controllers;
using DivePlannerMk3.DataAccessLayer.DataMappers;
using DivePlannerMk3.DataAccessLayer.Serialisers;
using DivePlannerMk3.ViewModels.DiveHeader;
using DivePlannerMk3.ViewModels.DiveInfo;
using DivePlannerMk3.ViewModels.DivePlan;
using DivePlannerMk3.ViewModels.DiveResult;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            _divePlan = new DivePlanViewModel(new DiveProfileService());

            CalculateDiveStepCommand = ReactiveCommand.Create(RunDiveStep, CanExecuteDiveStep); // create command
            NewCommand = ReactiveCommand.Create(CreateNewDiveSession);
            SaveCommand = ReactiveCommand.Create(SaveDivePlannerState);
        }

        private DiveResultsViewModel _diveResults = new DiveResultsViewModel();
        public DiveResultsViewModel DiveResults
        {
            get => _diveResults;
            set => this.RaiseAndSetIfChanged(ref _diveResults, value);
        }

        private DivePlanViewModel _divePlan;
        public DivePlanViewModel DivePlan
        {
            get => _divePlan;
            set => this.RaiseAndSetIfChanged(ref _divePlan, value);
        }

        private DiveInfoViewModel _diveInfo = new DiveInfoViewModel();
        public DiveInfoViewModel DiveInfo
        {
            get => _diveInfo;
            set => this.RaiseAndSetIfChanged(ref _diveInfo, value);
        }

        private DiveHeaderViewModel _diveHeader = new DiveHeaderViewModel();
        public DiveHeaderViewModel DiveHeader
        {
            get => _diveHeader;
            set => this.RaiseAndSetIfChanged(ref _diveHeader, value);
        }

        public ReactiveCommand<Unit, Unit> CalculateDiveStepCommand
        {
            get;
        }

        public IObservable<bool> CanExecuteDiveStep
        {
            get => this.WhenAnyValue(vm => vm.DivePlan.GasMixture.SelectedGasMixture, vm => vm.DivePlan.DiveModelSelector.SelectedDiveModel, (selectedGasMixture, selectedDiveModel) => selectedGasMixture != null && selectedDiveModel != null);
        }

        public ReactiveCommand<Unit, Unit> NewCommand
        {
            get;
        }

        public ReactiveCommand<Unit, Unit> SaveCommand
        {
            get;
        }

        private void RunDiveStep()
        {
            DivePlan.CalculateDiveStep(DiveResults);
            DiveResults.DiveParametersResult = DivePlan.UpdateUsedParameters(DiveResults.DiveParametersResult);
            DiveInfo.CalculateDiveStep(DiveResults.DiveProfileResults.SelectMany(diveModel => diveModel.DiveProfileStepOutput.Select(x => x.ToleratedAmbientPressureResult)));
        }

        private void CreateNewDiveSession()
        {
            DiveResults = new DiveResultsViewModel();
            DivePlan = new DivePlanViewModel(new DiveProfileService());
            DiveInfo = new DiveInfoViewModel();
            DiveHeader = new DiveHeaderViewModel();
        }

        //TODO AH this whole method needs breaking down and moving into a controller once the functionality is in
        private void SaveDivePlannerState()
        {
            //http://reference.avaloniaui.net/api/Avalonia.Controls/SaveFileDialog/

            /*var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filters.Add(new FileDialogFilter() { Name = "Json", Extensions = { "json" } });
            saveFileDialog.InitialFileName = "DivePlan";
            var result = await saveFileDialog.ShowAsync(this);
            if (result != null)
            {
    
            }*/

            var applicationConverter = new ApplicationEntityModelDataMapper();
            var applicationSaver = new ApplicationSaveLoad();

            var entityModels = applicationConverter.GenerateEntityModels(this);
            applicationSaver.SaveApplication(entityModels.ToList());
        }
    }
}
