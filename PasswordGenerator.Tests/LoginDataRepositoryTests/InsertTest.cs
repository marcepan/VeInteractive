using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordGenerator.Entieties;
using PasswordGenerator.Repositories;

namespace PasswordGenerator.Tests.LoginDataRepositoryTests
{
    [TestClass]
    public class InsertTest
    {
        public LoginData User { get; private set; }
        public LoginDataRepository Repository;

        [TestInitialize]
        public void InsertInitialize()
        {
            User = new LoginData() { UserId = "Test user", TimeStamp = DateTime.Now, PasswordUsed = false};
            Repository = new LoginDataRepository();
        }

        [TestMethod]
        public void Insert_ValidDataEmptyList_OneElementOnList()
        {
            Repository.Insert(User);

            Assert.AreEqual(1, Repository.GetAll().Count());
        }

        [TestMethod]
        public void Insert_ValidDataNotEmptyList_ListExpanded()
        {
            Repository.Insert(User);
            var user2 = new LoginData() { UserId = "User 2", TimeStamp = DateTime.Now, PasswordUsed = false };
            Repository.Insert(user2);

            Assert.AreEqual(2, Repository.GetAll().Count());
        }

        [TestMethod]
        public void Insert_UserData_UserDataOnList()
        {
            Repository.Insert(User);

            var checkUser = from users in Repository.GetAll()
                            where users.UserId == User.UserId
                            select users;

            Assert.AreEqual(User, checkUser.FirstOrDefault());
        }

        [TestMethod]
        public void Insert_UserDataIsNull_NoChangesInList()
        {
            Repository.Insert(User);

            Repository.Insert(null);

            Assert.AreEqual(1, Repository.GetAll().Count());
        }

        [TestMethod]
        public void Insert_SameUserId_NoChangesInList()
        {
            Repository.Insert(User);

            Repository.Insert(User);

            Assert.AreEqual(1, Repository.GetAll().Count());
        }
    }
}
