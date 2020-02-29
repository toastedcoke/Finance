using System;
using Xunit;
using Finance.Entity;
using System.Collections.Generic;
using Moq;

namespace Finance.Core.Tests
{
    public class CalculatorShould
    {
        [Fact]
        public void Should_Accept_Lower_Rate_Lower_Or_Equal_Than_Upper_Rate()
        {
            var dto = new Npv
            {
                Name = "Cashflow 1",
                IncrementRate = 25,
                LowerRate = 1,
                UpperRate = 2,
                InitialValue = 10000,
                TotalNpvAmount = null,
                CashFlows = new List<CashFlow> {
                    new CashFlow { Amount = 1000, NpvAmount = 2700 },
                    new CashFlow { Amount = 2000, NpvAmount = 2700 } }
            };

            Mock<INpvValidate> mockValidator = new Mock<INpvValidate>();
            mockValidator.Setup(x => x.Validate(It.Is<Npv>(n=>n.LowerRate<=n.UpperRate))).Returns(true);

            var sut = new NpvCalculator(mockValidator.Object);

            var n = sut.Compute(dto);

            Assert.Equal("-7750.14", String.Format("{0:0.##}", n.TotalNpvAmount));
        }

        [Fact]
        public void Compute_NPV()
        {
            var dto = new Npv
            {
                Name = "Cashflow 1",
                IncrementRate = 25,
                LowerRate = 1,
                UpperRate = 2,
                InitialValue = 10000,
                TotalNpvAmount = null,
                CashFlows = new List<CashFlow> {
                    new CashFlow { Amount = 1000},
                    new CashFlow { Amount = 2000 } }
            };

            Mock<INpvValidate> mockValidator = new Mock<INpvValidate>();
            mockValidator.Setup(x => x.Validate(It.IsAny<Npv>())).Returns(true);

            var sut = new NpvCalculator(mockValidator.Object);

            var n = sut.Compute(dto);

            Assert.Equal("-7750.14", String.Format("{0:0.##}", n.TotalNpvAmount));
        }


        [Fact]
        public void Should_Not_Accept_Lower_Rate_Higher_Than_Upper_Rate()
        {
            var dto = new Npv
            {
                Name = "Cashflow 1",
                IncrementRate = 25,
                LowerRate = 2,
                UpperRate = 1,
                InitialValue = 10000,
                TotalNpvAmount = null,
                CashFlows = new List<CashFlow> {
                    new CashFlow { Amount = 1000, NpvAmount = 2700 },
                    new CashFlow { Amount = 2000, NpvAmount = 2700 } }
            };

            Mock<INpvValidate> mockValidator = new Mock<INpvValidate>();
            mockValidator.Setup(x => x.Validate(It.Is<Npv>(n => n.LowerRate > n.UpperRate))).Throws<Exception>();

            var sut = new NpvCalculator(mockValidator.Object);

            Assert.Throws<Exception>(() => sut.Compute(dto));
        }

        [Fact]
        public void Should_Not_Accept_If_Empty_Cashflow()
        {
            var dto = new Npv
            {
                Name = "Cashflow 1",
                IncrementRate = 25,
                LowerRate = 2,
                UpperRate = 1,
                InitialValue = 10000,
                TotalNpvAmount = null,
                CashFlows = null
            };

            Mock<INpvValidate> mockValidator = new Mock<INpvValidate>();
            mockValidator.Setup(x => x.Validate(It.Is<Npv>(n => n.CashFlows == null))).Throws<Exception>();

            var sut = new NpvCalculator(mockValidator.Object);

            Assert.Throws<Exception>(() => sut.Compute(dto));
        }
    }
}
