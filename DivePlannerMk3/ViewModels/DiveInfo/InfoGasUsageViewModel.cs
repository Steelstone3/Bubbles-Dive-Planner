using System;
using DivePlannerMk3.Contracts;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DiveInfo
{
    public class InfoGasUsageViewModel : ViewModelBase, IGasUsageModel
    {
        public InfoGasUsageViewModel()
        {
            UiEnabled = false;
        }

        private int _gasUsedForStep;
        public int GasUsedForStep
        {
            get => _gasUsedForStep;
            set => this.RaiseAndSetIfChanged(ref _gasUsedForStep, value);
        }

        private int _gasRemaining;
        public int GasRemaining
        {
            get => _gasRemaining;
            set => this.RaiseAndSetIfChanged(ref _gasRemaining, value);
        }

        public void CalculateGasUsage()
        {
            throw new NotImplementedException();
        }
    }
}