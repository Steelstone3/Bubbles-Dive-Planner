using System;
using System.Collections.Generic;
using DivePlannerMk3.Contracts;
using DivePlannerMk3.Models;
using DivePlannerMk3.ViewModels.DiveInfo;
using DivePlannerMk3.ViewModels.DivePlan;
using DivePlannerMk3.ViewModels.DiveResult;
using DivePlannerMK3.Contracts;

namespace DivePlannerMk3.Controllers
{
    public class DiveProfileService : IDiveProfileService
    {
        //private double aValues = 0.0;
        //private double bValues = 0.0;
        private IDiveProfile _diveProfile = new DiveProfile();

        //TODO AH Composition

        private IDiveModel _theDiveModel;
        public IDiveModel TheDiveModel
        {
            get => _theDiveModel;
            set
            {
                _theDiveModel = value;
                InitaliseDiveProfile();
            }
        }

        //TODO AH Strategy pattern

        public DiveProfileResultsListViewModel RunDiveStep(PlanDiveStepViewModel diveStep, PlanGasMixtureViewModel gasMixture)
        {
            //TODO AH reconsider the whole output and input view model architechture to optimise the amount of classes...
            /*var outputResults = new DiveProfileResultsListViewModel()
            {
                DiveProfileStepHeader = _theDiveModel.DiveModelName + "\r\nDepth: " + diveStep.Depth.ToString() + " Time: " + diveStep.Time.ToString(),
            };

            var updatedOutputResults = DiveParameterOutput( diveStep, gasMixture, outputResults );

            return RunDiveStep( diveStep, gasMixture, updatedOutputResults );*/

            IDiveStage[] diveStages = new IDiveStage[]
            {
                new DiveStageTissuePressure(_theDiveModel, _diveProfile, diveStep.Time),
                new DiveStageABValues(_theDiveModel, _diveProfile),
                new DiveStageToleratedAmbientPressure(),
                new DiveStageMaximumSurfacePressure(),
                new DiveStageCompartmentLoad(),
            };

            foreach(var stage in diveStages)
            {
                stage.RunStage();
            }

            return null;
        }

        //TODO AH finish this later
        /*public IEnumerable<DiveProfileResultsListViewModel> RunDecompressionDiveSteps(InfoDecompressionProfileViewModel decompressionDiveSteps, PlanGasMixtureViewModel gasMixture)
        {
            //TODO AH Insert Dive Step Calculation and environment dependencies

            //TODO AH consider how the dive model and dive profile are to be updated external to this class' state post calculations


            foreach( var diveStep in decompressionDiveSteps.DecoDiveSteps )
            {
            var outputResults = new DiveProfileResultsListViewModel()
            {
                DiveProfileStepHeader = _theDiveModel.DiveModelName + "\r\nDepth: " + diveStep.Depth.ToString() + " Time: " + diveStep.Time.ToString(),
            };
                var updatedOutputResults = DiveParameterOutput( diveStep, gasMixture, outputResults );

                yield return RunDiveStep( diveStep, gasMixture, updatedOutputResults );
            }
        }*/


        private DiveProfileResultsListViewModel DiveParameterOutput(PlanDiveStepViewModel diveStep, PlanGasMixtureViewModel gasMixture, DiveProfileResultsListViewModel outputResults)
        {
            outputResults.ParameterOutput.DiveModelUsed = _theDiveModel.DiveModelName;
            outputResults.ParameterOutput.StepDepthParameter = diveStep.Depth;
            outputResults.ParameterOutput.StepTimeParameter = diveStep.Time;
            outputResults.ParameterOutput.StepGasMixNameParameter = gasMixture.SelectedGasMixture.GasName;

            //TODO AH add when gas management is integrated
            //outputResults.ParameterOutput.GasUsedParameter
            //outputResults.ParameterOutput.GasRemainingParameter

            return outputResults;
        }

        private DiveProfileResultsListViewModel RunDiveStep(PlanDiveStepViewModel diveStep, PlanGasMixtureViewModel gasMixture, DiveProfileResultsListViewModel outputResults)
        {
            for (int compartment = 0; compartment < TheDiveModel.CompartmentIndexMax; compartment++)
            {
                IDiveProfileStepOutputModel output = new DiveProfileStepOutputModel();
                //SetAmbientPressures( gasMixture.SelectedGasMixture.Oxygen, gasMixture.SelectedGasMixture.Helium, diveStep.Depth );

                //output.Compartment = compartment + 1;
                //output.TissuePressureResult = Math.Round( CalculateTissuePressures( compartment, diveStep.Time ), 2 );
                //CalculateABValues( compartment );
                //output.ToleratedAmbientPressureResult = Math.Round( CalculateToleratedAmbientPressure( compartment ), 2 );
                //output.MaximumSurfacePressureResult = Math.Round( CalculateMaximumSurfacePressure( compartment ), 2 );
                //output.CompartmentLoadResult = Math.Round( CalculateCompartmentLoad( compartment ), 2 );

                outputResults.DiveProfileStepOutput.Add(output);
            }

            return outputResults;
        }

        /*private void SetAmbientPressures( double oxygenPercentage, double heliumPercentage, int depth )
        {

            //taken from user input used to calculate the pressure at depth for nitrogen
            //calcs nitrogen pressure being breathed
            double nitrogenFraction = 1.0f - ( oxygenPercentage / 100 + heliumPercentage / 100 );

            //calculates ambient pressure
            double pressureAmbient = 1.0f + (double)depth / 10.0f;

            //calculates ambient pressure of each gas
            _diveProfile.PressureNitrogen = nitrogenFraction * pressureAmbient;
            _diveProfile.PressureOxygen = oxygenPercentage / 100 * pressureAmbient;
            _diveProfile.PressureHelium = heliumPercentage / 100 * pressureAmbient;
        }

        private double CalculateTissuePressures( int compartmentCount, int bottomTime )
        {
            //works out tissue pressure for a given compartment (Nitrogen)
            _diveProfile.TissuePressuresNitrogen[compartmentCount] = _diveProfile.TissuePressuresNitrogen[compartmentCount] + ( ( _diveProfile.PressureNitrogen - _diveProfile.TissuePressuresNitrogen[compartmentCount] ) * ( 1.0f - Math.Pow( 2.0f, -( bottomTime / TheDiveModel.NitrogenHalfTime[compartmentCount] ) ) ) );
            //works out tissue pressure for a given compartment (Helium)
            _diveProfile.TissuePressuresHelium[compartmentCount] = _diveProfile.TissuePressuresHelium[compartmentCount] + ( ( _diveProfile.PressureHelium - _diveProfile.TissuePressuresHelium[compartmentCount] ) * ( 1.0f - Math.Pow( 2.0f, -( bottomTime / TheDiveModel.HeliumHalfTime[compartmentCount] ) ) ) );

            //total combined tissue pressure
            return _diveProfile.TissuePressuresTotal[compartmentCount] = _diveProfile.TissuePressuresHelium[compartmentCount] + _diveProfile.TissuePressuresNitrogen[compartmentCount];
        }

        private void CalculateABValues( int compartmentCount )
        {
            //a and b coefficients set based on user input
            aValues = ( ( TheDiveModel.AValuesNitrogen[compartmentCount] * _diveProfile.TissuePressuresNitrogen[compartmentCount] ) + ( TheDiveModel.AValuesHelium[compartmentCount] * _diveProfile.TissuePressuresHelium[compartmentCount] ) ) / _diveProfile.TissuePressuresTotal[compartmentCount];
            bValues = ( ( TheDiveModel.BValuesNitrogen[compartmentCount] * _diveProfile.TissuePressuresNitrogen[compartmentCount] ) + ( TheDiveModel.BValuesHelium[compartmentCount] * _diveProfile.TissuePressuresHelium[compartmentCount] ) ) / _diveProfile.TissuePressuresTotal[compartmentCount];
        }

        private double CalculateToleratedAmbientPressure( int compartmentCount )
        {
            //calculates tolerated ambient pressure of diver based on user inputs
            return _diveProfile.ToleratedAmbientPressures[compartmentCount] = ( _diveProfile.TissuePressuresTotal[compartmentCount] - aValues ) * bValues;
        }

        private double CalculateMaximumSurfacePressure( int compartmentCount )
        {
            return _diveProfile.MaxSurfacePressures[compartmentCount] = ( 1.0f / bValues ) + aValues;
        }

        private double CalculateCompartmentLoad( int compartmentCount )
        {
            return _diveProfile.CompartmentLoad[compartmentCount] = _diveProfile.TissuePressuresTotal[compartmentCount] / _diveProfile.MaxSurfacePressures[compartmentCount] * 100;
        }*/

        private void InitaliseDiveProfile()
        {
            ResetDiveProfile();

            for (int i = 0; i < TheDiveModel.CompartmentIndexMax; i++)
            {
                _diveProfile.MaxSurfacePressures.Add(0);
                _diveProfile.ToleratedAmbientPressures.Add(0);
                _diveProfile.CompartmentLoad.Add(0);

                _diveProfile.TissuePressuresNitrogen.Add(0.79);
                _diveProfile.TissuePressuresHelium.Add(0);
                _diveProfile.TissuePressuresTotal.Add(0);
            }

        }

        private void ResetDiveProfile()
        {
            _diveProfile.MaxSurfacePressures.Clear();
            _diveProfile.ToleratedAmbientPressures.Clear();
            _diveProfile.CompartmentLoad.Clear();

            _diveProfile.TissuePressuresNitrogen.Clear();
            _diveProfile.TissuePressuresHelium.Clear();
            _diveProfile.TissuePressuresTotal.Clear();
        }

    }
}
