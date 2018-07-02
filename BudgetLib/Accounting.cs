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
                var totalAmount = 0m;

                foreach (var budget in budgets)
                {
                    var otherPeriod = new Period(budget.FirstDay, budget.LastDay);
                    var monthlyAmount  = budget.DailyAmount * period.OverlappingDays(otherPeriod);
                    totalAmount += monthlyAmount;
                }

                return totalAmount;
            }

            return 0m;
        }
    }
}