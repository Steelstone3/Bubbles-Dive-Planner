using System.Collections.Generic;
using System.IO;
using BubblesDivePlanner.Controllers.Json;
using BubblesDivePlanner.Models;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Controllers.Json
{
    public class FileControllerShould
    {
        private readonly Mock<IJsonController> jsonController = new();
        private readonly IDivePlan divePlan;
        private readonly List<IDivePlan> expectedDivePlans = new();
        private IFileController fileController;

        public FileControllerShould()
        {
            divePlan = new DivePlan(TestFixture.ExpectedDiveModel, TestFixture.ExpectedCylinders(), TestFixture.FixtureDiveStep, TestFixture.ExpectedSelectedCylinder);
            expectedDivePlans.Add(divePlan);
            expectedDivePlans.Add(divePlan);
        }

        [Fact]
        public void SaveFile()
        {
            // Given
            jsonController.Setup(jc => jc.Serialise(expectedDivePlans));
            fileController = new FileController(jsonController.Object, expectedDivePlans);
            fileController.AddDivePlan(divePlan);
            fileController.AddDivePlan(divePlan);

            // When
            fileController.SaveFile();

            // Then
            jsonController.VerifyAll();
        }

        [SkippableFact]
        public void LoadFile()
        {
            // Given
            Skip.If(File.Exists("dive_plan.json"));

            fileController = new FileController(jsonController.Object, expectedDivePlans);

            // When
            var result = fileController.LoadFile();

            // Then
            Assert.NotNull(result);
        }

        [Fact]
        public void Intergrate()
        {
            // Given
            var expectedDivePlan = new DivePlan(TestFixture.FixtureDiveModel(null), TestFixture.FixtureCylinders(), TestFixture.FixtureDiveStep, TestFixture.FixtureSelectedCylinder);
            fileController = new FileController(new JsonController(), new());
            fileController.AddDivePlan(divePlan);
            fileController.AddDivePlan(divePlan);

            // When
            fileController.AddDivePlan(expectedDivePlan);
            fileController.AddDivePlan(expectedDivePlan);
            fileController.SaveFile();
            var actualDivePlan = fileController.LoadFile();

            // Then
            Assert.Equivalent(expectedDivePlan.DiveModel, actualDivePlan.DiveModel);

            for (int i = 0; i < expectedDivePlan.Cylinders.Count; i++)
            {
                Assert.Equivalent(expectedDivePlan.Cylinders[i], actualDivePlan.Cylinders[i]);
            }
        }
    }
}