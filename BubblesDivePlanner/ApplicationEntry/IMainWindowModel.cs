using System;
using System.Reactive;
using BubblesDivePlanner.DiveInformation;
using BubblesDivePlanner.DivePlanner;
using BubblesDivePlanner.Header;
using BubblesDivePlanner.Results;
using ReactiveUI;

namespace BubblesDivePlanner.ApplicationEntry
{
    public interface IMainWindowModel : IEventSubscriber
    {
        IHeaderModel HeaderModel { get; }
        DivePlannerViewModel DivePlanner { get; set; }
        IDiveInformationModel DiveInformation { get; set; }
        IResultsOverviewModel ResultsOverview { get; set; }
        ReactiveCommand<Unit, Unit> CalculateDiveStepCommand { get; }
        IObservable<bool> CanCalculateDiveStep { get; }
    }
}