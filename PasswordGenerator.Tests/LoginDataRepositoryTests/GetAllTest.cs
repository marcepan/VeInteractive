using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordGenerator.Entieties;
using PasswordGenerator.Repositories;

namespace PasswordGenerator.Tests.LoginDataRepositoryTests
{
    [TestClass]
    public class GetAllTest
    {
        public LoginData User { get; private set; }
        public LoginDataRepository Repository;

        [TestInitialize]
        public void GetAllInitialize()
        {
            User = new LoginData() { UserId = "Test user",TimeStamp = DateTime.Now, PasswordUsed = false };
            Repository = new LoginDataRepository();
        }

        [TestMethod]
        public void Get_EmptyList_EmptyList()
        {
            var list = Repository.GetAll();

            Assert.AreEqual(0,list.Count());
        }

        [TestMethod]
        public void Get_ElementsOnList_UserList()
        {
            Repository.Insert(User);
            Repository.Insert(User);
            var list = Repository.GetAll();

            Assert.AreEqual(1, list.Count());
        }
    }
}
