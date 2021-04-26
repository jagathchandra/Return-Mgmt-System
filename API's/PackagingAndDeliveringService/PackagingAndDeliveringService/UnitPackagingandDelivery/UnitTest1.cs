using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using PackagingAndDeliveringService.Controllers;
using PackagingAndDeliveringService.Models;
using PackagingAndDeliveringService.Repository;
using System.Collections.Generic;
using System.Linq;

namespace UnitPackagingandDelivery
{
    public class Tests
    {
        List<LoginDetail> loginDetails = new List<LoginDetail>();
        IQueryable<LoginDetail> logindata;
        Mock<DbSet<LoginDetail>> mockSet;
        Mock<testContext> testContextmock;


        PackageDelivery obj = new PackageDelivery();
        List<ProcessDetail> processDetails = new List<ProcessDetail>();
        IQueryable<ProcessDetail> processdata;
        Mock<DbSet<ProcessDetail>> processmockSet;

       

       [SetUp]

        public void Setup()
        {
            loginDetails = new List<LoginDetail>()
            {
                new LoginDetail{ Username="jagath",Password="123"},
                new LoginDetail{ Username="chandra",Password="123"}

            };

            processDetails = new List<ProcessDetail>()
            {
                new ProcessDetail{ Name="jagath",ContactNumber="9012345678",CreditCardNumber=1234567890123456,ComponentType="Integral",ComponentName="Laptop",Quantity=12,IsPriorityRequest="Yes",RequestId=1,ProcessingCharge=500,PackagingAndDeliveryCharge=345},
                new ProcessDetail{ Name="chandra",ContactNumber="9112345678",CreditCardNumber=2345678901231234,ComponentType="Accessory",ComponentName="Watch",Quantity=10,IsPriorityRequest="No",RequestId=2,ProcessingCharge=300,PackagingAndDeliveryCharge=565}

            };

            logindata = loginDetails.AsQueryable();
            mockSet = new Mock<DbSet<LoginDetail>>();
            mockSet.As<IQueryable<LoginDetail>>().Setup(m => m.Provider).Returns(logindata.Provider);
            mockSet.As<IQueryable<LoginDetail>>().Setup(m => m.Expression).Returns(logindata.Expression);
            mockSet.As<IQueryable<LoginDetail>>().Setup(m => m.ElementType).Returns(logindata.ElementType);
            mockSet.As<IQueryable<LoginDetail>>().Setup(m => m.GetEnumerator()).Returns(logindata.GetEnumerator());
            var p = new DbContextOptions<testContext>();
            testContextmock = new Mock<testContext>(p);
            testContextmock.Setup(x => x.LoginDetails).Returns(mockSet.Object);

            processdata = processDetails.AsQueryable();
            processmockSet = new Mock<DbSet<ProcessDetail>>();
            processmockSet.As<IQueryable<ProcessDetail>>().Setup(m => m.Provider).Returns(processdata.Provider);
            processmockSet.As<IQueryable<ProcessDetail>>().Setup(m => m.Expression).Returns(processdata.Expression);
            processmockSet.As<IQueryable<ProcessDetail>>().Setup(m => m.ElementType).Returns(processdata.ElementType);
            processmockSet.As<IQueryable<ProcessDetail>>().Setup(m => m.GetEnumerator()).Returns(processdata.GetEnumerator());
            var pd = new DbContextOptions<testContext>();
            testContextmock = new Mock<testContext>(pd);
            testContextmock.Setup(x => x.ProcessDetails).Returns(processmockSet.Object);

        }

        [Test]
        public void TestPackageandDeliveryPass()
        {

            var packagerepo = new PackageDelivery(testContextmock.Object);
            string result = packagerepo.PackageDeliver("Accessory", 10);
            Assert.AreEqual("600",result);
        }
        [Test]
        public void TestPackageandDeliveryFail()
        {

            var packagerepo = new PackageDelivery(testContextmock.Object);
            string result = packagerepo.PackageDeliver("Accessory", 10);
            Assert.AreNotEqual("400", result);
        }
    }
}