using System;

namespace BudgetLib
{
    public class Budget
    {
        public string YearMonth { private get; set; }
        public decimal Amount { private get; set; }

        private DateTime FirstDay => DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null);

        private DateTime LastDay => DateTime.ParseExact(YearMonth + DaysInMonth, "yyyyMMdd", null);

        private int DaysInMonth => DateTime.DaysInMonth(FirstDay.Year, FirstDay.Month);

        private decimal DailyAmount => Amount / DaysInMonth;

        private Period BudgetPeriod => new Period(FirstDay, LastDay);

        public decimal MonthlyAmount(Period period)
        {
            return DailyAmount * period.OverlappingDays(BudgetPeriod);
        }
    }
}
