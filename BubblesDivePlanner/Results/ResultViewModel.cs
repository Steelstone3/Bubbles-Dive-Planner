using System.Collections.Generic;
using BubblesDivePlanner.DiveModels.DiveProfile;
using BubblesDivePlanner.DiveStep;
using System.Reactive;

namespace BubblesDivePlanner.Results
{
    public class ResultViewModel : ReactiveObject, IResultModel
    {
        private IDiveStepModel _diveStepModel;
        public IDiveStepModel DiveStepModel
        {
            get => _diveStepModel;
            set => this.RaiseAndSetIfChanged(ref _diveStepModel, value);
        }

        private IDiveStepModel _diveProfileModel;
        public IDiveStepModel DiveProfileModel
        {
            get => _diveProfileModel;
            set => this.RaiseAndSetIfChanged(ref _diveProfileModel, value);
        }
    }
}