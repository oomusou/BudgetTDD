using System;

namespace BudgetLib
{
    public class Period
    {
        public Period(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new ArgumentException();
            }

            Start = start;
            End = end;
        }

        public DateTime Start { get; }
        public DateTime End { get; }

        public decimal OverlappingDays(Period otherPeriod)
        {
            if (End < otherPeriod.Start || Start > otherPeriod.End)
            {
                return 0m;
            }

            var overlapEnd = End < otherPeriod.End ? End : otherPeriod.End;
            var overlapStart = Start > otherPeriod.Start ? Start : otherPeriod.Start;
            return (overlapEnd - overlapStart).Days + 1;
        }
    }
}