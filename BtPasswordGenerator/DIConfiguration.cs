using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BtPasswordGenerator.BusinessLogic.ServicesImplementations;
using BtPasswordGenerator.BusinessLogic.ServicesInterfaces;
using BtPasswordGenerator.DataLayer.Implementations;
using BtPasswordGenerator.DataLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BtPasswordGenerator
{
    public static class DIConfiguration
    {

        public static void ConfigureDI(this IServiceCollection service)
        {

            service.AddTransient<IPasswordGeneratorService, PasswordGeneratorService>();
            service.AddTransient<IPasswordValidatorService, PasswordValidatorService>();
            service.AddSingleton<IPasswordGuidRepository, PasswordGuidRepository>();
        }
    }
}
