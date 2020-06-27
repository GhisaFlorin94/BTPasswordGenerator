using System;
using System.Collections.Generic;
using System.Text;
using BtPasswordGenerator.BusinessLogic.Helpers;
using BtPasswordGenerator.BusinessLogic.ServicesImplementations;
using BtPasswordGenerator.BusinessLogic.ServicesInterfaces;
using BtPasswordGenerator.DataLayer.Interfaces;
using BtPasswordGenerator.Model;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace PasswordGeneratorTests
{
    class PasswordGeneratorTests
    {
        private readonly Mock<IPasswordGeneratorService>_passwordGeneratorServiceMock = new Mock<IPasswordGeneratorService>();
        private readonly Mock<IPasswordValidatorService> _passwordValidatorServiceMock = new Mock<IPasswordValidatorService>();
        private readonly Mock<IPasswordGuidRepository> _passwordGuidRepositoryMock = new Mock<IPasswordGuidRepository>();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(1, "01/20/2012")]
        [TestCase(21, "01/20/2100")]
        [TestCase(14, "01/20/2000")]
        [TestCase(0, "01/20/2019")]

        public void PasswordGeneration_CheckGeneration_StringShouldNotBeEmpty(int userId, DateTime generationDate)
        {
            //Arrange

            _passwordGuidRepositoryMock.Setup(x => x.StoreGuid(It.IsAny<string>())).Returns(true);
            var passwordGenerator = new PasswordGeneratorService(_passwordGuidRepositoryMock.Object);

            //Act

            var password = passwordGenerator.GenerateOneTimePassword(userId, generationDate);

            //Assert

            Assert.IsNotEmpty(password);

        }

        [Test]
        [TestCase(1, "01/20/2012")]
        [TestCase(21, "01/20/2100")]
        [TestCase(14, "01/20/2000")]
        [TestCase(0, "01/20/2019")]
        public void PasswordGeneration_CheckGeneration_DecodedPasswordShouldKeepTheValues(int userId, DateTime generationDate)
        {
            //Arrange

            _passwordGuidRepositoryMock.Setup(x => x.StoreGuid(It.IsAny<string>())).Returns(true);
            var passwordGenerator = new PasswordGeneratorService(_passwordGuidRepositoryMock.Object);

            //Act
            var password = passwordGenerator.GenerateOneTimePassword(userId, generationDate);
            var decodedPassword = EncodingHelper.Base64Decode(password);
            var passwordModel = JsonConvert.DeserializeObject<PasswordModel>(EncryptionHelper.Decrypt(decodedPassword));
            

            //Assert
            Assert.AreEqual(userId, passwordModel.UserId);
            Assert.AreEqual(generationDate, new DateTime(passwordModel.CreationDateTicks));

        }

    }
}
