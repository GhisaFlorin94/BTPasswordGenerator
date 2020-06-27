using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BtPasswordGenerator.BusinessLogic.ServicesInterfaces
{
    public interface IPasswordValidatorService
    {
        bool ValidatePassword(string password, int UserId);
        bool ValidatePassword(string password, int UserId, DateTime validationDateTime);
    }
}
