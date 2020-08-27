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
        private IDiveProfileController _diveProfileController;

        public MainWindowViewModel()
        {
            _diveProfileController = DiveProfileController.GetInstance;

            CalculateDiveStepCommand = ReactiveCommand.Create( RunDiveStep );

            CalculateDecompressionCommand = ReactiveCommand.Create( RunDecompressionProfile );

            DivePlan = new DivePlanViewModel();
            DiveInfo = new DiveInfoViewModel();
            DiveHeader = new DiveHeaderViewModel();
            DiveProfile = new DiveResultsViewModel();
        }

        private DiveResultsViewModel _diveProfile;
        public DiveResultsViewModel DiveProfile
        {
            get => _diveProfile;
            set => this.RaiseAndSetIfChanged( ref _diveProfile, value );
        }

        private DivePlanViewModel _divePlan;
        public DivePlanViewModel DivePlan
        {
            get => _divePlan;
            set => this.RaiseAndSetIfChanged( ref _divePlan, value );
        }

        private DiveInfoViewModel _diveInfo;
        public DiveInfoViewModel DiveInfo
        {
            get => _diveInfo;
            set => this.RaiseAndSetIfChanged( ref _diveInfo, value );
        }

        private DiveHeaderViewModel _diveHeader;
        public DiveHeaderViewModel DiveHeader
        {
            get => _diveHeader;
            set => this.RaiseAndSetIfChanged( ref _diveHeader, value );
        }

        public ReactiveCommand<Unit, Unit> CalculateDiveStepCommand
        {
            get;
        }

        public ReactiveCommand<Unit, Unit> CalculateDecompressionCommand
        {
            get;
        }

        private void RunDiveStep()
        {
            //TODO AH Work out can execute
            if( DivePlan.GasMixture.SelectedGasMixture != null && DivePlan.DiveModelSelector.SelectedDiveModel != null )
            {
                DiveProfile.DiveProfileResults = _diveProfileController.RunDiveStep( DivePlan.DiveStep, DivePlan.GasMixture );
                DiveProfile.DiveProfileHistoryResults.Add( _diveProfileController.RunDiveStep( DivePlan.DiveStep, DivePlan.GasMixture ) );
            }
        }

        private void RunDecompressionProfile()
        {
            _diveProfileController.RunDecompressionDiveSteps( DiveInfo.DecompressionProfile, DivePlan.GasMixture );
        }
    }
}
