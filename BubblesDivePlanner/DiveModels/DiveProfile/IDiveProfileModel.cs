using System.Collections.Generic;

namespace BubblesDivePlanner.DiveModels.DiveProfile
{
    public interface IDiveProfileModel
    {
        IList<double> MaxSurfacePressures { get; set; }
        IList<double> CompartmentLoad { get; set; }
        IList<double> TissuePressuresNitrogen { get; set; }
        IList<double> TissuePressuresHelium { get; set; }
        IList<double> TissuePressuresTotal { get; set; }
        IList<double> ToleratedAmbientPressures { get; set; }
        IList<double> AValues { get; set; }
        IList<double> BValues { get; set; }
        double PressureOxygen { get; set; }
        double PressureHelium { get; set; }
        double PressureNitrogen { get; set; }
        IDiveProfileModel DeepClone();
    }
}