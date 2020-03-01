using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Finance.Entity.Entities;
using Finance.Data;

namespace Finance.Tests
{
    public class NpvDataShould
    {
        private List<Npv> npvs;
        private List<CashFlow> cashFlows;


        public NpvDataShould()
        {
            npvs = new List<Npv>(){
                new Npv{NpvId = 1, Name="Cashflow 1", IncrementRate = .25, LowerRate=1, UpperRate =2,
                    InitialValue =10000, TotalNpvAmount = -6145.16 },
                new Npv{NpvId = 2, Name="Cashflow 2", IncrementRate = .5, LowerRate=1, UpperRate =2,
                    InitialValue =2000, TotalNpvAmount = -39.239241451869475 }
            };

            cashFlows = new List<CashFlow>()
                {
                    new CashFlow { NpvId =1 , Amount = 1000, NpvAmount= -9009.90},
                    new CashFlow { NpvId =1 , Amount = 2000, NpvAmount= -8034.43},
                    new CashFlow { NpvId =1 , Amount = 3000, NpvAmount= -7078.1},
                    new CashFlow { NpvId =1 , Amount = 7000, NpvAmount= -6145.16},
                    new CashFlow { NpvId =2 , Amount = 1000, NpvAmount= -1009.90099009901},
                    new CashFlow { NpvId =2 , Amount = 1000, NpvAmount= -39.239241451869475}
            };

        }

        [Fact]
        public void Get_All_Finance()
        {
            var mockSet = CreateDbSetMock(npvs);

            var mockContext = new Mock<FinanceDbContext>();
            mockContext.Setup(c => c.Npvs).Returns(mockSet.Object);


            var service = new NpvData(mockContext.Object);
            var allNpvs = service.GetNpvByName(string.Empty);

            // Assert
            Assert.Equal(2, allNpvs.Count());
            Assert.Equal("Cashflow 1", allNpvs[0].Name);
        }

        [Fact]
        public void Add_New_Finance()
        {
            var mockSet = CreateDbSetMock(npvs);

            var mockContext = new Mock<FinanceDbContext>();
            mockContext.Setup(c => c.Npvs).Returns(mockSet.Object);

            var cfmockSet = CreateDbSetMock(cashFlows);
            mockContext.Setup(x => x.CashFlows).Returns(cfmockSet.Object);

            var service = new NpvData(mockContext.Object);

            var npv = service.Add(new Npv {
                Name = "Cashflow 1",
                IncrementRate = .25,
                LowerRate = 1,
                UpperRate = 2,
                InitialValue = 10000,
                TotalNpvAmount = -6145.16,
                CashFlows = new List<CashFlow>()
                {
                    new CashFlow { Amount = 1000, NpvAmount= -9009.90}
            }});


            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Delete_Existing_Finance()
        {
            var mockSet = CreateDbSetMock(npvs);

            var mockContext = new Mock<FinanceDbContext>();
            mockContext.Setup(c => c.Npvs).Returns(mockSet.Object);

            var cfmockSet = CreateDbSetMock(cashFlows);
            mockContext.Setup(x => x.CashFlows).Returns(cfmockSet.Object);

            var service = new NpvData(mockContext.Object);

            service.Delete(1);

            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Update_Existing_Finance()
        {
            var mockSet = CreateDbSetMock(npvs);

            var mockContext = new Mock<FinanceDbContext>();
            mockContext.Setup(c => c.Npvs).Returns(mockSet.Object);

            var cfmockSet = CreateDbSetMock(cashFlows);
            mockContext.Setup(x => x.CashFlows).Returns(cfmockSet.Object);

            var service = new NpvData(mockContext.Object);

            var updatedNpv = new Npv
            {
                NpvId = 1,
                Name = "Updated Cashflow 1",
                IncrementRate = .25,
                LowerRate = 1,
                UpperRate = 2,
                InitialValue = 10000,
                TotalNpvAmount = -6145.16,
                CashFlows = new List<CashFlow>()
                {
                    new CashFlow { Amount = 1000, NpvAmount= -9009.90}
                }
            };

            service.Update(updatedNpv);

            Assert.Equal(updatedNpv.Name, npvs.Where(x=>x.NpvId==updatedNpv.NpvId).FirstOrDefault().Name);

            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        private static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

            return dbSetMock;
        }
    }
}
