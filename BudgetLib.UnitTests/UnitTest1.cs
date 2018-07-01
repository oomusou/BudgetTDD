using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace BudgetLib.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly IBudgetRepository _budgetRepository = Substitute.For<IBudgetRepository>();
        private Accounting _accounting;

        [TestInitialize]
        public void TestInit()
        {
            _accounting = new Accounting(_budgetRepository);
        }

        [TestMethod]
        public void 日期區間無Budget()
        {
            GivenBudget();
            AmountShouldBe(0m, new DateTime(2018, 7, 15), new DateTime(2018, 7, 15));
        }

        [TestMethod]
        public void 日期區間在Budget內()
        {
            GivenBudget(new Budget {YearMonth = "201807", Amount = 31m});
            AmountShouldBe(1m, new DateTime(2018, 7, 15), new DateTime(2018, 7, 15));
        }

        [TestMethod]
        public void 日期區間在Budget前()
        {
            GivenBudget(new Budget {YearMonth = "201807", Amount = 31m});
            AmountShouldBe(0m, new DateTime(2018, 6, 15), new DateTime(2018, 6, 15));
        }

        [TestMethod]
        public void 日期區間在Budget後()
        {
            GivenBudget(new Budget {YearMonth = "201807", Amount = 31m});
            AmountShouldBe(0m, new DateTime(2018, 8, 15), new DateTime(2018, 8, 15));
        }

        private void GivenBudget(params Budget[] budgets)
        {
            _budgetRepository.GetAll().Returns(budgets.ToList());
        }

        private void AmountShouldBe(decimal expected, DateTime start, DateTime end)
        {
            var totalAmount = _accounting.TotalAmount(new Period(start, end));
            Assert.AreEqual(expected, totalAmount);
        }
    }
}