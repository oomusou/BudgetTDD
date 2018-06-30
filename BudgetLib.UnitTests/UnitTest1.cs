using System;
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
            AmountShouldBe(0m, new DateTime(2018, 7, 15), new DateTime(2018, 7, 15));
        }

        private void AmountShouldBe(decimal expected, DateTime start, DateTime end)
        {
            var totalAmount = _accounting.TotalAmount(start, end);
            Assert.AreEqual(expected, totalAmount);
        }
    }
}