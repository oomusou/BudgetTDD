using System;

namespace BudgetLib
{
    public class Budget
    {
        public string YearMonth { get; set; }
        public decimal Amount { get; set; }

        public DateTime FirstDay => DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null);

        public DateTime LastDay => DateTime.ParseExact(YearMonth + DaysInMonth, "yyyyMMdd", null);

        public int DaysInMonth => DateTime.DaysInMonth(FirstDay.Year, FirstDay.Month);

        public decimal DailyAmount => Amount / DaysInMonth;
    }
}