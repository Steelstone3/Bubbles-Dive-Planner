using System;
using System.Reactive;
using BubblesDivePlanner.DiveInformation;
using BubblesDivePlanner.Events;
using BubblesDivePlanner.Header;
using BubblesDivePlanner.ViewModels.DivePlans;
using BubblesDivePlanner.ViewModels.Models;
using ReactiveUI;

namespace BubblesDivePlanner.ApplicationEntry
{
    public interface IMainWindowModel : IEventSubscriber
    {
        IHeaderModel HeaderModel { get; }
        DivePlanViewModel DivePlan { get; set; }
        IDiveInformationModel DiveInformation { get; set; }
        IResultsOverviewModel ResultsOverview { get; set; }
        ReactiveCommand<Unit, Unit> CalculateDiveStepCommand { get; }
        IObservable<bool> CanCalculateDiveStep { get; }
    }
}