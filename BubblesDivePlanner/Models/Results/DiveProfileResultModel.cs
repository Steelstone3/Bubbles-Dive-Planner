using BubblesDivePlanner.Contracts.Models.Results;

namespace BubblesDivePlanner.Models.Results
{
	public class DiveProfileResultModel : IDiveProfileResultModel
	{
		public string DiveProfileStepHeader
		{
			get; set;
		}

		public int Compartment
		{
			get; set;
		} = 0;

		public double TissuePressureResult
		{
			get; set;
		} = 0;

		public double ToleratedAmbientPressureResult
		{
			get; set;
		} = 0;

		public double MaximumSurfacePressureResult
		{
			get; set;
		} = 0;

		public double CompartmentLoadResult
		{
			get; set;
		} = 0;
	}
}
