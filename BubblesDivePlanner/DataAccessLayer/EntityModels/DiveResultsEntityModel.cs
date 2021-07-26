using System.Collections.Generic;
using DivePlannerMk3.Contracts;
using DivePlannerMk3.Contracts.DataAccessContracts;
using DivePlannerMk3.Models;

namespace DivePlannerMk3.DataAccessLayer.EntityModels
{
    public class DiveResultsEntityModel : IEntityModel
    {
        public string DiveProfileStepHeader { get; set; }
        public string DiveModelUsed { get; set; }
        public int Depth { get; set; }
        public int Time { get; set; }
        public string GasName { get; set; }
        public double Oxygen { get; set; }
        public double Helium { get; set; }
        public double Nitrogen { get; set; }
        public List<DiveResultsModel> DiveResults { get; set; } = new List<DiveResultsModel>();
    }
}