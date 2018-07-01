using System.Collections.Generic;

namespace BudgetLib
{
    public interface IBudgetRepository
    {
        List<Budget> GetAll();
    }
}