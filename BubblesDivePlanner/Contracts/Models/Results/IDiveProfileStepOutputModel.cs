namespace BubblesDivePlanner.Contracts.Models.Results
{
	public interface IDiveProfileStepOutputModel
    {
		int Compartment
		{
			get; set;
		}

		double TissuePressureResult
		{
			get; set;
		}

		double ToleratedAmbientPressureResult
		{
			get; set;
		}

		double MaximumSurfacePressureResult
		{
			get; set;
		}

		double CompartmentLoadResult
		{
			get; set;
		}
	}
}
