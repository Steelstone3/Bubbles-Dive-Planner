using System;
using System.Reactive;
using System.Reactive.Linq;
using DivePlannerMk3.Contracts;
using DivePlannerMk3.Controllers;
using DivePlannerMk3.ViewModels.DiveHeader;
using DivePlannerMk3.ViewModels.DiveInfo;
using DivePlannerMk3.ViewModels.DivePlan;
using DivePlannerMk3.ViewModels.DiveResult;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IDiveProfileService _diveProfileController;

        public MainWindowViewModel()
        {
            _diveProfileController = new DiveProfileService();

            CalculateDiveStepCommand = ReactiveCommand.Create( RunDiveStep );

            //TODO AH Add this feature later
            //CalculateDecompressionCommand = ReactiveCommand.Create( RunDecompressionProfile );
        }

        private DiveResultsViewModel _diveProfile= new DiveResultsViewModel();
        public DiveResultsViewModel DiveProfile
        {
            get => _diveProfile;
            set => this.RaiseAndSetIfChanged( ref _diveProfile, value );
        } 

        private DivePlanViewModel _divePlan = new DivePlanViewModel();
        public DivePlanViewModel DivePlan
        {
            get => _divePlan;
            set => this.RaiseAndSetIfChanged( ref _divePlan, value );
        }

        private DiveInfoViewModel _diveInfo = new DiveInfoViewModel();
        public DiveInfoViewModel DiveInfo
        {
            get => _diveInfo;
            set => this.RaiseAndSetIfChanged( ref _diveInfo, value );
        }

        private DiveHeaderViewModel _diveHeader = new DiveHeaderViewModel();
        public DiveHeaderViewModel DiveHeader
        {
            get => _diveHeader;
            set => this.RaiseAndSetIfChanged( ref _diveHeader, value );
        }

        public ReactiveCommand<Unit, Unit> CalculateDiveStepCommand
        {
            get;
        }

        //TODO AH Add this feature later
        /*public ReactiveCommand<Unit, Unit> CalculateDecompressionCommand
        {
            get;
        }*/

        private void RunDiveStep()
        {
            //TODO AH Work out can execute!!!
            if( DivePlan.GasMixture.SelectedGasMixture != null && DivePlan.DiveModelSelector.SelectedDiveModel != null )
            {
                _diveProfileController.TheDiveModel = DivePlan.DiveModelSelector.SelectedDiveModel;
                
                DiveProfile.DiveProfileResults = _diveProfileController.RunDiveStep( DivePlan.DiveStep, DivePlan.GasMixture );
                DiveProfile.DiveProfileHistoryResults.Add( DiveProfile.DiveProfileResults );
            }
        }

        //TODO AH add decompression feature
        /*private void RunDecompressionProfile()
        {
            _diveProfileController.RunDecompressionDiveSteps( DiveInfo.DecompressionProfile, DivePlan.GasMixture );
        }*/
    }
}
