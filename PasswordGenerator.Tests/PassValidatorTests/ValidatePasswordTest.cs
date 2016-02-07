using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordGenerator.Generators;

namespace PasswordGenerator.Tests.PassValidatorTests
{
    [TestClass]
    public class ValidatePasswordTest
    {
        public PassValidator Validator;
        public PassGenerator Generator;
        public string TestUserId;
        public DateTime StartTime;
        public string Password;

        [TestInitialize]
        public void ValidatePasswordInitialize()
        {
            Generator = new PassGenerator();
            Validator = new PassValidator(Generator);
            TestUserId = "Test user";
            StartTime = DateTime.Now;

        }

        [TestMethod]
        public void ValidatePassword_ValidPassword_True()
        {
            Password = Generator.Generate(TestUserId);
            var validatedPassword = Validator.ValidatePassword(TestUserId, Password);
            Assert.IsTrue(validatedPassword);
        }

        [TestMethod]
        public void ValidatePassword_InvalidPassword_False()
        {
            Thread.Sleep(1000);
            var validatedPassword = Validator.ValidatePassword(TestUserId, Password);
            Assert.IsFalse(validatedPassword);
        }
    }
}
