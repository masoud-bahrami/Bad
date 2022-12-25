using System;

namespace Bad.Code.BadSmells._05MutableData
{
    public class DistanceService
    {

        public double DistanceTraveled(Scenario scenario, int time)
        {
            double acc = scenario.PrimaryForce / scenario.Mass;
            double primaryTime = Math.Min(time, scenario.Delay);

            var result = 0.5 * acc * primaryTime * primaryTime;

            double secondaryTime = time - scenario.Delay;
            if (secondaryTime > 0)
            {
                double primaryVelocity = acc * scenario.Delay;

                acc = (scenario.PrimaryForce + scenario.SecondaryForce) / scenario.Mass;
                result += primaryVelocity * secondaryTime + 0.5 * acc * secondaryTime * secondaryTime;
            }

            return result;
        }
    }

    public class Scenario
    {
        public double Mass;
        public double PrimaryForce;
        public int Delay;
        public double SecondaryForce;
    }
}