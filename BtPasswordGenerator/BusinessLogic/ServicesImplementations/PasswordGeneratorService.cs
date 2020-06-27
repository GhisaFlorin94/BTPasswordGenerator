using System;
using BtPasswordGenerator.BusinessLogic.Helpers;
using BtPasswordGenerator.BusinessLogic.ServicesInterfaces;
using BtPasswordGenerator.DataLayer.Implementations;
using BtPasswordGenerator.DataLayer.Interfaces;
using BtPasswordGenerator.Model;
using Newtonsoft.Json;

namespace BtPasswordGenerator.BusinessLogic.ServicesImplementations
{
    public class PasswordGeneratorService : IPasswordGeneratorService
    {

        private readonly IPasswordGuidRepository _passwordGuidRepository;

        public PasswordGeneratorService(IPasswordGuidRepository passwordGuidRepository)
        {
            this._passwordGuidRepository = passwordGuidRepository;
        }

        public string GenerateOneTimePassword(int userId, DateTime dateTime)
        {

            string guid = Guid.NewGuid().ToString();
            var passwordModel = new PasswordModel()
            {
                Guid = guid,
                UserId = userId,
                CreationDateTicks = dateTime.Ticks
            };

            var jsonPasswordModel = JsonConvert.SerializeObject(passwordModel);
            var encryptedPasswordJsondModel = EncryptionHelper.Encrypt(jsonPasswordModel);

            var encodedPassword = EncodingHelper.Base64Encode(encryptedPasswordJsondModel);

            _passwordGuidRepository.StoreGuid(jsonPasswordModel);

            return encodedPassword;
        }

        public string GenerateOneTimePassword(int userId, string dateTime)
        {
            DateTime converteDateTime = DateTime.Parse(dateTime);
            return this.GenerateOneTimePassword(userId, converteDateTime);
        }

        public string GenerateOneTimePassword(int userId)
        {
           return this.GenerateOneTimePassword(userId, DateTime.Now);
        }
    }
}
