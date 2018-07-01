using System;

namespace BudgetLib
{
    public class Budget
    {
        public string YearMonth { get; set; }
        public decimal Amount { get; set; }

        public DateTime FirstDay => DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null);

        public DateTime LastDay
        {
            get
            {
                var daysInMonth = DateTime.DaysInMonth(FirstDay.Year, FirstDay.Month);
                return DateTime.ParseExact(YearMonth + daysInMonth, "yyyyMMdd", null);
            }
        }
    }
}