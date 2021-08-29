using System;
using System.Reactive;
using BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Information;
using BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Plan;
using BubblesDivePlanner.Contracts.ViewModels.Results;
using ReactiveUI;

namespace BubblesDivePlanner.Contracts.ViewModels.DiveApplication
{
    public interface IDiveApplicationViewModel
    {
        IDivePlanSetupViewModel DivePlanSetup { get; set; }

        IDiveInformationViewModel DiveInformation { get; set; }

        IDiveResultsViewModel DiveResults { get; set; }

        ReactiveCommand<Unit, Unit> CalculateDiveStepCommand { get; }

        IObservable<bool> CanExecuteDiveStep { get; }
    }
}