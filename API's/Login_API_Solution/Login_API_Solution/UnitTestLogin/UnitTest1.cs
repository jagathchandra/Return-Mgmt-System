using Login_API_Project.Models;
using Login_API_Project.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestLogin
{
    public class Tests
    {
        List<LoginDetail> loginDetails = new List<LoginDetail>();
        IQueryable<LoginDetail> logindata;
        Mock<DbSet<LoginDetail>> mockSet;
        Mock<testContext> testContextmock;


        [SetUp]
        public void Setup()
        {
            loginDetails = new List<LoginDetail>()
            {
                new LoginDetail{ Username="jagath",Password="123"},
                new LoginDetail{ Username="chandra",Password="123"}

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
        }

        [Test]
        public void TestPass()
        {
            var loginrepo = new LoginRepo(testContextmock.Object);
            var data = loginrepo.AuthenticateUser("jagath", "123");
            //string name = data.username;
            Assert.AreEqual("jagath", data);
            
        }

        [Test]
        public void TestFail()
        {
            var loginrepo = new LoginRepo(testContextmock.Object);
            var data = loginrepo.AuthenticateUser("jagath", "123");
            //string name = data.username;
            Assert.AreNotEqual("chandra", data);

        }
    }
}