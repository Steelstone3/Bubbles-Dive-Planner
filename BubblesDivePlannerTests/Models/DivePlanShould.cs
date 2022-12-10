using System.Collections.Generic;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Models.DiveModels.Types;
using Xunit;

namespace BubblesDivePlannerTests.Models
{
    public class DivePlanShould
    {
        private readonly IDiveModel diveModel = new Zhl16Buhlmann(null);
        private readonly ICylinder cylinder = TestFixture.FixtureSelectedCylinder;
        private readonly IDiveStep diveStep = TestFixture.FixtureDiveStep;
        private readonly List<ICylinder> cylinders = new();
        private readonly IDivePlan divePlan;

        public DivePlanShould()
        {
            cylinders.Add(cylinder);
            divePlan = new DivePlan(diveModel, cylinders, diveStep, cylinder);
        }

        [Fact]
        public void ContainADiveModel()
        {
            IDivePlan divePlan = new DivePlan(diveModel, cylinders, diveStep, cylinder);

            Assert.NotNull(divePlan.DiveModel);
        }

        [Fact]
        public void ContainCylinders()
        {
            Assert.NotNull(divePlan.Cylinders);
        }

        [Fact]
        public void ContainADiveStep()
        {
            Assert.NotNull(divePlan.DiveStep);
        }

        [Fact]
        public void ContainASelectedCylinder()
        {
            Assert.NotNull(divePlan.SelectedCylinder);
        }
    }
}