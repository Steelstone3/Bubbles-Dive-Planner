using BubblesDivePlanner.ViewModels.Model.Plan.DiveModels;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Plan.DiveModels
{
    public class DiveProfile : ViewModelBase, IDiveProfile
    {
        public DiveProfile(int compartments)
        {
            MaxSurfacePressures = new double[compartments];
            NitrogenTissuePressures = new double[compartments];
            HeliumTissuePressures = new double[compartments];
            TotalTissuePressures = new double[compartments];
            ToleratedAmbientPressures = new double[compartments];
            AValues = new double[compartments];
            BValues = new double[compartments];
            CompartmentLoads = new double[compartments];

            for (int i = 0; i < compartments; i++)
            {
                NitrogenTissuePressures[i] = 0.79;
                TotalTissuePressures[i] = 0.79;
            }
        }

        public double[] maxSurfacePressures;
        public double[] MaxSurfacePressures
        {
            get => maxSurfacePressures;
            set => this.RaiseAndSetIfChanged(ref maxSurfacePressures, value);
        }

        public double[] nitrogenTissuePressures;
        public double[] NitrogenTissuePressures
        {
            get => nitrogenTissuePressures;
            set => this.RaiseAndSetIfChanged(ref nitrogenTissuePressures, value);
        }

        public double[] heliumTissuePressures;
        public double[] HeliumTissuePressures
        {
            get => heliumTissuePressures;
            set => this.RaiseAndSetIfChanged(ref heliumTissuePressures, value);
        }

        public double[] totalTissuePressures;
        public double[] TotalTissuePressures
        {
            get => totalTissuePressures;
            set => this.RaiseAndSetIfChanged(ref totalTissuePressures, value);
        }

        public double[] toleratedAmbientPressures;
        public double[] ToleratedAmbientPressures
        {
            get => toleratedAmbientPressures;
            set => this.RaiseAndSetIfChanged(ref toleratedAmbientPressures, value);
        }

        public double[] aValues;
        public double[] AValues
        {
            get => aValues;
            set => this.RaiseAndSetIfChanged(ref aValues, value);
        }

        public double[] bValues;
        public double[] BValues
        {
            get => bValues;
            set => this.RaiseAndSetIfChanged(ref bValues, value);
        }

        public double[] compartmentLoads;
        public double[] CompartmentLoads
        {
            get => compartmentLoads;
            set => this.RaiseAndSetIfChanged(ref compartmentLoads, value);
        }

        public double oxygenAtPressure;
        public double OxygenAtPressure
        {
            get => oxygenAtPressure;
            set => this.RaiseAndSetIfChanged(ref oxygenAtPressure, value);
        }

        public double nitrogenAtPressure;
        public double NitrogenAtPressure
        {
            get => nitrogenAtPressure;
            set => this.RaiseAndSetIfChanged(ref nitrogenAtPressure, value);
        }

        public double heliumAtPressure;
        public double HeliumAtPressure
        {
            get => heliumAtPressure;
            set => this.RaiseAndSetIfChanged(ref heliumAtPressure, value);
        }
    }
}