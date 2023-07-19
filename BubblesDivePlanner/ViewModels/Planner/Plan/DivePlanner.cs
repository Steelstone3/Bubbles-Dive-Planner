using System;
using System.Reactive;
using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan.Information;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan.Result;
using BubblesDivePlanner.ViewModels.Model.Planner.Setup;
using BubblesDivePlanner.ViewModels.Planner.Plan.Information;
using BubblesDivePlanner.ViewModels.Planner.Plan.Setup;
using BubblesDivePlanner.ViewModels.Planner.Plan.Stage;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Planner.Plan
{
    public class DivePlanner : ViewModelBase, IDivePlannerVM
    {
        public DivePlanner()
        {
            CalculateDiveProfileCommand = ReactiveCommand.Create(CalculateDiveProfile);
        }

        public IDiveSetup diveSetup = new DiveSetup();
        public IDiveSetup DiveSetup
        {
            get => diveSetup;
            set => this.RaiseAndSetIfChanged(ref diveSetup, value);
        }

        // TODO combine into a dive application?
        private IDiveInformation information = new DiveInformation();
        public IDiveInformation Information
        {
            get => information;
            set => this.RaiseAndSetIfChanged(ref information, value);
        }

        private IDiveStage diveStage = new DiveStage();
        public IDiveStage DiveStage
        {
            get => diveStage;
            set => this.RaiseAndSetIfChanged(ref diveStage, value);
        }

        public IResults results = new Results();
        public IResults Results
        {
            get => results;
            set => this.RaiseAndSetIfChanged(ref results, value);
        }

        public ReactiveCommand<Unit, Unit> CalculateDiveProfileCommand { get; }

        public void CalculateDiveProfile()
        {
            DiveStage.Cylinder = DiveSetup.CylinderSelection.Cylinder;

            DiveController.Run(DiveStage);
        }
    }
}