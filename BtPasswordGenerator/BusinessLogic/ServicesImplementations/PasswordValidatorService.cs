using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BtPasswordGenerator.BusinessLogic.Helpers;
using BtPasswordGenerator.BusinessLogic.ServicesInterfaces;
using BtPasswordGenerator.DataLayer.Interfaces;
using BtPasswordGenerator.Model;
using Newtonsoft.Json;

namespace BtPasswordGenerator.BusinessLogic.ServicesImplementations
{
    public class PasswordValidatorService : IPasswordValidatorService
    {
        private readonly IPasswordGuidRepository _passwordGuidRepository;

        public PasswordValidatorService(IPasswordGuidRepository passwordGuidRepository)
        {
            this._passwordGuidRepository = passwordGuidRepository;
        }

        public bool ValidatePassword(string password, int userId, DateTime validationDateTime)
        {

            var passwordModel = GetPasswordModelFromEncodedPassword(password);
            var storedPasswordsModels = _passwordGuidRepository.ReadActiveGuid();
            var activePasswords = storedPasswordsModels.Select(JsonConvert.DeserializeObject<PasswordModel>).ToList();

            if (activePasswords.Any(p => p.Guid == passwordModel.Guid && p.UserId == userId))
            {
                var passwordCreationDate = new DateTime(passwordModel.CreationDateTicks);
                if ((validationDateTime - passwordCreationDate).TotalSeconds < Constants.PASSWORD_LIFETIME_SECONDS && validationDateTime >= passwordCreationDate)
                    return true;
            }
            return false;
        }

        public bool ValidatePassword(string password, int userId)
        {
            return ValidatePassword(password, userId, DateTime.Now);
        }

        private PasswordModel GetPasswordModelFromEncodedPassword(string password)
        {
            var decodedPassword = EncodingHelper.Base64Decode(password);
            var decryptedPassword = EncryptionHelper.Decrypt(decodedPassword);
            return JsonConvert.DeserializeObject<PasswordModel>(decryptedPassword);
        }
    }
}
