using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordGenerator.Entieties;
using PasswordGenerator.Generators;

namespace PasswordGenerator.Tests.PassGeneratorTests
{
    [TestClass]
    public class VerifyDataTest
    {
        public PassGenerator Generator;
        public LoginData User;
        public string TestUserId;
        public DateTime StartTime;

        [TestInitialize]
        public void VerifyDataInitialize()
        {
            Generator = new PassGenerator();
            TestUserId = "Test user";
            StartTime = DateTime.Now;
            User = new LoginData() { UserId = TestUserId, TimeStamp = StartTime, PasswordUsed = false };
            Generator.Repository.Insert(User);

        }

        [TestMethod]
        public void VerifyData_ExistingUserCurrentDataStampPasswordNotUsed_NoChanges()
        {
            Generator.VerifyData(TestUserId, StartTime);
            var testUser = Generator.Repository.GetById(TestUserId);

            Assert.AreEqual(StartTime, testUser.TimeStamp);
            Assert.AreEqual(false, testUser.PasswordUsed);
        }

        [TestMethod]
        public void VerifyData_ExistingUserDataStampWithin30sPasswordUsed_NewDataStampPasswordUsedFalse()
        {
            Generator.Repository.GetById(TestUserId).PasswordUsed = true;

            Generator.VerifyData(TestUserId, StartTime.AddSeconds(10));
            var testUser = Generator.Repository.GetById(TestUserId);

            Assert.AreNotEqual(StartTime, testUser.TimeStamp);
            Assert.AreEqual(false, testUser.PasswordUsed);
        }

        [TestMethod]
        public void VerifyData_ExistingUserDataStampWithin40sPasswordUsed_NewDataStampPasswordUsedFalse()
        {
            Generator.Repository.GetById(TestUserId).PasswordUsed = true;

            Generator.VerifyData(TestUserId, StartTime.AddSeconds(40));
            var testUser = Generator.Repository.GetById(TestUserId);

            Assert.AreEqual(StartTime.AddSeconds(40), testUser.TimeStamp);
            Assert.AreEqual(false, testUser.PasswordUsed);
        }

        [TestMethod]
        public void VerifyData_UserNotExists_NewUserAded()
        {
            Generator.VerifyData("new user", StartTime);
            var testUser = Generator.Repository.GetById("new user");

            Assert.AreEqual(2, Generator.Repository.GetAll().Count());
            Assert.AreEqual(false, testUser.PasswordUsed);
            Assert.AreEqual(StartTime, testUser.TimeStamp);
        }
    }
}
