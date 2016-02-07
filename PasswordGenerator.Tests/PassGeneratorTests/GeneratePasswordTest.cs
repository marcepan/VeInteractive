using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordGenerator.Entieties;
using PasswordGenerator.Generators;

namespace PasswordGenerator.Tests.PassGeneratorTests
{
    [TestClass]
    public class GeneratePasswordTest
    {
        public PassGenerator Generator;
        public string TestUserId;
        public DateTime StartTime;

        [TestInitialize]
        public void GeneratePasswordInitialize()
        {
            Generator = new PassGenerator();
            TestUserId = "Test user";
            StartTime = DateTime.Now;

        }

        [TestMethod]
        public void GeneratePassword_SameUserIdSameTimeStamp_SamePasswords()
        {
            Generator.Repository.Insert(new LoginData() { UserId = TestUserId, TimeStamp = StartTime, PasswordUsed = false });
            var oldPass = Generator.GeneratePassword(TestUserId);
            var newPass = Generator.GeneratePassword(TestUserId);

            Assert.AreEqual(oldPass, newPass);
        }

        [TestMethod]
        public void GeneratePassword_SameUserIdDiffrentTimeStamp_DiffrentPasswords()
        {
            Generator.Repository.Insert(new LoginData() { UserId = TestUserId, TimeStamp = StartTime, PasswordUsed = false });
            var oldPass = Generator.GeneratePassword(TestUserId);
            Generator.Repository.GetById(TestUserId).TimeStamp = StartTime.AddSeconds(55);
            var newPass = Generator.GeneratePassword(TestUserId);

            Assert.AreNotEqual(oldPass, newPass);
        }

        [TestMethod]
        public void GeneratePassword_DiffeentUsersIdSameTimeStamp_DiffrentPasswords()
        {
            Generator.Repository.Insert(new LoginData() { UserId = TestUserId, TimeStamp = StartTime, PasswordUsed = false });
            var oldPass = Generator.GeneratePassword(TestUserId);
            Generator.Repository.Insert(new LoginData() { UserId = "Diffrent user", TimeStamp = StartTime, PasswordUsed = false });
            var newPass = Generator.GeneratePassword("Diffrent user");

            Assert.AreNotEqual(oldPass, newPass);
        }

        [TestMethod]
        public void GeneratePassword_DiffeentUsersIdDiffrentTimeStamp_DiffrentPasswords()
        {
            Generator.Repository.Insert(new LoginData() { UserId = TestUserId, TimeStamp = StartTime, PasswordUsed = false });
            var oldPass = Generator.GeneratePassword(TestUserId);
            Generator.Repository.Insert(new LoginData() { UserId = "Diffrent user", TimeStamp = StartTime.AddSeconds(10), PasswordUsed = false });
            var newPass = Generator.GeneratePassword("Diffrent user");

            Assert.AreNotEqual(oldPass, newPass);
        }

    }
}
