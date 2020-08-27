using System.Collections.Generic;

namespace DivePlannerMK3.Contracts
{
    public interface IDiveProfile
    {
		List<double> MaxSurfacePressures
		{
			get; set;
		}

		double PressureOxygen
		{
			get; set;
		}

		double PressureHelium
		{
			get; set;
		}

		double PressureNitrogen
		{
			get; set;
		}

		List<double> CompartmentLoad
		{
			get; set;
		}

		List<double> TissuePressuresNitrogen
		{
			get; set;
		}

		List<double> TissuePressuresHelium
		{
			get; set;
		}

		List<double> TissuePressuresTotal
		{
			get; set;
		}

		List<double> ToleratedAmbientPressures
		{
			get; set;
		}
    }
}