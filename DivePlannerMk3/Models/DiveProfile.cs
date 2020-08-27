using DivePlannerMK3.Contracts;
using System.Collections.Generic;

namespace DivePlannerMk3.Models
{
    public class DiveProfile : IDiveProfile
    {
        public DiveProfile()
        {
            MaxSurfacePressures = new List<double>();
            TissuePressuresNitrogen = new List<double>();
            TissuePressuresHelium = new List<double>();
            TissuePressuresTotal = new List<double>();
            ToleratedAmbientPressures = new List<double>();
            CompartmentLoad = new List<double>();
        }

        public List<double> MaxSurfacePressures
        {
            get; set;
        }

        public List<double> CompartmentLoad
        {
            get; set;
        }

        public List<double> TissuePressuresNitrogen
        {
            get; set;
        }

        public List<double> TissuePressuresHelium
        {
            get; set;
        }

        public List<double> TissuePressuresTotal
        {
            get; set;
        }

        public List<double> ToleratedAmbientPressures
        {
            get; set;
        }

        public double PressureOxygen
        {
            get; set;
        }

        public double PressureHelium
        {
            get; set;
        }

        public double PressureNitrogen
        {
            get; set;
        }

    }
}