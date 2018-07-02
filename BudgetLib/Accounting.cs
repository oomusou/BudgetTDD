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
                var budget = budgets[0];
                var otherPeriod = new Period(budget.FirstDay, budget.LastDay);
                return budget.DailyAmount * period.OverlappingDays(otherPeriod);
            }

            return 0m;
        }
    }
}