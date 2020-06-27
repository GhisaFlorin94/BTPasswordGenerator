using System;

namespace BtPasswordGenerator.BusinessLogic.ServicesInterfaces
{
    public interface IPasswordGeneratorService
    {

        string GenerateOneTimePassword(int userId, DateTime dateTime);
        string GenerateOneTimePassword(int userId, string dateTime);
        string GenerateOneTimePassword(int userId);


    }
}
