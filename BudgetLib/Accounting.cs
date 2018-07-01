using System;
using System.Linq;

namespace BudgetLib
{
    public class Accounting
    {
        private readonly IBudgetRepository _budgetRepository;

        public Accounting(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public decimal TotalAmount(Period period)
        {
            var budgets = _budgetRepository.GetAll();

            if (budgets.Any())
            {
                if (period.End < DateTime.ParseExact(budgets[0].YearMonth + "01", "yyyyMMdd", null))
                {
                    return 0m;
                }

                return period.Days;
            }

            return 0m;
        }
    }
}