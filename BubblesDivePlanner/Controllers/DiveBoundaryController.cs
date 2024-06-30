using System.Collections;
using System.Collections.Generic;

public class DiveBoundaryController : IDiveBoundaryController
{
    public float CalculateMaximumOperatingDepth(float oxygen)
    {
        float toleratedPartialPressure = 1.4F;
        float oxygenPartialPressure = oxygen / 100;
        float toleratedPressure = toleratedPartialPressure / oxygenPartialPressure;
        float maximumOperatingDepth = (float)Math.Round((toleratedPressure * 10) - 10, 2);

        return maximumOperatingDepth;
    }

    public float CalculateDiveCeiling(float[] toleratedAmbientPressures)
    {
        return (float)Math.Round((toleratedAmbientPressures.Max() - 1.0) * 10.0, 2);
    }

    public float GetOverallDiveCeiling(IEnumerable<IDiveStage> Results)
    {
        if (Results == null || Results.LastOrDefault() == null)
        {
            return 0.0F;
        }
        else
        {
            return Results.LastOrDefault().DiveModel.DiveModelProfile.DiveCeiling;
        }
    }
}

public interface IDiveBoundaryController
{
    float CalculateMaximumOperatingDepth(float oxygen);
    float CalculateDiveCeiling(float[] toleratedAmbientPressures);
    float GetOverallDiveCeiling(IEnumerable<IDiveStage> Results);
}