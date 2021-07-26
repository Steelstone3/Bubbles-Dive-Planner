using DivePlannerMk3.Contracts;

namespace DivePlannerMk3.Models
{
	public class DiveProfileStepOutputModel : IDiveProfileStepOutputModel
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
