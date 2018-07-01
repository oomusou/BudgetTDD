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
            if (End < budget.FirstDay || Start > budget.LastDay)
            {
                return 0m;
            }

            return Days;
        }
    }
}