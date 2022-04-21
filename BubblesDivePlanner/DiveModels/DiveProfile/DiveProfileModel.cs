using System.Collections.Generic;

namespace BubblesDivePlanner.DiveModels.DiveProfile
{
    public class DiveProfileModel : IDiveProfileModel
    {
        public IList<double> MaxSurfacePressures { get; set; } = new List<double>();
        public IList<double> TissuePressuresNitrogen { get; set; } = new List<double>();
        public IList<double> TissuePressuresHelium { get; set; } = new List<double>();
        public IList<double> TissuePressuresTotal { get; set; } = new List<double>();
        public IList<double> ToleratedAmbientPressures { get; set; } = new List<double>();
        public IList<double> AValues { get; set; } = new List<double>();
        public IList<double> BValues { get; set; } = new List<double>();
        public IList<double> CompartmentLoad { get; set; } = new List<double>();
        public double PressureOxygen { get; set; } = 0.0;
        public double PressureHelium { get; set; } = 0.0;
        public double PressureNitrogen { get; set; } = 0.0;

        public IDiveProfileModel DeepClone()
        {
            return new DiveProfileModel()
            {
                MaxSurfacePressures = this.MaxSurfacePressures,
                TissuePressuresNitrogen = this.TissuePressuresNitrogen,
                TissuePressuresHelium = this.TissuePressuresHelium,
                TissuePressuresTotal = this.TissuePressuresTotal,
                ToleratedAmbientPressures = this.ToleratedAmbientPressures,
                AValues = this.AValues,
                BValues = this.BValues,
                CompartmentLoad = this.CompartmentLoad,
                PressureOxygen = this.PressureOxygen,
                PressureHelium = this.PressureHelium,
                PressureNitrogen = this.PressureNitrogen,
            };
        }
    }
}