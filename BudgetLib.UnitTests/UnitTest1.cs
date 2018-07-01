using System;
using System.Collections.Generic;
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
            _budgetRepository.GetAll().Returns(new List<Budget>());
            AmountShouldBe(0m, new DateTime(2018, 7, 15), new DateTime(2018, 7, 15));
        }

        [TestMethod]
        public void 日期區間在Budget內()
        {
            _budgetRepository.GetAll().Returns(new List<Budget>
            {
                new Budget {YearMonth = "201807", Amount = 31m}
            });
            AmountShouldBe(1m, new DateTime(2018, 7, 15), new DateTime(2018, 7, 15));
        }

        private void AmountShouldBe(decimal expected, DateTime start, DateTime end)
        {
            var totalAmount = _accounting.TotalAmount(start, end);
            Assert.AreEqual(expected, totalAmount);
        }
    }
}