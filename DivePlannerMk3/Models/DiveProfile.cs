using DivePlannerMK3.Contracts;
using System.Collections.Generic;

namespace DivePlannerMk3.Models
{
    public class DiveProfile : IDiveProfile
    {
        public DiveProfile()
        {

        }

        public List<double> MaxSurfacePressures
        {
            get; set;
        } = new List<double>();

        public List<double> CompartmentLoad
        {
            get; set;
        } = new List<double>();

        public List<double> TissuePressuresNitrogen
        {
            get; set;
        } = new List<double>();

        public List<double> TissuePressuresHelium
        {
            get; set;
        } = new List<double>();

        public List<double> TissuePressuresTotal
        {
            get; set;
        } = new List<double>();

        public List<double> ToleratedAmbientPressures
        {
            get; set;
        } = new List<double>();

        public List<double> AValues
        {
            get; set;
        } = new List<double>();

        public List<double> BValues
        {
            get; set;
        } = new List<double>();

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
