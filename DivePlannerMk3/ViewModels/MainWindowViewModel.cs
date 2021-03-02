using System;
using System.IO;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using DivePlannerMk3.Controllers;
using DivePlannerMk3.ViewModels.DiveHeader;
using DivePlannerMk3.ViewModels.DiveInfo;
using DivePlannerMk3.ViewModels.DivePlan;
using DivePlannerMk3.ViewModels.DiveResult;
using Newtonsoft.Json;
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

        private DiveParametersResultViewModel _diveParametersResult = new DiveParametersResultViewModel();
        public DiveParametersResultViewModel DiveParametersResult
        {
            get => _diveParametersResult;
            set => this.RaiseAndSetIfChanged(ref _diveParametersResult, value);
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
            DiveParametersResult = DivePlan.UpdateUsedParameters(DiveParametersResult);
            DiveInfo.CalculateDiveStep(DiveResults.DiveProfileResults.SelectMany(diveModel => diveModel.DiveProfileStepOutput.Select(x => x.ToleratedAmbientPressureResult)));
        }

        private void CreateNewDiveSession()
        {
            DiveResults = new DiveResultsViewModel();
            DiveParametersResult = new DiveParametersResultViewModel();
            DivePlan = new DivePlanViewModel(new DiveProfileService());
            DiveInfo = new DiveInfoViewModel();
            DiveHeader = new DiveHeaderViewModel();
        }

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

            string homePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
                Environment.OSVersion.Platform == PlatformID.MacOSX)
                ? Environment.GetEnvironmentVariable("HOME")
                : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

            //var jsonFile = JsonConvert.SerializeObject(this, Formatting.Indented);

            var jsonFile = JsonConvert.SerializeObject(DiveResults, Formatting.Indented);
            jsonFile += JsonConvert.SerializeObject(DiveParametersResult, Formatting.Indented);
            jsonFile += JsonConvert.SerializeObject(DivePlan, Formatting.Indented);
            jsonFile += JsonConvert.SerializeObject(DiveInfo, Formatting.Indented);
            jsonFile += JsonConvert.SerializeObject(DiveHeader, Formatting.Indented);

            // File name  
            //string fileName = $"{homePath}{Path.DirectorySeparatorChar}{Path.DirectorySeparatorChar}DivePlan.json";
            string fileName = $"DivePlan.json";
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    writer.Write(jsonFile);
                }
            }
            catch (UnauthorizedAccessException uaex)
            {
                Console.Write(uaex.Message);
            }
            catch (IOException ioex)
            {
                Console.Write(ioex.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
}
