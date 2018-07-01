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

        public decimal Days => (End - Start).Days + 1;

        public decimal OverlappingDays(Budget budget)
        {
            if (HasNoOverlap(budget))
            {
                return 0m;
            }

            if (End >= budget.LastDay)
            {
                return (budget.LastDay - Start).Days + 1;
            }

            return Days;
        }

        private bool HasNoOverlap(Budget budget)
        {
            return End < budget.FirstDay || Start > budget.LastDay;
        }
    }
}