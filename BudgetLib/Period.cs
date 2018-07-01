using System;

namespace BudgetLib
{
    public class Period
    {
        public Period(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; }
        public DateTime End { get; }

        public decimal OverlappingDays(Budget budget)
        {
            if (HasNoOverlap(budget))
            {
                return 0m;
            }

            var overlapEnd = End < budget.LastDay ? End : budget.LastDay;
            var overlapStart = Start > budget.FirstDay ? Start : budget.FirstDay;
            return (overlapEnd - overlapStart).Days + 1;
        }

        private bool HasNoOverlap(Budget budget)
        {
            return End < budget.FirstDay || Start > budget.LastDay;
        }
    }
}