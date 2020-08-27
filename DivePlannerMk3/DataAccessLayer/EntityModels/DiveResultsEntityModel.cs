using System.Collections.ObjectModel;
using DivePlannerMk3.Contracts.DataAccessContracts;
using DivePlannerMk3.Models;

namespace DivePlannerMk3.DataAccessLayer.EntityModels
{
    public class DiveResultsEntityModel : IEntityModel
    {
        public string DiveProfileStepHeader { get; internal set; }
        public string DiveModelUsed { get; internal set; }
        public int Depth { get; internal set; }
        public int Time { get; internal set; }
        public string GasName { get; internal set; }
        public double Oxygen { get; internal set; }
        public double Helium { get; internal set; }
        public double Nitrogen { get; internal set; }
    }
}