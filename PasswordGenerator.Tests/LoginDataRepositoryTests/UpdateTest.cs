using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordGenerator.Entieties;
using PasswordGenerator.Repositories;

namespace PasswordGenerator.Tests.LoginDataRepositoryTests
{
    [TestClass]
    public class UpdateTest
    {
        public LoginData User;
        public LoginDataRepository Repository;
        public DateTime StartTime;
        public string TestUserId;

        [TestInitialize]
        public void UpdateInitialize()
        {
            StartTime = DateTime.Now;
            TestUserId = "Test user";
            User = new LoginData() { UserId = TestUserId, TimeStamp = StartTime, PasswordUsed = false};
            Repository = new LoginDataRepository();
            Repository.Insert(User);
        }

        [TestMethod]
        public void Update_UserExistsNewTimeStamp_ModifiedTimeStamp()
        {
            var time = DateTime.Now;
            var newUser = new LoginData() { UserId = TestUserId, TimeStamp = time, PasswordUsed = false};
            
            Repository.Update(newUser);
            var user = Repository.GetById(User.UserId);

            Assert.AreEqual(time, user.TimeStamp);
        }

        [TestMethod]
        public void Update_UserExistsUsePassword_PasswordUsedTrue()
        {
            var newUser = new LoginData() { UserId = TestUserId, TimeStamp = StartTime, PasswordUsed = true };

            Repository.Update(newUser);
            var user = Repository.GetById(User.UserId);

            Assert.AreEqual(true, user.PasswordUsed);
        }

        [TestMethod]
        public void Update_UserNotExists_Null()
        {
            var noUser = new LoginData() {UserId = "user not exists", TimeStamp = DateTime.Now, PasswordUsed = false };
            Repository.Update(noUser);
            var user = Repository.GetById(User.UserId);

            Assert.AreEqual(StartTime, user.TimeStamp);
            Assert.AreEqual(false,user.PasswordUsed);
        }
    }
}
