using System;
using System.Linq;
using System.Collections.Generic;

namespace DivePlannerMk3.Controllers
{
    public class DiveCeilingController
    {
        public double CalculateDiveCeiling(List<double> toleratedAmbientPressures)
        {
            return ( toleratedAmbientPressures.Max() - 1.0 ) * 10.0;
        }
    }
}