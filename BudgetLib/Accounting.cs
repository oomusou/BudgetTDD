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
                    totalAmount += budget.MonthlyAmount(period);
                }

                return totalAmount;
            }

            return 0m;
        }
    }
}