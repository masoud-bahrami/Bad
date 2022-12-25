using System.Collections.Generic;

//Replace Derived Variable With Query

namespace Bad.Code.BadSmells._05MutableData
{

    public class ProductionPlan
    {
        private readonly List<Plan> _adjustments;
        private decimal _production;

        public ProductionPlan(List<Plan> adjustments)
        {
            _adjustments = adjustments;
        }

        public decimal Production()
        {
            return _production;
        }

        public void ApplyAdjustment(Plan anPlan)
        {
            this._adjustments.Add(anPlan);
            this._production += anPlan.Amount;
        }
    }

    public class Plan
    {
        public decimal Amount;
        public object Product;
    }
}