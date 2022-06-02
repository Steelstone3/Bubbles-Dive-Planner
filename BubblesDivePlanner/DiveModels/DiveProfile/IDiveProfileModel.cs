using System.Collections.Generic;

namespace BubblesDivePlanner.DiveModels.DiveProfile
{
    public interface IDiveProfileModel
    {
        double[] MaxSurfacePressures { get; set; }
        double[] CompartmentLoad { get; set; }
        double[] TissuePressuresNitrogen { get; set; }
        double[] TissuePressuresHelium { get; set; }
        double[] TissuePressuresTotal { get; set; }
        double[] ToleratedAmbientPressures { get; set; }
        double[] AValues { get; set; }
        double[] BValues { get; set; }
        double PressureOxygen { get; set; }
        double PressureHelium { get; set; }
        double PressureNitrogen { get; set; }
        IDiveProfileModel DeepClone();
    }
}