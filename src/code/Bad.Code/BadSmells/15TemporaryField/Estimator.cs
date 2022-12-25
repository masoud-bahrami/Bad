using System;
using System.Collections.Generic;
using System.Linq;

namespace Bad.Code.BadSmells._15TemporaryField
{
    /// <summary>
    /// https://blog.ploeh.dk/2015/09/18/temporary-field-code-smell/
    /// In this example, a developer was asked to provide an estimate of a duration, based on a collection of previously observed durations.
    /// The requirements are these:
    ///     There's a collection of previously observed durations. These must be used as statistics upon which to base the estimate.
    ///     It's better to estimate too high than too low.
    ///     Durations are assumed to be normal distributed.
    ///     The estimate should be higher than the actual duration in more than 99% of the times.
    /// If there are no previous observations, a default estimate must be used as a fall-back mechanism.
    /// </summary>
    public class Estimator
    {
        private readonly TimeSpan _defaultEstimate;
        private IReadOnlyCollection<TimeSpan> _durations;
        private TimeSpan _average;
        private TimeSpan _standardDeviation;

        public Estimator(TimeSpan defaultEstimate)
        {
            this._defaultEstimate = defaultEstimate;
        }

        public TimeSpan CalculateEstimate(IReadOnlyCollection<TimeSpan> durations)
        {
            if (durations == null)
                throw new ArgumentNullException(nameof(durations));

            if (durations.Count == 0)
                return this._defaultEstimate;

            this._durations = durations;
            this.CalculateAverage();
            this.CalculateStandardDeviation();

            var margin = TimeSpan.FromTicks(this._standardDeviation.Ticks * 3);
            return this._average + margin;
        }

        private void CalculateAverage()
        {
            this._average = TimeSpan.FromTicks((long)this._durations.Average(ts => ts.Ticks));
        }

        private void CalculateStandardDeviation()
        {
            var variance = this._durations.Average(ts => Math.Pow((ts - this._average).Ticks, 2));
            this._standardDeviation = TimeSpan.FromTicks((long)Math.Sqrt(variance));
        }
    }
}