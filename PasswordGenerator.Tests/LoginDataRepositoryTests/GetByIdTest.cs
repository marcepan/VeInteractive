using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordGenerator.Entieties;
using PasswordGenerator.Repositories;

namespace PasswordGenerator.Tests.LoginDataRepositoryTests
{
    [TestClass]
    public class GetByIdTest
    {
        public LoginData User { get; private set; }
        public LoginDataRepository Repository;
        public string TestUserId;

        [TestInitialize]
        public void GetByIdInitialize()
        {
            TestUserId = "Test user";
            User = new LoginData() { UserId = TestUserId, TimeStamp = DateTime.Now, PasswordUsed = false };
            Repository = new LoginDataRepository();
            Repository.Insert(User);
        }

        [TestMethod]
        public void GetUser_UserExists_User()
        {
            var checkUser = Repository.GetById(TestUserId);

            Assert.AreEqual(User,checkUser);
        }

        [TestMethod]
        public void GetUser_UserNotExists_Null()
        {
            var checkUser = Repository.GetById("User 9");

            Assert.IsNull(checkUser);
        }
    }
}
