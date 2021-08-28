using BubblesDivePlanner.Contracts.ViewModels.Results;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Result
{
    public class DiveParametersResultViewModel : ViewModelBase, IDiveParametersResultViewModel
    {
        private string _diveProfileStepHeader;
        public string DiveProfileStepHeader
        {
            get => _diveProfileStepHeader;
            set => this.RaiseAndSetIfChanged(ref _diveProfileStepHeader, value);
        }

        //TODO May need a property using this model GasUsageInfoModel and assign it in the pre dive stage where the parameters are calculated

        private string _diveModelUsed;
        public string DiveModelUsed
        {
            get => _diveModelUsed;
            set => this.RaiseAndSetIfChanged(ref _diveModelUsed, value);
        }

        private int _depth;
        public int Depth
        {
            get => _depth;
            set => this.RaiseAndSetIfChanged(ref _depth, value);
        }
        private int _time;
        public int Time
        {
            get => _time;
            set => this.RaiseAndSetIfChanged(ref _time, value);
        }

        private string _gasName;
        public string GasName
        {
            get => _gasName;
            set => this.RaiseAndSetIfChanged(ref _gasName, value);
        }

        private double _oxygen;
        public double Oxygen
        {
            get => _oxygen;
            set => this.RaiseAndSetIfChanged(ref _oxygen, value);
        }

        private double _helium;
        public double Helium
        {
            get => _helium;
            set => this.RaiseAndSetIfChanged(ref _helium, value);
        }

        private double _nitrogen;
        public double Nitrogen
        {
            get => _nitrogen;
            set => this.RaiseAndSetIfChanged(ref _nitrogen, value);
        }

        private double _diveCeiling;
        public double DiveCeiling 
        { 
            get => _diveCeiling;
            set => this.RaiseAndSetIfChanged(ref _diveCeiling, value);
        }
    }
}