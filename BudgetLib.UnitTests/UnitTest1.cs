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

        [TestMethod]
        public void 日期區間在Budget最後一天有交集()
        {
            GivenBudget(new Budget {YearMonth = "201807", Amount = 31m});
            AmountShouldBe(1m, new DateTime(2018, 7, 31), new DateTime(2018, 8, 15));
        }

        [TestMethod]
        public void 日期區間在Budget第一天有交集()
        {
            GivenBudget(new Budget {YearMonth = "201807", Amount = 31m});
            AmountShouldBe(1m, new DateTime(2018, 6, 15), new DateTime(2018, 7, 1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void 日期區間不合理()
        {
            var start = new DateTime(2018, 7, 1);
            var end = new DateTime(2018, 6, 15);
            _accounting.TotalAmount(new Period(start, end));
        }

        [TestMethod]
        public void 每天Budget不是1元()
        {
            GivenBudget(new Budget {YearMonth = "201807", Amount = 62m});
            AmountShouldBe(2m, new DateTime(2018, 7, 15), new DateTime(2018, 7, 15));
        }

        [TestMethod]
        public void 含多月Budget()
        {
            GivenBudget(
                new Budget {YearMonth = "201806", Amount = 30m},
                new Budget {YearMonth = "201807", Amount = 31m}
            );
            AmountShouldBe(31m, new DateTime(2018, 6, 15), new DateTime(2018, 7, 15));
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