using System;
using System.Collections.Generic;
using System.Text;
using BtPasswordGenerator.BusinessLogic.Helpers;
using BtPasswordGenerator.BusinessLogic.ServicesImplementations;
using BtPasswordGenerator.DataLayer.Interfaces;
using BtPasswordGenerator.Model;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace PasswordGeneratorTests
{
    class PasswordValidatorTests
    {

        private readonly Mock<IPasswordGuidRepository> _passwordGuidRepositoryMock = new Mock<IPasswordGuidRepository>();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(1,"GUID" , "01/20/2020", 1,true)]
        [TestCase(1,"GUID" , "01/20/2020",20, true)]
        [TestCase(5,"TestGUID" , "01/20/2020 13:40:10",29, true)]
        [TestCase(5,"TestGUID" , "01/20/2020 13:40:10",0, true)]
        [TestCase(5,"TestGUID" , "01/20/2020 16:40:10",60, false)]
        [TestCase(5,"TestGUID" , "01/20/2020 13:40:10",30, false)]
        [TestCase(7,"TestGUID" , "01/20/2020",-20, false)]

        public void PasswordValidator_ValidateAValidPassword_PasswordShouldBeValid(int userId, string guid, DateTime generatedDate, int secondsPassed, bool expectedResult)
        {
            //Arrange
            var storedPasswordModel = new PasswordModel()
            {
                CreationDateTicks = generatedDate.Ticks,
                Guid = guid,
                UserId = userId
            };

            var storedPasswordModelJson = JsonConvert.SerializeObject(storedPasswordModel);
            var inputPassword = EncryptAndEncodePassword(storedPasswordModelJson);
            var listOfStoredPasswords = new List<string>(){ storedPasswordModelJson };
            
            _passwordGuidRepositoryMock.Setup(x => x.ReadActiveGuid()).Returns(listOfStoredPasswords);
            var passwordValidator = new PasswordValidatorService(_passwordGuidRepositoryMock.Object);

            //Act

            var valid = passwordValidator.ValidatePassword(inputPassword, userId, generatedDate.AddSeconds(secondsPassed));

            //Assert
            
            Assert.AreEqual(expectedResult,valid);
        }


        private string EncryptAndEncodePassword(string jsonPassword)
        {
           return EncodingHelper.Base64Encode(EncryptionHelper.Encrypt(jsonPassword));
        }

    }
}
