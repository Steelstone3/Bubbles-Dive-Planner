namespace BubblesDivePlanner.Contracts.Models.Results
{
    public interface IDiveProfileResultModel
    {
        int Compartment { get; set; }

        double TissuePressureResult { get; set; }

        double ToleratedAmbientPressureResult { get; set; }

        double MaximumSurfacePressureResult { get; set; }

        double CompartmentLoadResult { get; set; }
    }
}