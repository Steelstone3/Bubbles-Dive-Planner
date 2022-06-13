using System.Collections.Generic;
using System.Collections.ObjectModel;
using BubblesDivePlanner.DiveStep;
using ReactiveUI;

namespace BubblesDivePlanner.DecompressionProfile
{
    public class DecompressionProfileViewModel : ReactiveObject, IDecompressionProfileModel
    {
        // private IList<IDiveStepModel> _decompressionDiveSteps = new List<IDiveStepModel>();
        // public IList<IDiveStepModel> DecompressionDiveSteps
        // {
        //     get => _decompressionDiveSteps;
        //     set => this.RaiseAndSetIfChanged(ref _decompressionDiveSteps, value);
        // }

        public ObservableCollection<IDiveStepModel> DecompressionDiveSteps
        {
            get;
        } = new ObservableCollection<IDiveStepModel>();

    }
}