using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BtPasswordGenerator.Model.ResponseModel
{
    public class PasswordResponseModel
    {

        public string Password { get; set; }
        public int SecondsValidity { get; set; }
    }
}
