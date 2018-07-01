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
                var overlappingDays = period.OverlappingDays(budgets[0]);
                var dailyAmount = budgets[0].Amount / budgets[0].DaysInMonth;
                return dailyAmount * overlappingDays;
            }

            return 0m;
        }
    }
}