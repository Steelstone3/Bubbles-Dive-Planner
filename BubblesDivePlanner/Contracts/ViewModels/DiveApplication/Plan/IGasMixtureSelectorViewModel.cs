using System;
using System.Collections.ObjectModel;
using System.Reactive;
using BubblesDivePlanner.Contracts.Models.Plan;
using ReactiveUI;

namespace BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Plan
{
    public interface IGasMixtureSelectorViewModel : IGasMixtureSelectorModel
    {
        ObservableCollection<IGasMixtureModel> GasMixtures { get; }

        IObservable<bool> CanAddGasMixture { get; }

        ReactiveCommand<Unit, Unit> AddGasMixtureCommand { get; }

        bool ValidateGasMixture(IGasMixtureModel selectedGasMixture);
    }
}