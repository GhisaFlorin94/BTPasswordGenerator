using System;
using BtPasswordGenerator.BusinessLogic.Helpers;
using NUnit.Framework;

namespace PasswordGeneratorTests
{
    public class EncryptionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Encryption_DecryptKnownValue_ResultShouldBeAsExpected()
        {
          byte[] encryptedValue = { 192, 38, 65, 206, 43, 136, 103, 183, 60, 208, 161, 207, 215, 65, 87, 26 };
          var resultedValue = EncryptionHelper.Decrypt(encryptedValue);
          Assert.AreEqual("test", resultedValue);
        }

        [Test]
        public void Encryption_EncryptKnownValue_ResultShouldBeAsExpected()
        {
            var resultedValue = EncryptionHelper.Encrypt("test");

            byte[] expectedValue = { 192, 38, 65, 206, 43, 136, 103, 183, 60, 208, 161, 207, 215, 65, 87, 26 };
            Assert.AreEqual(expectedValue, resultedValue);
        }


        [Test]
        [TestCase("test")]
        [TestCase("test1")]
        [TestCase("")]
        [TestCase("LongText With Spaces and Many_SP$c!@L char")]
        public void Encryption_EncryptThenDecrypt_ResultEqualStartValue(string testValue)
        {
            var encryptedValue = EncryptionHelper.Encrypt(testValue);
            Console.WriteLine(encryptedValue);
            var resultedValue = EncryptionHelper.Decrypt(encryptedValue);

            Assert.AreEqual(testValue, resultedValue);
        }
    }
}