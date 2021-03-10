using System;
using System.IO;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using DivePlannerMk3.Contracts;
using DivePlannerMk3.Controllers;
using DivePlannerMk3.DataAccessLayer;
using DivePlannerMk3.DataAccessLayer.Serialisers;
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
            
            var divePlanEntity = DivePlan.ModelToEntity();
            DiveInfo.ModelToEntity();
            DiveResults.ModelToEntity();
            DiveHeader.ModelToEntity();

            string homePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
                Environment.OSVersion.Platform == PlatformID.MacOSX)
                ? Environment.GetEnvironmentVariable("HOME")
                : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

            var jsonFile = string.Empty;

            // File name  
            //string fileName = $"{homePath}{Path.DirectorySeparatorChar}{Path.DirectorySeparatorChar}DivePlan.json";
            string fileName = $"DivePlan.json";
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    using (JsonWriter jsonWriter = new JsonTextWriter(writer))
                    {
                        JsonSerializer serializer = new JsonSerializer();

                        CreateDataConverters(serializer);
                        CreateSerializerSettings(serializer);

                        serializer.Converters[0].WriteJson(jsonWriter, divePlanEntity, serializer);
                    }

                    //writer.Write(jsonFile);
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

        private void CreateDataConverters(JsonSerializer serializer)
        {
            serializer.Converters.Add(new DivePlanSerialiser());
        }

        private void CreateSerializerSettings(JsonSerializer serializer)
        {
            serializer.NullValueHandling = NullValueHandling.Ignore;
        }

        //TODO AH ****Consider potentially something like this (below)****
        /*private void CreateEntityModels()
        {
            var modelConverters = CreateDataConverters();

            foreach(var converter in modelConverters)
            {
                converter.EntityToModel();
            }
        }

        private IModelConverter[] CreateDataConverters()
        {
            return new IModelConverter[]
            {
                DivePlan,
                DiveInfo,
                DiveResults,
                DiveHeader,
            };
        }*/
    }
}
