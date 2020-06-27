using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BtPasswordGenerator.Model.RequestsModel
{
    public class PasswordValidationModel
    {
        public string Password { get; set; }
        
        public int UserId { get; set; }
    }
}
